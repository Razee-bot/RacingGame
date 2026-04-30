using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    private bool finished = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (finished) return;   // prevent triggering twice

        if (other.CompareTag("Player"))
        {
            finished = true;

            // Stop the timer
            RaceTimer timer = FindObjectOfType<RaceTimer>();
            if (timer != null) timer.StopTimer();

            // Freeze the car
            CarController car = other.GetComponent<CarController>();
            if (car != null) car.canMove = false;

            // Load Finish Screen after 1 second delay
            Invoke(nameof(LoadFinishScreen), 1f);
        }
    }

    void LoadFinishScreen()
    {
        SceneManager.LoadScene("FinishScreen");
    }
}