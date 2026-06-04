using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clicksSound;

    public void PlayClickSound()
    {
        audioSource.PlayOneShot(clicksSound);
    }
}
