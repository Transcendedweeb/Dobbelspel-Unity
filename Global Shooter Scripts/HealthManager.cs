using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public float health = 1000f;
    public Image healthBar;
    [HideInInspector] public float maxHealth;
    public GameObject endScreen;
    public GameObject player;
    public GameObject damageSpark;

    void Start()
    {
        maxHealth = health;
    }


    public void AdjustHealth(int value)
    {
        if (damageSpark != null) damageSpark.SetActive(true);
        health -= value;
        UpdateHealthBar();
        CheckForDeath();
    }

    void CheckForDeath()
    {
        if (health > 0) return;

        endScreen.SetActive(true);
        StopPlayer();
    }

    void UpdateHealthBar()
    {
        healthBar.fillAmount = health / maxHealth;
    }

    void StopPlayer()
    {
        player.GetComponent<BoxCollider>().enabled = false;
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<PcMovement>().enabled = false;
        player.GetComponent<PcShoot>().enabled = false;
        player.GetComponent<DodgeRoll>().enabled = false;
        player.GetComponent<HealthManager>().enabled = false;
    }
}
