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
        UpdateUI();
    }

    public void UpdateUI()
    {
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
}
