using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBar : MonoBehaviour
{
    [SerializeField] private AbilityManager manager;
    //[SerializeField] private GameObject alchemicalAbilityPrefab;
    [Header("Default Ability UI")]
    [SerializeField] private GameObject defaultAbilityPrefab;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite defaultBorderSprite;
    [SerializeField] private Sprite defaultBorderActiveSprite;

    [Header("Alchemy Ability UI")]
    [SerializeField] private Sprite defaultAlchemySprite;
    [SerializeField] private Sprite alchemyBorderSprite;
    [SerializeField] private Sprite alchemyBorderActiveSprite;
    [SerializeField] private GameObject AlchemyBar;
    [SerializeField] private List<GameObject> alchemicalSlots;

    private Dictionary<string, GameObject> abilityUIElements = new Dictionary<string, GameObject>();

    void Start()
    {
        // manager.OnAbilitiesUpdated.AddListener(UpdateUI);
        UpdateUI();
    }

    public void UpdateUI()
    {
        // Remove old icons
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        
        //TODO: IS THIS NEEDED?
        // Clear old alchemical abilities
        // foreach (GameObject slot in alchemicalSlots)
        // {
        //     foreach (Transform child in slot.transform)
        //     {
        //         Destroy(child.gameObject);
        //     }
        // }

        // Check for all abilities
        List<AbilityStatus> statuses = manager.GetAbilities();

        if (statuses == null) return;

        int alchemicalCount = 0;

        // Add all abilities to the bar
        foreach (AbilityStatus status in statuses)
        {
            GameObject instance = null;
            Transform iconTransform;
            Image image;
            switch (status.ability.type)
            {
                case Ability.AbilityType.Default:
                    instance = Instantiate(defaultAbilityPrefab, transform);
                    iconTransform = instance.transform.Find("Icon"); // IK KAN HET NIET ZONDER FIND OKE  IM SORRY
                    image = iconTransform.GetComponent<Image>();
                    image.sprite = status.isUnlocked ? status.ability.icon : defaultSprite;
                    break;
                case Ability.AbilityType.Alchemical:
                    if (alchemicalCount < 3)
                    { 
                        instance = alchemicalSlots[alchemicalCount];
                        iconTransform = instance.transform.Find("Icon"); // IK KAN HET NIET ZONDER FIND OKE  IM SORRY
                        image = iconTransform.GetComponent<Image>();
                        image.sprite = status.isUnlocked ? status.ability.icon : defaultAlchemySprite;
                        alchemicalCount++;
                    }
                    break;
                default: break;
            }

            // Store the instance in the dictionary
            if (instance != null) abilityUIElements[status.ability.abilityName] = instance.gameObject;

        }
    }

    public void StartCooldown(string abilityName, float cooldown)
    {
        // Look for the correct ability UI prefab in the dictionary
        if(!abilityUIElements.TryGetValue(abilityName, out GameObject uiGameObject))
        {
            Debug.Log($"No {abilityName} component found");
            return;
        }


        // Trigger cooldown animation using slider 
        Slider slider = uiGameObject.gameObject.GetComponent<Slider>();
        if(slider == null) {
            Debug.Log("No slider found");
            return;
        }

        StartCoroutine(MoveSliderOverTime(cooldown, slider));
    }

    IEnumerator MoveSliderOverTime(float time, Slider slider)
    {
        float elapsedTime = 0f;
        float startValue = slider.maxValue;
        float endValue = slider.minValue;

        while (elapsedTime < time)
        {
            // Calculate the value based on the elapsed time
            slider.value = Mathf.Lerp(startValue, endValue, elapsedTime / time);
    
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure the slider reaches the exact end value
        slider.value = endValue;
    }

    public void SetIconActive(string abilityName, float cooldown, Ability.AbilityType type) 
    {
        // Look for the correct ability UI object in the dictionary
        if(!abilityUIElements.TryGetValue(abilityName, out GameObject uiGameObject))
        {
            Debug.Log($"No {abilityName} component found");
            return;
        }
        
        //TODO: REMOVE FINDS
        switch(type)
        {
            case Ability.AbilityType.Default:
                if (!uiGameObject.transform.Find("Border").TryGetComponent<Image>(out var defaultBorderImage)) 
                    Debug.LogWarning($"No Image component found on parent of {uiGameObject.name}");
                StartCoroutine(ActiveIconDuration(defaultBorderImage, cooldown, defaultBorderSprite, defaultBorderActiveSprite));
            break;
            case Ability.AbilityType.Alchemical:
                if (!uiGameObject.TryGetComponent<Image>(out var alchemicalBorderImage)) 
                    Debug.LogWarning($"No Image component found on parent of {uiGameObject.name}");
                StartCoroutine(ActiveIconDuration(alchemicalBorderImage, cooldown, alchemyBorderSprite, alchemyBorderActiveSprite));
            break;
        }


    }

    private IEnumerator ActiveIconDuration(Image border, float cooldown, Sprite inactive, Sprite active)
    {
        border.sprite = active;
        yield return new WaitForSeconds(cooldown);
        border.sprite = inactive;
    }

}
