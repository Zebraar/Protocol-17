using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Current Event")]
    public EventSO currentEvent;

    [Header("UI")]
    public Text titleText;
    public Text descriptionText;
    public Image eventImage;
    public CardItem[] cards;

    private void Start()
    {
        ShowEvent(currentEvent);
    }

    public void ShowEvent(EventSO eventData)
    {
        currentEvent = eventData;

        titleText.text = eventData.eventName;
        descriptionText.text = eventData.description;
        eventImage.sprite = eventData.image;

        SpawnCards();
    }

    private void SpawnCards()
    {
        for(int i = 0; i < cards.Length; i++)
        {
            cards[i].Setup(currentEvent.choices[i], this);
        }
    }

    public void SelectChoice(EventChoice choice)
    {
        ShowEvent(choice.nextEvent);
    }
}