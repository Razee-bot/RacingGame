using UnityEngine;
using TMPro;

public class RaceTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;   // drag TimerText UI object here

    public static float finalTime = 0f;  // shared with FinishScreen scene
    private float  elapsed  = 0f;
    private bool   running  = true;

    void Update()
    {
        if (!running) return;

        elapsed += Time.deltaTime;
        finalTime = elapsed;

        // Format as MM:SS.mm
        int minutes = (int)(elapsed / 60);
        int seconds = (int)(elapsed % 60);
        int millis  = (int)((elapsed * 100) % 100);

        timerText.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, millis);
    }

    public void StopTimer()
    {
        running = false;
    }
}