using UnityEngine;
using System.Collections;

public class BoostZone : MonoBehaviour
{
    public float boostMultiplier = 0.50f;  // speed
    public float boostDuration  = 0.90f;  // seconds

    void OnTriggerEnter2D(Collider2D other)
    {
        CarController car = other.GetComponent<CarController>();
        if (car != null)
        {
            StartCoroutine(ApplyBoost(car));
        }
    }

    IEnumerator ApplyBoost(CarController car)
    {
        car.speedMultiplier = boostMultiplier;
        yield return new WaitForSeconds(boostDuration);
        car.speedMultiplier = 1f;  // reset to normal speed
    }
}