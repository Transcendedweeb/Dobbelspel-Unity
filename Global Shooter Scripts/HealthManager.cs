using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public float health = 1000f;
    public Image healthBar;
    [HideInInspector] public float maxHealth;

    void Start()
    {
        maxHealth = health;
    }


    public void AdjustHealth(int value)
    {
        health -= value;
        UpdateHealthBar();
        CheckForDeath();
    }

    void CheckForDeath()
    {
        if (health > 0) return;

        if (gameObject.CompareTag("Player")) Debug.LogWarning("PLAYER DEATH");
        else Debug.Log("BOSS DEATH");
    }

    void UpdateHealthBar()
    {
        healthBar.fillAmount = health / maxHealth;
    }
}
