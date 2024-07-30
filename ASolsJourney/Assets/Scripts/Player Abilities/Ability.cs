using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public string abilityName;
    public float cooldownTime;
    public float activeTime;
    public KeyCode key;

    public Sprite icon;

    public enum AbilityState {
        Ready,
        Active,
        Cooldown,
    }
    public AbilityState state = AbilityState.Ready;

    public enum AbilityType {
            Default,
            Alchemical,
    }

    public AbilityType type;

    public GameObject prefab; 
    public GameObject instance; 

    public virtual void Activate(GameObject parent)
    {
        
    }

    // void Update() {
    //     if(!isUnlocked) return;

    //     switch(state)
    //     {
    //         case AbilityState.Ready: 
    //             if(Input.GetKeyDown(key)) 
    //             {
    //                 Activate(gameObject);
    //                 state = AbilityState.Active;
    //                 activeTimer = activeTime;
    //             }
    //         break;
    //         case AbilityState.Active: 
    //             if(activeTimer > 0) {
    //                 activeTimer -= Time.deltaTime;
    //             }
    //             else {
    //                 state = AbilityState.Cooldown;
    //                 cooldownTimer = cooldownTime;
    //             }
    //         break;
    //         case AbilityState.Cooldown: 
    //             if(cooldownTimer > 0) {
    //                 cooldownTimer -= Time.deltaTime;
    //             }
    //             else {
    //                 state = AbilityState.Ready;
    //             }
    //         break;
    //     }
    // }
}
