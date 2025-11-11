using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class EventCard
{
    public string title;
    [TextArea(2,5)]
    public string description;
}

public class EventDisplayHandler : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public float buttonHoldTime = 1f;
    public CenterCameraHandler centerHandler;

    private bool buttonHoldFlag = true;

    [Header("Deck Settings")]
    [Tooltip("Stel hier je kaarten in via de Inspector")]
    public List<EventCard> deck = new List<EventCard>();

    private List<EventCard> discardPile = new List<EventCard>();

    private const string DeckKey = "CardDeck_Save";
    private const string DiscardKey = "DiscardPile_Save";

    void OnEnable()
    {
        Debug.Log("EventDisplayHandler enabled");

        CancelInvoke();
        buttonHoldFlag = true;
        Invoke(nameof(ReleaseButton), buttonHoldTime);

        LoadDeckState();

        if (deck.Count == 0 && discardPile.Count > 0)
        {
            deck.AddRange(discardPile);
            discardPile.Clear();
            Shuffle(deck);
            SaveDeckState();
        }

        DrawAndDisplayCard();
    }

    void Update()
    {
        if (ArduinoDataManager.Instance.ButtonBPressed && !buttonHoldFlag)
        {
            Debug.Log("B pressed -> returning to menu");
            ArduinoDataManager.Instance.ResetButtonStates();
            buttonHoldFlag = true;
            ReturnToMenu();
        }
    }

    void ReleaseButton()
    {
        buttonHoldFlag = false;
    }

    void ReturnToMenu()
    {
        centerHandler.enabled = true;
        centerHandler.SendMessage("ResetVars", SendMessageOptions.DontRequireReceiver);

        if (centerHandler.canvasEvent != null)
            centerHandler.canvasEvent.SetActive(false);
        if (centerHandler.canvasIdle != null)
            centerHandler.canvasIdle.SetActive(true);
        if (centerHandler.canvasMenu != null)
            centerHandler.canvasMenu.SetActive(false);

        this.gameObject.SetActive(false);   
    }

    // -------------------- CARD HANDLING --------------------

    void DrawAndDisplayCard()
    {
        if (deck.Count > 0)
        {
            int randomIndex = Random.Range(0, deck.Count);
            EventCard card = deck[randomIndex];

            deck.RemoveAt(randomIndex);
            discardPile.Add(card);

            title.text = card.title;
            description.text = card.description;

            SaveDeckState();
        }
        else
        {
            title.text = "No More Cards";
            description.text = "All cards have been used.";
        }
    }

    void Shuffle(List<EventCard> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            EventCard temp = list[i];
            int rand = Random.Range(i, list.Count);
            list[i] = list[rand];
            list[rand] = temp;
        }
    }

    // -------------------- SAVE & LOAD --------------------

    void SaveDeckState()
    {
        PlayerPrefs.SetString(DeckKey, JsonUtility.ToJson(new CardListWrapper(deck)));
        PlayerPrefs.SetString(DiscardKey, JsonUtility.ToJson(new CardListWrapper(discardPile)));
        PlayerPrefs.Save();
    }

    void LoadDeckState()
    {
        if (PlayerPrefs.HasKey(DeckKey))
        {
            deck = JsonUtility.FromJson<CardListWrapper>(PlayerPrefs.GetString(DeckKey)).cards;
            discardPile = JsonUtility.FromJson<CardListWrapper>(PlayerPrefs.GetString(DiscardKey)).cards;
        }
    }

    [System.Serializable]
    private class CardListWrapper
    {
        public List<EventCard> cards;
        public CardListWrapper(List<EventCard> cards) { this.cards = cards; }
    }
}
