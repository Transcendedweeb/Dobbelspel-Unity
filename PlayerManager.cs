using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [Header("Player Settings")]
    public GameObject originalPlayer; // The first player in the scene
    public GameObject deadBodyPrefab; // Prefab to replace dead player
    public GameObject modelPrefab; // Prefab to replace the child model on spawn
    
    [Header("UI Transition Settings")]
    public GameObject transitionUI; // UI panel to show during transition
    public TextMeshProUGUI transitionText; // Text showing "Player 2", "Player 3", etc.
    public float transitionTime = 3f; // Time for transition (editable in inspector)
    
    [Header("Game Over Settings")]
    public GameObject gameOverScreen; // Screen to show when all players die
    
    private int totalPlayers = 1;
    private int currentPlayerIndex = 0;
    private int alivePlayers = 0;
    private Vector3 spawnPosition;
    private GameObject currentPlayer;
    private HealthManager currentPlayerHealth;
    private BossAI bossAI;
    private GameObject boss;
    private bool isTransitioning = false;
    
    // Store original player's component settings
    private Dictionary<System.Type, Component> originalComponents = new Dictionary<System.Type, Component>();
    private Dictionary<System.Type, Dictionary<string, object>> originalComponentValues = new Dictionary<System.Type, Dictionary<string, object>>();
    
    // Store initial active states of child GameObjects
    private Dictionary<string, bool> originalChildStates = new Dictionary<string, bool>();
    
    // Store original materials from each renderer (before any modifications)
    private Dictionary<string, Material[]> originalMaterials = new Dictionary<string, Material[]>();
    
    void Awake()
    {
        // Store original player's component settings BEFORE any gameplay modifications
        // This must happen in Awake to capture the initial state
        if (originalPlayer != null)
        {
            StoreOriginalPlayerSettings(originalPlayer);
            StoreOriginalChildStates(originalPlayer);
            StoreOriginalMaterials(originalPlayer);
        }
    }
    
    void StoreOriginalChildStates(GameObject player)
    {
        // Store the initial active state of all child GameObjects
        Transform[] allChildren = player.GetComponentsInChildren<Transform>(true);
        foreach (Transform child in allChildren)
        {
            if (child != player.transform)
            {
                string path = GetRelativePath(player.transform, child);
                // If path is empty, it means it's a direct child - use just the name
                if (string.IsNullOrEmpty(path))
                {
                    path = child.name;
                }
                originalChildStates[path] = child.gameObject.activeSelf;
            }
        }
    }
    
    void StoreOriginalMaterials(GameObject player)
    {
        // Store the original materials from all renderers before any modifications
        Renderer[] renderers = player.GetComponentsInChildren<Renderer>(true);
        foreach (Renderer renderer in renderers)
        {
            if (renderer == null || renderer.sharedMaterials == null) continue;
            
            string path = GetRelativePath(player.transform, renderer.transform);
            if (string.IsNullOrEmpty(path))
            {
                path = renderer.name;
            }
            
            // Store a copy of the material array (not references that might be modified)
            Material[] mats = new Material[renderer.sharedMaterials.Length];
            System.Array.Copy(renderer.sharedMaterials, mats, renderer.sharedMaterials.Length);
            originalMaterials[path] = mats;
        }
    }
    
    void Start()
    {
        // Get player count from PlayerPrefs (set by CenterCameraHandler)
        totalPlayers = PlayerPrefs.GetInt("PlayerCount", 1);
        totalPlayers = Mathf.Clamp(totalPlayers, 1, 4);
        alivePlayers = totalPlayers;
        
        // Store spawn position from original player
        if (originalPlayer != null)
        {
            spawnPosition = originalPlayer.transform.position;
            currentPlayer = originalPlayer;
            currentPlayerHealth = originalPlayer.GetComponent<HealthManager>();
        }
        
        // Find boss and BossAI
        boss = GameObject.FindGameObjectWithTag("Boss");
        if (boss == null)
        {
            boss = FindObjectOfType<BossAI>()?.gameObject;
        }
        
        if (boss != null)
        {
            bossAI = boss.GetComponent<BossAI>();
        }
        
        // Subscribe to player death
        if (currentPlayerHealth != null)
        {
            // We'll handle this through HealthManager modification
        }
        
        // Hide transition UI initially
        if (transitionUI != null)
        {
            transitionUI.SetActive(false);
        }
        
        // Hide game over screen initially
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }
    }
    
    void StoreOriginalPlayerSettings(GameObject player)
    {
        // Store all MonoBehaviour components and their serialized field values
        // This captures the initial inspector state before any gameplay modifications
        MonoBehaviour[] components = player.GetComponents<MonoBehaviour>();
        
        foreach (MonoBehaviour comp in components)
        {
            if (comp == null) continue;
            
            System.Type compType = comp.GetType();
            originalComponents[compType] = comp;
            
            // Store serialized field values using reflection
            Dictionary<string, object> fieldValues = new Dictionary<string, object>();
            var fields = compType.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            
            foreach (var field in fields)
            {
                // Skip HideInInspector fields
                if (System.Attribute.IsDefined(field, typeof(HideInInspector))) continue;
                
                try
                {
                    object value = field.GetValue(comp);
                    // Store the value (for value types and references)
                    fieldValues[field.Name] = value;
                }
                catch { }
            }
            
            originalComponentValues[compType] = fieldValues;
        }
    }
    
    public void OnPlayerDeath(GameObject deadPlayer)
    {
        if (isTransitioning) return;
        
        currentPlayerIndex++;
        alivePlayers--;
        
        if (alivePlayers <= 0)
        {
            // All players dead - game over
            StartCoroutine(GameOver());
            return;
        }
        
        // Start transition to next player
        StartCoroutine(TransitionToNextPlayer(deadPlayer));
    }
    
    IEnumerator TransitionToNextPlayer(GameObject deadPlayer)
    {
        isTransitioning = true;
        
        // Show transition UI
        if (transitionUI != null)
        {
            transitionUI.SetActive(true);
        }
        
        if (transitionText != null)
        {
            transitionText.text = $"Player {currentPlayerIndex + 1}";
        }
        
        // Wait for transition time
        yield return new WaitForSeconds(transitionTime);
        
        // Store old player reference before spawning new one
        GameObject oldPlayer = currentPlayer;
        
        GameObject newPlayer = SpawnNewPlayer();

        // NEW: update original BEFORE destroying old player
        originalPlayer = newPlayer;

        UpdateAllPlayerReferences(newPlayer, oldPlayer);

        // Now safe to destroy the old one
        ReplaceDeadPlayer(deadPlayer);
        
        // Hide transition UI
        if (transitionUI != null)
        {
            transitionUI.SetActive(false);
        }
        
        isTransitioning = false;
    }
    
    GameObject SpawnNewPlayer()
    {
        // Instantiate new player at spawn position
        // Unity will automatically copy all components and their inspector values from originalPlayer
        GameObject newPlayer = Instantiate(originalPlayer, spawnPosition, originalPlayer.transform.rotation);
        newPlayer.name = $"Player {currentPlayerIndex + 1}";
        // Replace the child model with a prefab if provided (helps fix skew/rotation issues)
        ReplaceModelWithPrefab(newPlayer);

        // Update references that point to the old player to point to the new player
        UpdatePlayerInternalReferences(newPlayer);
        
        // Restore child GameObject active states (like "Destroyed Handler", "AudioSourceHolder", etc.)
        RestoreChildGameObjectStates(newPlayer);
        
        // Reset health to max
        HealthManager newPlayerHealth = newPlayer.GetComponent<HealthManager>();
        if (newPlayerHealth != null)
        {
            newPlayerHealth.health = newPlayerHealth.maxHealth;
            newPlayerHealth.UpdateHealthBar();
        }
        
        // Reset animation states
        ResetPlayerAnimations(newPlayer);
        
        // Reset materials to original state
        ResetPlayerMaterials(newPlayer);
        
        // Enable all player components (they should already be enabled from instantiation, but ensure it)
        EnablePlayerComponents(newPlayer);
        
        currentPlayer = newPlayer;
        currentPlayerHealth = newPlayerHealth;
        originalPlayer = newPlayer;
        return newPlayer;
    }
    
    void UpdatePlayerInternalReferences(GameObject newPlayer)
    {
        // Copy all component settings from the stored original values
        // This ensures we get the initial inspector state, not runtime-modified values
        MonoBehaviour[] newComponents = newPlayer.GetComponents<MonoBehaviour>();
        
        foreach (MonoBehaviour newComp in newComponents)
        {
            if (newComp == null) continue;
            
            System.Type compType = newComp.GetType();
            
            // Check if we have stored original values for this component type
            if (originalComponentValues.ContainsKey(compType))
            {
                Dictionary<string, object> storedValues = originalComponentValues[compType];
                var fields = compType.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                
                foreach (var field in fields)
                {
                    if (System.Attribute.IsDefined(field, typeof(HideInInspector))) continue;
                    if (!storedValues.ContainsKey(field.Name)) continue;
                    
                    try
                    {
                        object originalValue = storedValues[field.Name];
                        
                        // Handle GameObject references - need to map to new player's hierarchy
                        if (originalValue is GameObject originalGO && originalGO != null)
                        {
                            GameObject newValue = null;
                            
                            // If it was the original player itself, use new player
                            if (originalGO == originalPlayer)
                            {
                                newValue = newPlayer;
                            }
                            // If it was a child of the original player, find equivalent in new player
                            else if (originalGO.transform.IsChildOf(originalPlayer.transform))
                            {
                                string relativePath = GetRelativePath(originalPlayer.transform, originalGO.transform);
                                if (!string.IsNullOrEmpty(relativePath))
                                {
                                    Transform newTarget = newPlayer.transform.Find(relativePath);
                                    if (newTarget != null)
                                    {
                                        newValue = newTarget.gameObject;
                                    }
                                }
                            }
                            // If it's a scene object (not a child of player), try to find by name
                            else
                            {
                                // Try to find by name (for scene objects like boss, UI elements, etc.)
                                GameObject found = GameObject.Find(originalGO.name);
                                if (found != null && found != newPlayer)
                                {
                                    newValue = found;
                                }
                                else
                                {
                                    // Keep the original reference if it's a prefab or persistent object
                                    newValue = originalGO;
                                }
                            }
                            
                            if (newValue != null)
                            {
                                field.SetValue(newComp, newValue);
                            }
                        }
                        // Handle Component references
                        else if (originalValue is Component originalComp && originalComp != null)
                        {
                            Component newComponentRef = null;
                            
                            // Find equivalent component in new player's hierarchy
                            if (originalComp.gameObject == originalPlayer)
                            {
                                newComponentRef = newPlayer.GetComponent(originalComp.GetType());
                            }
                            else if (originalComp.transform.IsChildOf(originalPlayer.transform))
                            {
                                string relativePath = GetRelativePath(originalPlayer.transform, originalComp.transform);
                                if (!string.IsNullOrEmpty(relativePath))
                                {
                                    Transform newTarget = newPlayer.transform.Find(relativePath);
                                    if (newTarget != null)
                                    {
                                        newComponentRef = newTarget.GetComponent(originalComp.GetType());
                                    }
                                }
                            }
                            else
                            {
                                // Keep original component reference for scene objects
                                newComponentRef = originalComp;
                            }
                            
                            if (newComponentRef != null)
                            {
                                field.SetValue(newComp, newComponentRef);
                            }
                        }
                        // For value types and other objects, copy directly
                        else if (originalValue != null)
                        {
                            field.SetValue(newComp, originalValue);
                        }
                    }
                    catch (System.Exception ex)
                    {
                        Debug.LogWarning($"Failed to copy field {field.Name} in {compType.Name}: {ex.Message}");
                    }
                }
            }
        }
        
        // Ensure HealthManager's player reference points to itself
        HealthManager healthManager = newPlayer.GetComponent<HealthManager>();
        if (healthManager != null)
        {
            healthManager.player = newPlayer;
        }
    }
    
    void ResetPlayerAnimations(GameObject player)
    {
        // Reset animation states - only set parameters that exist
        Animator[] animators = player.GetComponentsInChildren<Animator>();
        foreach (Animator anim in animators)
        {
            if (anim == null || !anim.isInitialized) continue;
            
            // Check and reset "Destroyed" parameter if it exists
            if (HasParameter(anim, "Destroyed"))
            {
                anim.SetBool("Destroyed", false);
            }
            
            // Check and reset "blink" parameter if it exists
            if (HasParameter(anim, "blink"))
            {
                anim.SetBool("blink", false);
            }
            
            // Check and reset "Lean position" parameter if it exists
            if (HasParameter(anim, "Lean position"))
            {
                anim.SetInteger("Lean position", 0);
            }
            
            // Check and reset "Dash position" parameter if it exists
            if (HasParameter(anim, "Dash position"))
            {
                anim.SetInteger("Dash position", 5); // reset state
            }
            
            // Reset any boolean parameters that might be set to true
            foreach (AnimatorControllerParameter param in anim.parameters)
            {
                if (param.type == AnimatorControllerParameterType.Bool && anim.GetBool(param.name))
                {
                    anim.SetBool(param.name, false);
                }
            }
            
            // Force animator to update immediately so state transitions happen
            anim.Update(0f);
        }
    }
    
    bool HasParameter(Animator animator, string paramName)
    {
        if (animator == null || !animator.isInitialized) return false;
        
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name == paramName)
            {
                return true;
            }
        }
        return false;
    }
    
    void ResetPlayerMaterials(GameObject newPlayer)
    {
        // Restore materials from the stored original materials (captured before any death modifications)
        Renderer[] renderers = newPlayer.GetComponentsInChildren<Renderer>();
        
        foreach (Renderer newRenderer in renderers)
        {
            if (newRenderer == null) continue;
            
            string path = GetRelativePath(newPlayer.transform, newRenderer.transform);
            if (string.IsNullOrEmpty(path))
            {
                path = newRenderer.name;
            }
            
            // Check if we have stored original materials for this renderer
            if (originalMaterials.ContainsKey(path))
            {
                Material[] storedMats = originalMaterials[path];
                if (storedMats != null && storedMats.Length > 0)
                {
                    // Create a new array and copy the stored materials
                    Material[] resetMaterials = new Material[storedMats.Length];
                    System.Array.Copy(storedMats, resetMaterials, storedMats.Length);
                    newRenderer.sharedMaterials = resetMaterials;
                }
            }
        }
    }
    
    void RestoreChildGameObjectStates(GameObject newPlayer)
    {
        // Restore the active state of all child GameObjects to match the original
        foreach (var kvp in originalChildStates)
        {
            string path = kvp.Key;
            bool shouldBeActive = kvp.Value;
            
            Transform child = null;
            
            // Try to find by path first (for nested children)
            if (path.Contains("/"))
            {
                child = newPlayer.transform.Find(path);
            }
            
            // If not found or it's a direct child, try by name
            if (child == null)
            {
                // Search all children recursively
                Transform[] allChildren = newPlayer.GetComponentsInChildren<Transform>(true);
                foreach (Transform t in allChildren)
                {
                    if (t != newPlayer.transform && GetRelativePath(newPlayer.transform, t) == path)
                    {
                        child = t;
                        break;
                    }
                    // Also check direct name match for direct children
                    if (t.parent == newPlayer.transform && t.name == path)
                    {
                        child = t;
                        break;
                    }
                }
            }
            
            if (child != null)
            {
                child.gameObject.SetActive(shouldBeActive);
            }
            else
            {
                // Debug: Log if we can't find a child that should exist
                Debug.LogWarning($"PlayerManager: Could not find child '{path}' in spawned player '{newPlayer.name}'. This might indicate an instantiation issue.");
            }
        }
        
        // Double-check that AudioSourceHolder exists (common issue)
        Transform audioSourceHolder = newPlayer.transform.Find("AudioSourceHolder");
        if (audioSourceHolder == null)
        {
            // Try to find it with different casing or as direct child
            foreach (Transform child in newPlayer.transform)
            {
                if (child.name == "AudioSourceHolder" || 
                    (child.name.ToLower().Contains("audio") && child.name.ToLower().Contains("holder")))
                {
                    audioSourceHolder = child;
                    break;
                }
            }
        }
    }

    void ReplaceModelWithPrefab(GameObject playerInstance)
    {
        if (modelPrefab == null || playerInstance == null) return;

        // Find a child named "model" (case-insensitive) or a child that contains "model" in its name
        Transform found = null;
        foreach (Transform t in playerInstance.GetComponentsInChildren<Transform>(true))
        {
            if (t == playerInstance.transform) continue;
            string n = t.name.ToLower();
            if (n == "model" || n == "modelroot" || n.Contains("model"))
            {
                found = t;
                break;
            }
        }

        if (found == null) return;

        Transform parent = found.parent;
        Vector3 localPos = found.localPosition;
        Quaternion localRot = found.localRotation;
        Vector3 localScale = found.localScale;
        int siblingIndex = found.GetSiblingIndex();

        // Destroy the old model
        Destroy(found.gameObject);

        // Instantiate the prefab as a child of the same parent
        GameObject newModel = Instantiate(modelPrefab, parent);
        newModel.name = found.name;

        // Restore local transform so it matches expected placement
        newModel.transform.localPosition = localPos;
        newModel.transform.localRotation = localRot;
        newModel.transform.localScale = localScale;
        newModel.transform.SetSiblingIndex(siblingIndex);
    }
    
    void EnablePlayerComponents(GameObject player)
    {
        // Enable all necessary components
        if (player.GetComponent<BoxCollider>() != null)
            player.GetComponent<BoxCollider>().enabled = true;
        if (player.GetComponent<CharacterController>() != null)
            player.GetComponent<CharacterController>().enabled = true;
        if (player.GetComponent<PcMovement>() != null)
            player.GetComponent<PcMovement>().enabled = true;
        if (player.GetComponent<PcShoot>() != null)
            player.GetComponent<PcShoot>().enabled = true;
        if (player.GetComponent<DodgeRoll>() != null)
            player.GetComponent<DodgeRoll>().enabled = true;
        if (player.GetComponent<HealthManager>() != null)
            player.GetComponent<HealthManager>().enabled = true;
        if (player.GetComponent<PlayerMovementParticles>() != null)
            player.GetComponent<PlayerMovementParticles>().enabled = true;
    }
    
    void ReplaceDeadPlayer(GameObject deadPlayer)
    {
        if (deadBodyPrefab == null) return;
        
        Vector3 deadPosition = deadPlayer.transform.position;
        Quaternion deadRotation = deadPlayer.transform.rotation;
        
        // Instantiate dead body at dead player's position
        GameObject deadBody = Instantiate(deadBodyPrefab, deadPosition, deadRotation);
        
        // Destroy the dead player
        Destroy(deadPlayer);
    }
    
    void UpdateAllPlayerReferences(GameObject newPlayer, GameObject oldPlayer)
    {
        currentPlayer = newPlayer;
        currentPlayerHealth = newPlayer.GetComponent<HealthManager>();

        // Update BossAI references
        if (bossAI != null)
        {
            bossAI.player = newPlayer;
            
            // Find playerMarker - it should be a child of the new player with the same structure as original
            // Since we instantiated, the playerMarker should exist with the same relative path
            GameObject playerMarker = null;
            
            // Try to find playerMarker by looking at the original player's structure
            if (originalPlayer != null && bossAI.playerMarker != null)
            {
                // Get the relative path from original player to playerMarker
                string relativePath = GetRelativePath(originalPlayer.transform, bossAI.playerMarker.transform);
                if (!string.IsNullOrEmpty(relativePath))
                {
                    Transform markerTransform = newPlayer.transform.Find(relativePath);
                    if (markerTransform != null)
                    {
                        playerMarker = markerTransform.gameObject;
                    }
                }
            }
            
            // Fallback: search by common names
            if (playerMarker == null)
            {
                Transform markerTransform = newPlayer.transform.Find("PlayerMarker");
                if (markerTransform == null)
                {
                    markerTransform = newPlayer.transform.Find("playerMarker");
                }
                if (markerTransform == null)
                {
                    // Search all children for a GameObject that might be the marker
                    foreach (Transform child in newPlayer.GetComponentsInChildren<Transform>())
                    {
                        if (child.name.Contains("Marker") || child.name.Contains("marker"))
                        {
                            markerTransform = child;
                            break;
                        }
                    }
                }
                if (markerTransform != null)
                {
                    playerMarker = markerTransform.gameObject;
                }
            }
            
            if (playerMarker != null)
            {
                bossAI.playerMarker = playerMarker;
            }

            StoreOriginalPlayerSettings(newPlayer);
            StoreOriginalChildStates(newPlayer);
            StoreOriginalMaterials(newPlayer);
        }
        
        // Update HealthManager references (boss's health manager)
        HealthManager bossHealth = boss?.GetComponent<HealthManager>();
        if (bossHealth != null)
        {
            bossHealth.player = newPlayer;
        }
        
        // Update LockOn scripts that target the player
        LockOn[] allLockOns = FindObjectsOfType<LockOn>();
        foreach (LockOn lockOn in allLockOns)
        {
            // If this LockOn was targeting the old player, update it
            if (lockOn.target != null && oldPlayer != null)
            {
                // Check if it was the old player or a child of the old player
                if (lockOn.target == oldPlayer || 
                    lockOn.target.transform.IsChildOf(oldPlayer.transform))
                {
                    // Find equivalent in new player
                    if (lockOn.target == oldPlayer)
                    {
                        lockOn.target = newPlayer;
                    }
                    else
                    {
                        // It's a child, find equivalent path
                        string relativePath = GetRelativePath(oldPlayer.transform, lockOn.target.transform);
                        if (!string.IsNullOrEmpty(relativePath))
                        {
                            Transform newTarget = newPlayer.transform.Find(relativePath);
                            if (newTarget != null)
                            {
                                lockOn.target = newTarget.gameObject;
                            }
                        }
                    }
                }
            }
        }
    }
    
    string GetRelativePath(Transform root, Transform target)
    {
        if (target == null || root == null) return "";
        if (target == root) return "";
        
        System.Collections.Generic.List<string> path = new System.Collections.Generic.List<string>();
        Transform current = target;
        
        while (current != null && current != root)
        {
            path.Insert(0, current.name);
            current = current.parent;
            
            // Safety check to prevent infinite loop
            if (current == null) return "";
        }
        
        if (current == root)
        {
            return string.Join("/", path);
        }
        
        return "";
    }
    
    IEnumerator GameOver()
    {
        // Stop boss
        if (bossAI != null)
        {
            bossAI.enabled = false;
        }
        
        // Show game over screen
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }
        
        yield return null;
    }
    
    public GameObject GetCurrentPlayer()
    {
        return currentPlayer;
    }
}

