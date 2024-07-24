using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public float cooldownTime;
    private float cooldownTimer;
    public float activeTime;
    private float activeTimer;
    public bool isUnlocked;
    public KeyCode key;

    enum AbilityState {
        Ready,
        Active,
        Cooldown,
    }
    AbilityState state = AbilityState.Ready;

    public virtual void Activate(GameObject parent)
    {
        
    }

    void Update() {
        if(!isUnlocked) return;

        switch(state)
        {
            case AbilityState.Ready: 
                if(Input.GetKeyDown(key)) 
                {
                    Activate(gameObject);
                    state = AbilityState.Active;
                    activeTimer = activeTime;
                }
            break;
            case AbilityState.Active: 
                if(activeTimer > 0) {
                    activeTimer -= Time.deltaTime;
                }
                else {
                    state = AbilityState.Cooldown;
                    cooldownTimer = cooldownTime;
                }
            break;
            case AbilityState.Cooldown: 
                if(cooldownTimer > 0) {
                    cooldownTimer -= Time.deltaTime;
                }
                else {
                    state = AbilityState.Ready;
                }
            break;
        }
    }
}
