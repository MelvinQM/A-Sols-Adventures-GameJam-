using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AbilityStatus
{
    public Ability ability;
    public bool isUnlocked;
}
public class AbilityManager : MonoBehaviour
{

    private float activeTimer;
    private float cooldownTimer;

    [SerializeField] private List<AbilityStatus> unlockedAbilities;
    [SerializeField] private AbilityBar ui;

    public List<AbilityStatus> GetAbilities()
    {
        return unlockedAbilities;
    }

    void Update() {
        foreach(AbilityStatus status in unlockedAbilities) {
            if(!status.isUnlocked) return;

            switch(status.ability.state)
            {
                case Ability.AbilityState.Ready: 
                    if(Input.GetKeyDown(status.ability.key)) 
                    {
                        status.ability.Activate(gameObject);
                        status.ability.state = Ability.AbilityState.Active;
                        activeTimer = status.ability.activeTime;
                    }
                break;
                case Ability.AbilityState.Active: 
                    if(activeTimer > 0) {
                        activeTimer -= Time.deltaTime;
                    }
                    else {
                        status.ability.state = Ability.AbilityState.Cooldown;
                        cooldownTimer = status.ability.cooldownTime;
                    }
                break;
                case Ability.AbilityState.Cooldown: 
                    if(cooldownTimer > 0) {
                        cooldownTimer -= Time.deltaTime;
                    }
                    else {
                        status.ability.state = Ability.AbilityState.Ready;
                    }
                break;
            }
        }
    }

    public void AddAbility(Ability ability, bool isUnlocked)
    {
        if (!unlockedAbilities.Exists(a => a.ability == ability))
        {
            unlockedAbilities.Add(new AbilityStatus { ability = ability, isUnlocked = isUnlocked });
        } else {
            AbilityStatus abilityStatus = unlockedAbilities.Find(a => a.ability == ability);
            if (abilityStatus != null)
            {
                abilityStatus.isUnlocked = isUnlocked;
            }
        }

        // Update UI
        ui.UpdateUI();
    }

    public void DeleteAbility(Ability ability)
    {
        unlockedAbilities.RemoveAll(a => a.ability == ability);
    }

    public bool IsAbilityUnlocked(Ability ability)
    {
        AbilityStatus abilityStatus = unlockedAbilities.Find(a => a.ability == ability);
        return abilityStatus != null && abilityStatus.isUnlocked;
    }
}

