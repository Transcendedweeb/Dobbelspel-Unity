using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMovementParticles : MonoBehaviour
{
    [Header("Movement particles")]
    public PcMovement playerMovement;
    public List<GameObject> movementParticlesUp = new();
    public List<GameObject> movementParticlesBack = new();
    public List<GameObject> movementParticlesLeft = new();
    public List<GameObject> movementParticlesRight = new();

    [Header("Booster start sound")]
    public AudioSource boostersSource;    
    public AudioClip startBoostersSFX;

    List<GameObject> activeParticles = new();
    string lastDir = "";
    readonly string[] movementGroupUp = new string[] { "Up", "RightUp", "LeftUp" };
    readonly string[] movementGroupDown = new string[] { "Down", "RightDown", "LeftDown" };


    void Update()
    {
        if (playerMovement == null) return;

        string dir = playerMovement.lastMov;

        if (dir != lastDir)
        {
            if (movementGroupUp.Contains(lastDir) && movementGroupUp.Contains(dir)) return;
            else if (movementGroupDown.Contains(lastDir) && movementGroupDown.Contains(dir)) return;
            else lastDir = dir;
        }
        else return;

        switch (dir)
        {
            case "Up":
            case "RightUp":
            case "LeftUp":
                EnableParticleEffect(movementParticlesUp);
                break;

            case "Down":
            case "RightDown":
            case "LeftDown":
                EnableParticleEffect(movementParticlesBack);
                break;

            case "Left":
                EnableParticleEffect(movementParticlesLeft);
                break;

            case "Right":
                EnableParticleEffect(movementParticlesRight);
                break;

            default:
                DisableActiveParticles();
                break;
        }
        PlaySfx.PlaySFX(startBoostersSFX, boostersSource);
    }

    void DisableActiveParticles()
    {
        if (!activeParticles.Any()) return;

        foreach (GameObject particle in activeParticles)
        {
            particle.SetActive(false);
        }
    }

    public void EnableParticleEffect(List<GameObject> particlesToEnable, bool disableActive = true)
    {
        if (disableActive) DisableActiveParticles();
        foreach (GameObject particle in particlesToEnable)
        {
            particle.SetActive(true);
            activeParticles.Add(particle);
        }
    }
}
