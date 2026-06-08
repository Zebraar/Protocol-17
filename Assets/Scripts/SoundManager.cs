using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioSourceVHS;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioClip clicksSound;
    [SerializeField] private AudioClip errorSound;

    private const string mixerParameterNameMaster = "Master"; 
    private const string mixerParameterNameSFX = "SFX"; 
    private const string mixerParameterNameVHS = "VHS";

    void Start()
    {
        audioSourceVHS.time = PlayerPrefs.GetFloat("Music", 0.0f);
        SetMusicVolume();
    }

    public void PlayClickSound()
    {
        audioSource.PlayOneShot(clicksSound);
    }

    public void PlayErrorSound()
    {
        audioSource.PlayOneShot(errorSound);
    }

    private void SetMusicVolume()
    {
        float dbValue = Mathf.Log10(PlayerPrefs.GetFloat("MasterVolume", 1.0f)) * 20;
        audioMixer.SetFloat(mixerParameterNameMaster, dbValue);

        dbValue = Mathf.Log10(PlayerPrefs.GetFloat("SFXVolume", 1.0f)) * 20;
        audioMixer.SetFloat(mixerParameterNameSFX, dbValue);

        dbValue = Mathf.Log10(PlayerPrefs.GetFloat("VHSVolume", 1.0f)) * 20;
        audioMixer.SetFloat(mixerParameterNameVHS, dbValue);
    }
}
