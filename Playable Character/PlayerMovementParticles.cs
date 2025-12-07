using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMovementParticles : MonoBehaviour
{
    [Header("Movement particles (auto-assigned)")]
    public PcMovement playerMovement;

    [HideInInspector] public List<GameObject> movementParticlesUp = new();
    [HideInInspector] public List<GameObject> movementParticlesBack = new();
    [HideInInspector] public List<GameObject> movementParticlesLeft = new();
    [HideInInspector] public List<GameObject> movementParticlesRight = new();

    [Header("Booster start sound")]
    public AudioSource boostersSource;
    public AudioClip startBoostersSFX;

    private List<GameObject> activeParticles = new();
    private string lastDir = "";

    private readonly string[] movementGroupUp = new[] { "Up", "RightUp", "LeftUp" };
    private readonly string[] movementGroupDown = new[] { "Down", "RightDown", "LeftDown" };


    void Start()
    {
        AutoAssignParticles();
    }

    /// <summary>
    /// Finds all MovementParticleTag components under this object
    /// and assigns them to their respective lists.
    /// </summary>
    private void AutoAssignParticles()
    {
        var tags = GetComponentsInChildren<MovementParticleTag>(true);

        movementParticlesUp.Clear();
        movementParticlesBack.Clear();
        movementParticlesLeft.Clear();
        movementParticlesRight.Clear();
        activeParticles.Clear();
        lastDir = "";

        foreach (var tag in tags)
        {
            switch (tag.direction)
            {
                case MovementParticleTag.Direction.Up:
                    movementParticlesUp.Add(tag.gameObject);
                    break;

                case MovementParticleTag.Direction.Down:
                    movementParticlesBack.Add(tag.gameObject);
                    break;

                case MovementParticleTag.Direction.Left:
                    movementParticlesLeft.Add(tag.gameObject);
                    break;

                case MovementParticleTag.Direction.Right:
                    movementParticlesRight.Add(tag.gameObject);
                    break;
            }
        }
    }


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

    public void DisableActiveParticles()
    {
        if (!activeParticles.Any()) return;

        foreach (GameObject particle in activeParticles)
        {
            if (particle != null)
                particle.SetActive(false);
        }

        activeParticles.Clear();
    }

    public void EnableParticleEffect(List<GameObject> particlesToEnable, bool disableActive = true)
    {
        if (disableActive) DisableActiveParticles();

        foreach (GameObject particle in particlesToEnable)
        {
            if (particle != null)
            {
                particle.SetActive(true);
                activeParticles.Add(particle);
            }
        }
    }
}
