using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DealerHandler : MonoBehaviour
{
    [Header("Scene References")]
    public GameObject centerCamera;
    public GameObject box;
    public GameObject canvas;

    [Header("UI Elements")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI priceText;

    [Header("3D Display")]
    public GameObject imageRendererGameObject;
    Renderer displayRenderer;
    Animator imageAnimator;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    [Header("Assign Dealer Items")]
    public List<DealerItem> dealerItems;

    Animator animatorBox;
    Vector3 originalPosition;
    Quaternion originalRotation;
    Vector3 originalScale;

    bool buttonFlag = true;

    void Start()
    {
        animatorBox = box.GetComponent<Animator>();
        displayRenderer = imageRendererGameObject.GetComponent<Renderer>();
        imageAnimator = imageRendererGameObject.GetComponent<Animator>();

        originalPosition = box.transform.position;
        originalRotation = box.transform.rotation;
        originalScale = box.transform.localScale;
    }

    void DisplayDealerItem()
    {
        if (dealerItems == null || dealerItems.Count == 0)
        {
            Debug.LogWarning("Dealer items list is empty!");
            return;
        }

        int randomIndex = Random.Range(0, dealerItems.Count);
        DealerItem selectedItem = dealerItems[randomIndex];
        int randomPrice = Random.Range(1, 6);

        titleText.text = selectedItem.Title;
        priceText.text = $"{randomPrice}";
        descriptionText.text = selectedItem.Description;

        // === Texture tonen op 3D-object ===
        if (displayRenderer != null && selectedItem.Image != null)
        {
            displayRenderer.material.mainTexture = selectedItem.Image.texture;
            imageAnimator.SetBool("card_active", true);
        }
        else
        {
            Debug.LogWarning("DisplayRenderer of Sprite niet ingesteld!");
        }
    }

    public void OnDealerAnimationEvent()
    {
        buttonFlag = false;
        canvas.SetActive(true);
        DisplayDealerItem();
    }

    public void TriggerBoxAnimation()
    {
        animatorBox.SetTrigger("DealerAnimateBox");
        int randomClipIndex = Random.Range(0, audioClips.Length);
        audioSource.PlayOneShot(audioClips[randomClipIndex]);
    }

    public void ResetBox()
    {
        imageAnimator.SetBool("card_active", false);
        box.transform.position = originalPosition;
        box.transform.rotation = originalRotation;
        box.transform.localScale = originalScale;

        animatorBox.Rebind();
        animatorBox.Update(0f);

        canvas.SetActive(false);
        centerCamera.SetActive(true);

        this.gameObject.SetActive(false);
    }

    void Update()
    {
        if (buttonFlag)
        {
            ArduinoDataManager.Instance.ButtonBPressed = false;
            return;
        }
        else if (ArduinoDataManager.Instance.ButtonBPressed)
        {
            ArduinoDataManager.Instance.ButtonBPressed = false;
            buttonFlag = true;
            ResetBox();
        }
    }
}

[System.Serializable]
public class DealerItem
{
    public string Title;

    [TextArea(3, 10)]
    public string Description;

    public Sprite Image;

    public DealerItem(string title, string description, Sprite image)
    {
        Title = title;
        Description = description;
        Image = image;
    }
}