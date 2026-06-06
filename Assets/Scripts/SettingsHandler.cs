using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsHandler : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Slider vhsVolumeSlider;

    [Header("Texts")]
    [SerializeField] private Text masterVolumeText;
    [SerializeField] private Text sfxVolumeText;
    [SerializeField] private Text vhsVolumeText;

    [Header("Audio")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Other")]
    [SerializeField] private GameObject vhsEffect;

    private const string mixerParameterNameMaster = "Master"; 
    private const string mixerParameterNameSFX = "SFX"; 
    private const string mixerParameterNameVHS = "VHS";

    void Start()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1.0f);
        masterVolumeText.text = $"{masterVolumeSlider.value:P0}";

        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
        sfxVolumeText.text = $"{sfxVolumeSlider.value:P0}";

        vhsVolumeSlider.value = PlayerPrefs.GetFloat("VHSVolume", 1.0f);
        vhsVolumeText.text = $"{vhsVolumeSlider.value:P0}";
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
}
