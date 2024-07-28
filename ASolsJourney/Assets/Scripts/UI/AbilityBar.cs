using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBar : MonoBehaviour
{
    [SerializeField] private AbilityManager manager;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Sprite defaultSprite;
    


    // Start is called before the first frame update
    void Start()
    {
        // Subscribe to the event
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
            Image image = instance.GetComponentInChildren<Image>();

            // Check which abilities are unlocked
            if(status.isUnlocked) {   
                image.sprite = status.ability.icon;
            } else {
                image.sprite = defaultSprite;
            }
        }
        

    }

    // IEnumerator MoveSliderOverTime()
    // {
    //     float elapsedTime = 0f;
    //     float startValue = slider.minValue;
    //     float endValue = slider.maxValue;

    //     while (elapsedTime < duration)
    //     {
    //         // Calculate the value based on the elapsed time
    //         slider.value = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            
    //         // Increment the elapsed time
    //         elapsedTime += Time.deltaTime;

    //         // Wait until the next frame
    //         yield return null;
    //     }

    //     // Ensure the slider reaches the exact end value at the end of the duration
    //     slider.value = endValue;
    // }
}
