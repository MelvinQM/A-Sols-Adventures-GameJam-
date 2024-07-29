using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBar : MonoBehaviour
{
    [SerializeField] private AbilityManager manager;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Sprite defaultSprite;
    
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


        // Check for all abilities
        List<AbilityStatus> statuses = manager.GetAbilities();

        if(statuses == null) return;

        // Add all abilities to the bar
        foreach(AbilityStatus status in statuses)
        {
            GameObject instance = Instantiate(prefab, transform);
            Transform iconTransform = instance.transform.Find("Icon");
            Image image = iconTransform.GetComponentInChildren<Image>();

            instance.name = status.ability.abilityName;

            // Check which abilities are unlocked
            if(status.isUnlocked) {   
                image.sprite = status.ability.icon;
            } else {
                image.sprite = defaultSprite;
            }
        }
        

    }

    public void StartCooldown(string abilityName, float cooldown)
    {
        // Look for the correct ability ui prefab
        Transform dashTransform = transform.Find(abilityName);
        if(dashTransform == null) {
            Debug.Log("No Dash component found");
            return;
        }

        // Trigger cooldown animation using slider 
        Slider slider = dashTransform.gameObject.GetComponent<Slider>();
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
}
