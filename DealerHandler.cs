using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DealerHandler : MonoBehaviour
{
    public GameObject centerCamera;
    public GameObject box;
    public GameObject canvas;

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI priceText;

    public AudioSource audioSource;
    public AudioClip[] audioClips;

    Animator animatorBox;

    Vector3 originalPosition;
    Quaternion originalRotation;
    Vector3 originalScale;

    List<DealerItem> dealerItems;

    bool buttonFlag = true;


    void InitializeItems()
    {
        dealerItems = new List<DealerItem>
        {
            new DealerItem("Sword", "A sharp blade perfect for close combat."),
            new DealerItem("Shield", "Provides excellent defense against attacks."),
            new DealerItem("Potion", "Restores health when consumed."),
            new DealerItem("Armor", "Protects the wearer from physical damage."),
            new DealerItem("Magic Ring", "Grants the wearer enhanced magical abilities.")
        };
    }

    void DisplayDealerItem()
    {
        int randomIndex = Random.Range(0, dealerItems.Count);
        DealerItem selectedItem = dealerItems[randomIndex];

        int randomPrice = Random.Range(1, 6);

        titleText.text = selectedItem.Title;
        priceText.text = $"{randomPrice}";
        descriptionText.text = selectedItem.Description;
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
        box.transform.position = originalPosition;
        box.transform.rotation = originalRotation;
        box.transform.localScale = originalScale;

        animatorBox.Rebind();
        animatorBox.Update(0f);

        canvas.SetActive(false);

        centerCamera.SetActive(true);
        this.gameObject.SetActive(false);
    }

    void Start() 
    {
        animatorBox = box.GetComponent<Animator>();

        originalPosition = box.transform.position;
        originalRotation = box.transform.rotation;
        originalScale = box.transform.localScale;

        InitializeItems();
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
    public string Description;

    public DealerItem(string title, string description)
    {
        Title = title;
        Description = description;
    }
}
