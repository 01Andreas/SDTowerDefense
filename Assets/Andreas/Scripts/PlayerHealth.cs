using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float currentPlayerHealth;

    public float startingHealth;

    public void Start()
    {
        ResetPlayerHealth();
    }

    public void ResetPlayerHealth()
    {
        currentPlayerHealth = startingHealth;
    }

    public void DamagePlayer(float amt)
    {
        currentPlayerHealth -= amt;
    }

    public float GetCurrentPlayerHealth()
    {
        return currentPlayerHealth;
    }
}
