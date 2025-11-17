using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Centralized provider for player and playerMarker references in multiplayer boss fights.
/// This prevents attack scripts from caching stale references when a player is swapped.
/// 
/// Attack scripts should use GetPlayer() and GetPlayerMarker() instead of caching references.
/// </summary>
public class PlayerReferenceProvider : MonoBehaviour
{
    private BossAI bossAI;

    void Start()
    {
        bossAI = GetComponent<BossAI>();
        
        if (bossAI == null)
        {
            Debug.LogError("PlayerReferenceProvider must be on the same GameObject as BossAI!");
        }
    }

    /// <summary>
    /// Get the current player reference. Always returns the active player.
    /// </summary>
    public GameObject GetPlayer()
    {
        if (bossAI == null || bossAI.player == null)
        {
            Debug.LogWarning("PlayerReferenceProvider: Player reference is null!");
            return null;
        }

        // Check if the player reference is still valid (not destroyed)
        if (bossAI.player.Equals(null))
        {
            Debug.LogWarning("PlayerReferenceProvider: Player reference has been destroyed!");
            return null;
        }

        return bossAI.player;
    }

    /// <summary>
    /// Get the current playerMarker reference. Always returns the active marker.
    /// </summary>
    public GameObject GetPlayerMarker()
    {
        if (bossAI == null || bossAI.playerMarker == null)
        {
            Debug.LogWarning("PlayerReferenceProvider: PlayerMarker reference is null!");
            return null;
        }

        // Check if the marker reference is still valid (not destroyed)
        if (bossAI.playerMarker.Equals(null))
        {
            Debug.LogWarning("PlayerReferenceProvider: PlayerMarker reference has been destroyed!");
            return null;
        }

        return bossAI.playerMarker;
    }

    /// <summary>
    /// Safely get player position, with fallback null check.
    /// </summary>
    public Vector3 GetPlayerPosition()
    {
        GameObject player = GetPlayer();
        if (player == null)
        {
            return transform.position; // Fallback to boss position if player not found
        }
        return player.transform.position;
    }

    /// <summary>
    /// Safely get playerMarker position, with fallback null check.
    /// </summary>
    public Vector3 GetPlayerMarkerPosition()
    {
        GameObject marker = GetPlayerMarker();
        if (marker == null)
        {
            return transform.position; // Fallback to boss position if marker not found
        }
        return marker.transform.position;
    }

    /// <summary>
    /// Check if both player and playerMarker references are valid.
    /// </summary>
    public bool AreReferencesValid()
    {
        GameObject player = GetPlayer();
        GameObject marker = GetPlayerMarker();
        return player != null && marker != null;
    }
}
