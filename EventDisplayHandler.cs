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
            new GameEvent("Event 1", "This is the first event."),
            new GameEvent("Event 2", "This is the second event."),
            new GameEvent("Event 3", "This is the third event."),
            new GameEvent("Event 4", "This is the fourth event."),
            new GameEvent("Event 5", "This is the fifth event."),
            new GameEvent("Event 6", "This is the sixth event."),
            new GameEvent("Event 7", "This is the seventh event."),
            new GameEvent("Event 8", "This is the eighth event."),
            new GameEvent("Event 9", "This is the ninth event."),
            new GameEvent("Event 10", "This is the tenth event.")
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
