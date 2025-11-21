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
        if (health > 0) health -= value;
        if (health < 0) health = 0;
        CheckForBlinking();
        UpdateHealthBar();
        CheckForDeath();
    }

    void CheckForDeath()
    {
        if (health > 0) return;

        // Determine if this HealthManager is on a player or boss
        bool isPlayer = IsPlayerHealthManager();
        
        if (isPlayer)
        {
            HandlePlayerDeath();
        }
        else
        {
            HandleBossDeath();
        }
    }
    
    bool IsPlayerHealthManager()
    {
        // Check if this HealthManager is attached to a player
        // A player will have PcMovement component
        return GetComponent<PcMovement>() != null;
    }
    
    void HandlePlayerDeath()
    {
        GameObject deadPlayer = gameObject;
        
        // Play death animation and effects
        if (player != null && player.GetComponent<PcMovement>() != null)
        {
            player.GetComponent<PcMovement>().DisableLeanPosition();
        }
        
        if (animationModel != null) animationModel.GetComponent<Animator>().SetBool("Destroyed", true);
        if (destructionEffect != null) destructionEffect.SetActive(true);
        if (changeMaterial) ChangeMaterial();
        if (setBlinking) PlaySfx.StopSFX(audioSource, blinkingSound);

        StopPlayer();
        
        // Notify PlayerManager about player death
        PlayerManager playerManager = FindObjectOfType<PlayerManager>();
        if (playerManager != null)
        {
            playerManager.OnPlayerDeath(deadPlayer);
        }
    }
    
    void HandleBossDeath()
    {
        // endScreen.SetActive(true);
        if (animationModel != null) animationModel.GetComponent<Animator>().SetBool("Destroyed", true);
        if (destructionEffect != null) destructionEffect.SetActive(true);
        if (changeMaterial) ChangeMaterial();
        if (setBlinking) PlaySfx.StopSFX(audioSource, blinkingSound);
        
        StopBoss();
        StopAllPlayers();
    }
    
    void StopAllPlayers()
    {
        // Find and stop all players in the game
        PcMovement[] allPlayers = FindObjectsOfType<PcMovement>();
        
        foreach (PcMovement playerMovement in allPlayers)
        {
            StopPlayer(playerMovement.gameObject);
        }
    }

    public void UpdateHealthBar()
    {
        if (healthBar != null)
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

    void StopPlayer(GameObject playerObj = null)
    {
        if (playerObj == null)
            playerObj = player != null ? player : gameObject;
        
        if (playerObj.GetComponent<BoxCollider>() != null)
            playerObj.GetComponent<BoxCollider>().enabled = false;
        if (playerObj.GetComponent<CharacterController>() != null)
            playerObj.GetComponent<CharacterController>().enabled = false;
        if (playerObj.GetComponent<PcMovement>() != null)
            playerObj.GetComponent<PcMovement>().enabled = false;
        if (playerObj.GetComponent<PcShoot>() != null)
            playerObj.GetComponent<PcShoot>().enabled = false;
        if (playerObj.GetComponent<DodgeRoll>() != null)
            playerObj.GetComponent<DodgeRoll>().enabled = false;
        if (playerObj.GetComponent<HealthManager>() != null)
            playerObj.GetComponent<HealthManager>().enabled = false;
        if (playerObj.GetComponent<PlayerMovementParticles>() != null)
        {
            playerObj.GetComponent<PlayerMovementParticles>().DisableActiveParticles();
            playerObj.GetComponent<PlayerMovementParticles>().enabled = false;
        }
    }

    void StopBoss()
    {
        GameObject bossObj = boss != null ? boss : gameObject;
        
        if (bossObj.GetComponent<BossAI>() != null)
            bossObj.GetComponent<BossAI>().enabled = false;
    }
}
