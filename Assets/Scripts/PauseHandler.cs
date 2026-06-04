using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseHandler : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject pausePanel;
    [Header("Audio")]
    public AudioSource audioSource;
    [Header("Scripts")]
    public SaveManager saveManager;
    private bool isPause = false;

    void Start()
    {
        pausePanel.SetActive(false);
    }
    private IEnumerator TogglePause()
    {
        if(isPause)
        {
            isPause = false;
            Time.timeScale = 1.0f;
            pausePanel.GetComponent<RectTransform>().DOScale(new Vector3(0.0f, 0.0f, 0.0f), 0.3f).SetEase(Ease.InBack);
            yield return new WaitForSeconds(0.3f);
            pausePanel.SetActive(false);
        } else
        {
            isPause = true;
            pausePanel.GetComponent<RectTransform>().localScale = Vector3.zero;
            pausePanel.SetActive(true);
            pausePanel.GetComponent<RectTransform>().DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.3f).SetEase(Ease.OutBack);
            yield return new WaitForSeconds(0.3f);
            Time.timeScale = 0.0f;
        }
    }
    
    public void Pause()
    {
        StartCoroutine(TogglePause());
    }

    public void BackToMainMenu()
    {
        saveManager.Save();
        PlayerPrefs.SetFloat("Music", audioSource.time);
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainMenu");
    }
    public void SaveGame()
    {
        saveManager.Save();
        PlayerPrefs.Save();
    }
}
