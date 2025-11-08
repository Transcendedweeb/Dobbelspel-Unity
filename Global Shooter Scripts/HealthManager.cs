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
    public GameObject animationModel;
    public GameObject player;
    public GameObject boss;

    [Header("Destruction settings")]
    public GameObject destructionEffect;
    public GameObject endScreen;

    [Header("Change material")]
    public bool changeMaterial = false;
    public GameObject modelToChange;
    public Material newMat;

    [Header("Blinking settings")]
    public bool setBlinking = false;
    public float blinkingHealthTreshold = 50f;
    public Animator animator;
    public AudioClip blinkingSound;
    public AudioSource audioSource;

    bool blinkingStarted = false;

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

        // endScreen.SetActive(true);
        player.GetComponent<PcMovement>().DisableLeanPosition();
        
        if (animationModel != null) animationModel.GetComponent<Animator>().SetBool("Destroyed", true);
        if (destructionEffect != null) destructionEffect.SetActive(true);
        if (changeMaterial) ChangeMaterial();
        if (setBlinking) PlaySfx.StopSFX(audioSource, blinkingSound);

        StopPlayer();
        StopBoss();
    }

    void UpdateHealthBar()
    {
        healthBar.fillAmount = health / maxHealth;
    }

    void ChangeMaterial()
    {
        Renderer rend = modelToChange.GetComponent<Renderer>();
        rend.material = newMat;
    }

    void CheckForBlinking()
    {
        if (blinkingHealthTreshold < health || animator == null || blinkingStarted || !setBlinking) return;

        blinkingStarted = true;
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
        player.GetComponent<PlayerMovementParticles>().DisableActiveParticles();
        player.GetComponent<PlayerMovementParticles>().enabled = false;
    }

    void StopBoss()
    {
        boss.GetComponent<BossAI>().enabled = false;
    }
}
