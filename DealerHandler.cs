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
            new DealerItem("Een schaap", "Als een moslim kaart geavctiveerd wordt dan, wordt deze kaart automatisch gespeeld. De moslims zijn nu afgeleid door het schaap en de kaart heeft nu geen effect. (mocro's, turken en vluchtelingen vallen natuurlijk onder moslims)"),
            new DealerItem("Shotgun", "Wanneer een andere speler loopt, mag je dit item gebruiken om hem een extra vakje te laten lopen."),
            new DealerItem("Container cocaine", "Item gaat niet uit je inventory. Beschermt eigenaar tegen de effect \"belasting incasso\" door de belastingdienst om te kopen met cocaine."),
            new DealerItem("NRG-1 capsules", "Gebruik deze pillen voordat je dobbelt. NRG-1 is zware drugs die je bloeddruk verhoogt, waardoor je 5 extra stappen zet. Maar let op: na gebruik krijg je hyperthermie en moet je je volgende beurt overslaan."),
            new DealerItem("Cannabis", "Gebruik deze joint voordat je dobbelt. Cannabis helpt je te ontspannen, waardoor je nu te high ben om te dobbelen. Waaneer je bij komt zie je dat je bij de eerst volgende buyout bent beland. Maar pas op: door de bijwerkingen ben je versuft en heb je een -3 op je volgende worp."),
            new DealerItem("Amfetamine", "Neem deze pillen voordat je dobbelt. Amfetamine geeft je een energieboost, waardoor je niet dobbelt maar direct 10 stappen vooruit mag. Maar pas op: door de crash die volgt heb je een gebroken been, dit kost je 2eu aan zorg kosten en sla je volgende beurt over."),
            new DealerItem("LSD", "Geef de drugs aan een andere speler voordat hij dobbelt. LSD opent zijn geest en verstoort zijn waarneming, waardoor hij deze beurt in tegengestelde richting loopt.")
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
