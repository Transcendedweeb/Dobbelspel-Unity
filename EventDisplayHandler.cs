using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class EventDisplayHandler : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI explanationText;
    public GameObject centerCamera;
    public GameObject canvasIdle;
    public GameObject canvasMenu;

    CenterCameraHandler script;

    List<GameEvent> allEvents;
    List<GameEvent> remainingEvents;
    List<GameEvent> usedEvents;

    const string UsedEventsKey = "UsedEvents";

    void InitializeEvents()
    {
        allEvents = new List<GameEvent>
        {
            // items
            new GameEvent("Item: Mirror image", "Bij het Russisch Roulette-event mag je een andere speler kiezen om het voor jou te doen. (Let op: gewonnen geld gaat naar die speler!)"),

            new GameEvent("Item: Corrupte politicus", "Geef een belastingmuntje dat voor jou bestemd is aan een andere speler."),

            new GameEvent("Item: Chronobreak", "Na het dobbelen mag je dit item gebruiken om niet te lopen en opnieuw te dobbelen."),

            new GameEvent("Item: Mocro huurlingen", "Wanneer een andere speler op een groenvakje landt, mag je deze kaart gebruiken om hem het geld te ontnemen dat hij zou verdienen."),

            new GameEvent("Item: Lucent Loaded Dice", "Wanneer een speler gaat dobbelen dan, mag je zelf de ogen bepalen."),

            new GameEvent("Item: Discount coupon", "Wanneer je een cadeau koopt via de buyout vakje dan kost het je 5eu."),

            new GameEvent("Item: Shotgun", "Wanneer een andere speler loopt, mag je dit item gebruiken om hem een extra vakje te laten lopen."),

            new GameEvent("Item: Prismatic ethereal path", "In plaats van te dobbelen mag je naar het dichtstbijzijnde buyout-vakje gaan."),

            new GameEvent("Item: Een schaap", "Als een moslim kaart geavctiveerd wordt dan, wordt deze kaart automatisch gespeeld. De moslims zijn nu afgeleid door het schaap en de kaart heeft nu geen effect. (mocro's, turken en vluchtelingen vallen natuurlijk onder moslims)"),

            // Events

            new GameEvent("Arena sponsor", "Daag een andere speler uit voor een duel in de arena."),

            new GameEvent("Purge the disdained", "Verplaats een andere speler naar een andere plek op het bord (hij mag naar of van de Lucky, maar niet naar de Hell)."),

            new GameEvent("Moskee", "Mohammed is boos dat je jezelf nog niet hebt opgeblazen! Hij verplicht jouw om een ronde russisch roulette te doen."),

            new GameEvent("Money Printer", "Je hebt een geldprinter gevonden! Als je minder dan 5eu hebt, wordt je geld verdubbeld."),

            new GameEvent("Smerige Scheet", "Marco laat een smerige scheet, die het hele speelbord bedekt. Om koolzuurgasvergiftiging te voorkomen, slaan alle andere spelers hun volgende beurt over. (Dobbel nog een keer)"),

            new GameEvent("Vrijdag avond", "Het is vrijdagavond! In plaats van te sparen voor een cadeau, koopt Mitxel alcohol. Iedere speler genaamd Mitxel verliest 2eu (de pinpas beschermt niet tegen dit effect)."),

            new GameEvent("Moslim Huurlingen", "Ga naar de dealer en beroof... bedoel, leen het item dat hij toont zonder te vragen."),

            new GameEvent("Ultrashock taser", "Kies een andere speler, hij gooit automatisch 0 in zijn volgende beurt."),

            new GameEvent("Pantheon of Greatness", "Iedereen weet dat Enrique fcking INSANE is. Iedere speler genaamd Enrique krijgt 1eu van alle andere spelers (de pinpas beschermt niet tegen dit effect)."),

            new GameEvent("Pinautomaat", "Kies een andere speler. Hij moet zijn geld van zijn pinpas opzak nemen."),

            new GameEvent("Hoofdpijn", "Inma heeft hoofdpijn, maar gaat toch naar werk. Iedere speler genaamd Inma slaat 1 beurt over!"),

            new GameEvent("Elite mocro huurlingen", "Kies een andere speler, steel 4eu van hem en verdeel dit over de overige spelers."),

            new GameEvent("Belasting incasso", "Alle spelers met meer dan 5eu moeten 3eu aan de belasting betalen. (De pinpas beschermt niet tegen dit effect)."),

            new GameEvent("Underworld casino", "Iedere speler betaalt 2eu in de pot. Iedereen dobbelt en de speler die het hoogst dobbelt wint de pot. (Er wordt doorgedobbeld totdat er een winnaar is)."),

            new GameEvent("Smerige snotneus", "Door je smerige snotneus denkt de politie dat je corona hebt. Sla je volgende beurt over."),

            new GameEvent("Marco's zware leven", "Marco heeft een echt \"zwaar\" leven. Hij mag eindelijk uitrusten, dus iedere speler genaamd Marco slaat 2 beurten over."),

            new GameEvent("Economische crisis", "De speler die de meeste cadeaus heeft geopend, moet zijn belastingmuntjes maximaal aanvullen. Bij een gelijkspel vult hij tot 2 muntjes aan. Bij een 3-way tie of meer, vult hij tot 1 muntje."),

            new GameEvent("Stonks up!", "Je hebt 3eu op straat gevonden."),

            new GameEvent("Elite vluchtelingen", "Je bent voor 4eu op zak berooft!"),

            new GameEvent("Vluchtelingen", "Je bent voor 2eu op zak berooft!"),

            new GameEvent("Online dealer", "Ga naar de dealer event."),

            new GameEvent("Elite Turkse huurlingen", "Steel een item van een andere speler."),

            new GameEvent("Turkse huurlingen", "Steel van een andere speler 2eu op zak."),

            new GameEvent("Online bankieren", "Plaats al je geld op je bankpas."),

            new GameEvent("Knuppel", "Selecteer een andere speler; hij/zij slaat zijn/haar volgende beurt over."),

            new GameEvent("Armehuis", "Iedere speler met minder dan 4eu krijgt 1eu van de armehuis."),

            new GameEvent("Corrupte politie", "Iedere speler op een groen vakje moet 4eu aan de politie betalen."),

            new GameEvent("Tijdmachine", "Ga 3 vakjes vooruit of 5 vakjes terug. Kies zelf."),

            new GameEvent("De Canadees", "Geef iedere speler 1eu van je eigen geld (pinpas beschermt niet tegen dit effect). Als niet genoeg geld heb, dan krijg je in plaats daarvan 5eu van de bank."),
        };

        usedEvents = LoadUsedEvents();

        remainingEvents = new List<GameEvent>();
        foreach (var gameEvent in allEvents)
        {
            if (!usedEvents.Contains(gameEvent))
            {
                remainingEvents.Add(gameEvent);
            }
        }

        if (remainingEvents.Count == 0)
        {
            ResetEvents();
        }
    }

    void DisplayNextEvent()
    {
        if (remainingEvents.Count == 0)
        {
            ResetEvents();
        }

        int randomIndex = Random.Range(0, remainingEvents.Count);
        GameEvent selectedEvent = remainingEvents[randomIndex];

        titleText.text = selectedEvent.Title;
        explanationText.text = selectedEvent.Explanation;

        usedEvents.Add(selectedEvent);
        remainingEvents.RemoveAt(randomIndex);

        SaveUsedEvents();
    }

    void ResetEvents()
    {
        remainingEvents = new List<GameEvent>(allEvents);
        usedEvents.Clear();
        SaveUsedEvents();
    }

    List<GameEvent> LoadUsedEvents()
    {
        string usedEventsData = PlayerPrefs.GetString(UsedEventsKey, string.Empty);
        if (string.IsNullOrEmpty(usedEventsData)) return new List<GameEvent>();

        var eventTitles = usedEventsData.Split('|');
        var events = new List<GameEvent>();

        foreach (var title in eventTitles)
        {
            foreach (var gameEvent in allEvents)
            {
                if (gameEvent.Title == title)
                {
                    events.Add(gameEvent);
                }
            }
        }

        return events;
    }

    void SaveUsedEvents()
    {
        var eventTitles = new List<string>();
        foreach (var gameEvent in usedEvents)
        {
            eventTitles.Add(gameEvent.Title);
        }

        PlayerPrefs.SetString(UsedEventsKey, string.Join("|", eventTitles));
    }

    void Awake()
    {
        InitializeEvents();
    }

    void OnEnable()
    {
        DisplayNextEvent();
        script = centerCamera.GetComponent<CenterCameraHandler>();
    }

    void Update()
    {
        if (ArduinoDataManager.Instance.ButtonBPressed)
        {
            Debug.Log("return");
            ArduinoDataManager.Instance.ButtonBPressed = false;
            canvasIdle.SetActive(true);
            canvasMenu.SetActive(false);
            script.enabled = true;
            this.gameObject.SetActive(false);
        }
    }
}

[System.Serializable]
public class GameEvent
{
    public string Title;
    public string Explanation;

    public GameEvent(string title, string explanation)
    {
        Title = title;
        Explanation = explanation;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        GameEvent other = (GameEvent)obj;
        return Title == other.Title && Explanation == other.Explanation;
    }

    public override int GetHashCode()
    {
        return Title.GetHashCode() ^ Explanation.GetHashCode();
    }
}
