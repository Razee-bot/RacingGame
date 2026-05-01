using UnityEngine;
using TMPro;

public class CountdownManager : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public CarController carController; // drag your car here

    private float countdown = 3f;
    private bool started = false;

    void Start()
    {
        // Freeze car at start
        if (carController != null)
            carController.canMove = false;
    }

    void Update()
    {
        if (started) return;

        countdown -= Time.deltaTime;

        if (countdown > 0)
        {
            countdownText.text = Mathf.CeilToInt(countdown).ToString();
        }
        else
        {
            countdownText.text = "GO!";
            
            // Allow car to move
            if (carController != null)
                carController.canMove = true;

            // Start the race timer
            RaceTimer timer = FindObjectOfType<RaceTimer>();
            if (timer != null) timer.StartTimer();

            started = true;
            Invoke(nameof(HideCountdown), 0.5f);
        }
    }

    void HideCountdown()
    {
        countdownText.gameObject.SetActive(false);
    }
}