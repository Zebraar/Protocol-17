using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScr : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject settingsPanel;
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        audioSource.time = PlayerPrefs.GetFloat("Music", 0.0f);
        CloseAllPanels();
    }
    public void CloseAllPanels()
    {
        settingsPanel.SetActive(false);
    }
    public void OpenSettingsPanel()
    {
        settingsPanel.GetComponent<RectTransform>().localScale = Vector3.zero;
        settingsPanel.SetActive(true);
        settingsPanel.GetComponent<RectTransform>().DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.3f).SetEase(Ease.OutBack);
    }
    public void ContiniueGame()
    {
        PlayerPrefs.SetFloat("Music", audioSource.time);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameScene");
    }
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("IsNewGame", 1);
        PlayerPrefs.SetFloat("Music", audioSource.time);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameScene");
    }
    public void ExitGame()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }
}
