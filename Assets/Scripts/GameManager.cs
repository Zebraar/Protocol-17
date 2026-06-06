using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
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
    [Header("Other")]
    public GameObject vhsEffect;

    private int autoSaveDelay;
    private const float defFilmGrain = 1.0f;
    private const float defLensDestortion = 0.215f;
    private const float defChromaticAberration = 0.3f;
    private const float defBloom = 2.0f;

    private void Start()
    {
        if(PlayerPrefs.GetInt("IsNewGame", 0) == 1)
        {
            saveManager.DeleteSaveData();
            PlayerPrefs.SetInt("IsNewGame", 0);
            PlayerPrefs.Save();
        }
        EventSO oldEvent = currentEvent;
        saveManager.Load();
        if(currentEvent == null) currentEvent = oldEvent;
        ShowEvent(currentEvent);
        UpdateEnergyUI();
        SetVHSEffect();
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
    private void SetVHSEffect()
    {
        var effect = vhsEffect.GetComponent<Volume>();
        float effectValue = PlayerPrefs.GetFloat("FilmGrain", defFilmGrain);
        FilmGrain filmGrain;
        if(effect.profile.TryGet(out filmGrain))
        {
            filmGrain.intensity.value = effectValue;
        }

        LensDistortion lensDistortion;
        effectValue = PlayerPrefs.GetFloat("LensDistortion", defLensDestortion);
        if(effect.profile.TryGet(out lensDistortion))
        {
            lensDistortion.intensity.value = effectValue;
        }

        ChromaticAberration chromaticAberration;
        effectValue = PlayerPrefs.GetFloat("ChromaticAberration", defChromaticAberration);
        if(effect.profile.TryGet(out chromaticAberration))
        {
            chromaticAberration.intensity.value = effectValue;
        }

        Bloom bloom;
        effectValue = PlayerPrefs.GetFloat("Bloom", defBloom);
        if(effect.profile.TryGet(out bloom))
        {
            bloom.intensity.value = effectValue;
        }
        if(PlayerPrefs.GetInt("IsVHS", 1) == 1) vhsEffect.SetActive(true);
        else vhsEffect.SetActive(false);
    }
}