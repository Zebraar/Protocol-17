using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class SettingsHandler : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Slider vhsVolumeSlider;
    [SerializeField] private Slider filmGrainSlider;
    [SerializeField] private Slider lensDestortionSlider;
    [SerializeField] private Slider chromaticAberrationSlider;
    [SerializeField] private Slider bloomSlider;
    [SerializeField] private Toggle vhsToggle;

    [Header("Texts")]
    [SerializeField] private Text masterVolumeText;
    [SerializeField] private Text sfxVolumeText;
    [SerializeField] private Text vhsVolumeText;
    [SerializeField] private Text filmGrainText;
    [SerializeField] private Text lensDestortionText;
    [SerializeField] private Text chromaticAberrationText;
    [SerializeField] private Text bloomText;

    [Header("Audio")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Other")]
    [SerializeField] private GameObject vhsEffect;

    private const string mixerParameterNameMaster = "Master"; 
    private const string mixerParameterNameSFX = "SFX"; 
    private const string mixerParameterNameVHS = "VHS";

    private const float defFilmGrain = 1.0f;
    private const float defLensDestortion = 0.215f;
    private const float defChromaticAberration = 0.3f;
    private const float defBloom = 2.0f;
    private const float defMaster = 1.0f;
    private const float defSFX = 1.0f;
    private const float defVHS = 1.0f;

    void Start()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1.0f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
        vhsVolumeSlider.value = PlayerPrefs.GetFloat("VHSVolume", 1.0f);

        filmGrainSlider.value = PlayerPrefs.GetFloat("FilmGrain", defFilmGrain);
        lensDestortionSlider.value = PlayerPrefs.GetFloat("LensDistortion", defLensDestortion);
        chromaticAberrationSlider.value = PlayerPrefs.GetFloat("ChromaticAberration", defChromaticAberration);
        bloomSlider.value = PlayerPrefs.GetFloat("Bloom", defBloom);
        if(PlayerPrefs.GetInt("IsVHS", 1) == 1) vhsToggle.isOn = true;
        else vhsToggle.isOn = false;
    }

    public void OnMasterVolumeChanged()
    {
        float dbValue = Mathf.Log10(masterVolumeSlider.value) * 20;
        audioMixer.SetFloat(mixerParameterNameMaster, dbValue);

        masterVolumeText.text = $"{masterVolumeSlider.value:P0}";

        PlayerPrefs.SetFloat("MasterVolume", masterVolumeSlider.value);
        PlayerPrefs.Save();
    }

    public void OnSFXVolumeChanged()
    {
        float dbValue = Mathf.Log10(sfxVolumeSlider.value) * 20;
        audioMixer.SetFloat(mixerParameterNameSFX, dbValue);

        sfxVolumeText.text = $"{sfxVolumeSlider.value:P0}";

        PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value);
        PlayerPrefs.Save();
    }

    public void OnVHSVolumeChanged()
    {
        float dbValue = Mathf.Log10(vhsVolumeSlider.value) * 20;
        audioMixer.SetFloat(mixerParameterNameVHS, dbValue);

        vhsVolumeText.text = $"{vhsVolumeSlider.value:P0}";

        PlayerPrefs.SetFloat("VHSVolume", vhsVolumeSlider.value);
        PlayerPrefs.Save();
    }

    public void OnFilmGrainChanged()
    {
        var effect = vhsEffect.GetComponent<Volume>();
        FilmGrain filmGrain;
        if(effect.profile.TryGet(out filmGrain))
        {
            filmGrain.intensity.value = filmGrainSlider.value;
            filmGrainText.text = $"{filmGrainSlider.value:P0}";
            PlayerPrefs.SetFloat("FilmGrain", filmGrainSlider.value);
            PlayerPrefs.Save();
        }
    }
    public void OnLensDestortionChanged()
    {
        var effect = vhsEffect.GetComponent<Volume>();
        LensDistortion lensDistortion;
        if(effect.profile.TryGet(out lensDistortion))
        {
            lensDistortion.intensity.value = lensDestortionSlider.value;
            lensDestortionText.text = $"{lensDestortionSlider.value:P0}";
            PlayerPrefs.SetFloat("LensDistortion", lensDestortionSlider.value);
            PlayerPrefs.Save();
        }
    }
    public void OnChromaticAberrationChanged()
    {
        var effect = vhsEffect.GetComponent<Volume>();
        ChromaticAberration chromaticAberration;
        if(effect.profile.TryGet(out chromaticAberration))
        {
            chromaticAberration.intensity.value = chromaticAberrationSlider.value;
            chromaticAberrationText.text = $"{chromaticAberrationSlider.value:P0}";
            PlayerPrefs.SetFloat("ChromaticAberration", chromaticAberrationSlider.value);
            PlayerPrefs.Save();
        }
    }
    public void OnBloomChanged()
    {
        var effect = vhsEffect.GetComponent<Volume>();
        Bloom bloom;
        if(effect.profile.TryGet(out bloom))
        {
            bloom.intensity.value = bloomSlider.value;
            bloomText.text = $"{bloomSlider.value:P0}";
            PlayerPrefs.SetFloat("Bloom", bloomSlider.value);
            PlayerPrefs.Save();
        }
    }
    public void OnVHSToggleChanged()
    {
        vhsEffect.SetActive(vhsToggle.isOn);
        if(vhsToggle.isOn == false) PlayerPrefs.SetInt("IsVHS", 0);
        else PlayerPrefs.SetInt("IsVHS", 1);
        PlayerPrefs.Save();
    }
    public void BackToStandard()
    {
        filmGrainSlider.value = defFilmGrain;
        lensDestortionSlider.value = defLensDestortion;
        chromaticAberrationSlider.value = defChromaticAberration;
        bloomSlider.value = defBloom;
        masterVolumeSlider.value = defMaster;
        sfxVolumeSlider.value = defSFX;
        vhsVolumeSlider.value = defVHS;
        vhsToggle.isOn = true;
    }
}
