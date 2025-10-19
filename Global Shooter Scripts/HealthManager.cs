using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [Header("General settings")]
    public float health = 1000f;
    public Image healthBar;
    [HideInInspector] public float maxHealth;
    public GameObject endScreen;
    public GameObject player;

    [Header("Blinking settings")]
    public bool setBlinking = false;
    public float blinkingHealthTreshold = 50f;
    public Animator animator;
    public AudioClip blinkingSound;
    public AudioSource audioSource;

    void Start()
    {
        maxHealth = health;
        CheckForBlinking();
    }


    public void AdjustHealth(int value)
    {
        health -= value;
        CheckForBlinking();
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

    void CheckForBlinking()
    {
        if ((blinkingHealthTreshold < health || animator == null) && !setBlinking) return;

        animator.SetBool("blink", true);
        PlaySfx.PlaySFX(blinkingSound, audioSource, loop: true);
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
