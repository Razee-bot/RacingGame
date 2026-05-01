using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public TextMeshProUGUI bestTimeDisplay;

    void Start()
    {
        float best = PlayerPrefs.GetFloat("BestTime", 0f);
        if (bestTimeDisplay != null)
        {
            if (best == 0f)
                bestTimeDisplay.text = "Best Time: ---";
            else
                bestTimeDisplay.text = "Best Time: " + FormatTime(best);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    string FormatTime(float t)
    {
        int m  = (int)(t / 60);
        int s  = (int)(t % 60);
        int ms = (int)((t * 100) % 100);
        return string.Format("{0:00}:{1:00}.{2:00}", m, s, ms);
    }
}