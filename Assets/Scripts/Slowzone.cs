using UnityEngine;
using System.Collections;

public class SlowZone : MonoBehaviour
{
    public float slowMultiplier = 0.4f;  // 40% speed = 60% slowdown
    public float slowDuration   = 3.0f;  // seconds to be slowed

    void OnTriggerEnter2D(Collider2D other)
    {
        CarController car = other.GetComponent<CarController>();
        if (car != null)
        {
            StartCoroutine(ApplySlow(car));
        }
    }

    IEnumerator ApplySlow(CarController car)
    {
        car.speedMultiplier = slowMultiplier;
        yield return new WaitForSeconds(slowDuration);
        car.speedMultiplier = 1f;  // restore normal speed
    }
}