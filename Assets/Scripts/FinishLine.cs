using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private int lapsRequired = 3; 
    private int completedLaps = 0;
    private bool isFinished = false;
    private float lastTriggerTime = 0f;
    private float triggerCooldown = 1.5f; 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isFinished || !other.CompareTag("Player")) return;
        
        // Prevent double-counting if the car stays in the trigger for multiple frames
        if (Time.time < lastTriggerTime + triggerCooldown) return;

        lastTriggerTime = Time.time;
        completedLaps++;

        // If you want to finish on the 3rd crossing:
        if (completedLaps >= lapsRequired)
        {
            HandleRaceFinish(other.gameObject);
        }
        else
        {
            Debug.Log($"Lap {completedLaps} registered! Laps to go: {lapsRequired - completedLaps}");
        }
    }

    void HandleRaceFinish(GameObject player)
    {
        isFinished = true;
        Debug.Log("Race Finished!");

        RaceTimer timer = FindObjectOfType<RaceTimer>();
        if (timer != null) timer.StopTimer();

        CarController car = player.GetComponent<CarController>();
        if (car != null) car.canMove = false;

        Invoke(nameof(LoadFinishScreen), 1f);
    }

    void LoadFinishScreen()
    {
        SceneManager.LoadScene("FinishScreen");
    }
}