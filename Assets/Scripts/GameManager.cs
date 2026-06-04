using UnityEngine;
using UnityEngine.UI;
using System.Linq; 

public class GameManager : MonoBehaviour
{
    [Header("Current Event")]
    public EventSO currentEvent;

    [Header("UI")]
    public Text titleText;
    public Text descriptionText;
    public Text energyText;
    public Image eventImage;
    public CardItem[] cards;
    [Header("Scripts")]
    public GameState gameState = new GameState();
    public EnergyHandler energyHandler;
    public SaveManager saveManager;

    private int autoSaveDelay;

    private void Start()
    {
        saveManager.Load();
        ShowEvent(currentEvent);
        UpdateEnergyUI();
    }

    public void ShowEvent(EventSO eventData)
    {
        autoSaveDelay++;
        if(autoSaveDelay >= 5)
        {
            autoSaveDelay = 0;
            saveManager.Save();
        }
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
            if (i < currentEvent.choices.Count)
                cards[i].gameObject.SetActive(true);
            else
            {
                cards[i].gameObject.SetActive(false);
                continue;
            }

            cards[i].Setup(currentEvent.choices[i], this);
        }
    }

    public void SelectChoice(EventChoice choice)
    {
        if(!choice.requiredFlags.All(rf =>
            gameState.activeFlags.Contains(rf.flagId)))
            return;
        if(energyHandler.GetEnergy() < choice.card.energyCost) return;
        else 
        {
            energyHandler.RemoveEnergy(choice.card.energyCost); 
        }
        energyHandler.AddEnergy(choice.card.energyAdd);
        UpdateEnergyUI();

        foreach(var flag in choice.flagsToSet)
        {
            gameState.activeFlags.Add(flag.flagId);
        }

        ShowEvent(choice.nextEvent);
    }
    private void UpdateEnergyUI()
    {
        energyText.text = "Ваша энергия: " + energyHandler.GetEnergy().ToString();
    }
}