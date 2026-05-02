using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    [Header("Time Display")]
    public TextMeshProUGUI currentTimeText;
    public TextMeshProUGUI bestTimeText;
    public TextMeshProUGUI newRecordText;

    [Header("UI Elements")]
    public TextMeshProUGUI titleText;
    public Button restartButton;
    public Button mainMenuButton;

    [Header("Scene Names")]
    public string gameSceneName = "GameScene";
    public string mainMenuSceneName = "MainMenu";

    public void PlayGame()
    {
    Debug.Log("Restart button clicked!");
    SceneManager.LoadScene(gameSceneName);
    }

    public void LoadMainMenu()
    {
    Debug.Log("Main Menu button clicked!");
    SceneManager.LoadScene(mainMenuSceneName);
    }

    void Start()
    {
        float current = RaceTimer.finalTime;
        float best    = PlayerPrefs.GetFloat("BestTime", float.MaxValue);

        // Title
        if (titleText) titleText.text = "RACE COMPLETE!";

        // Current time
        if (currentTimeText)
            currentTimeText.text = "Your Time\n" + FormatTime(current);

        // Check record
        if (current < best)
        {
            best = current;
            PlayerPrefs.SetFloat("BestTime", best);
            PlayerPrefs.Save();
            if (newRecordText) newRecordText.gameObject.SetActive(true);
        }
        else
        {
            if (newRecordText) newRecordText.gameObject.SetActive(false);
        }

        // Best time
        if (bestTimeText)
            bestTimeText.text = "Best Time\n" + FormatTime(best);

        // Buttons
    }

    string FormatTime(float t)
    {
        if (t == float.MaxValue) return "--:--:--";
        int m  = (int)(t / 60);
        int s  = (int)(t % 60);
        int ms = (int)((t * 100) % 100);
        return string.Format("{0:00}:{1:00}.{2:00}", m, s, ms);
    }
}