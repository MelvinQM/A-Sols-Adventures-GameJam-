using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class AbilityStatus
{
    public Ability ability;
    public bool isUnlocked;
    [HideInInspector] public float cooldownTimer;
    [HideInInspector] public float activeTimer;
    [HideInInspector] public bool isUIActive;

}
public class AbilityManager : MonoBehaviour
{
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
                        status.activeTimer = status.ability.activeTime;
                    }
                break;
                case Ability.AbilityState.Active: 
                    if(status.activeTimer > 0) {
                        status.activeTimer -= Time.deltaTime;
                        if (!status.isUIActive) 
                        {
                            ui.SetIconActive(status.ability.abilityName, status.activeTimer, status.ability.type);
                            status.isUIActive = true;
                        }
                    }
                    else {
                        status.ability.state = Ability.AbilityState.Cooldown;
                        status.cooldownTimer = status.ability.cooldownTime;
                        status.isUIActive = false;
                    }
                break;
                case Ability.AbilityState.Cooldown: 
                    if(status.cooldownTimer > 0) {
                        status.cooldownTimer -= Time.deltaTime;
                        if (!status.isUIActive && status.ability.type == Ability.AbilityType.Default) { // For now alchemical has no cooldown animation
                            ui.StartCooldown(status.ability.abilityName, status.ability.cooldownTime);
                            status.isUIActive = true;
                        }
                    }
                    else {
                        status.ability.state = Ability.AbilityState.Ready;
                        status.isUIActive = false;
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

        ui.UpdateUI();
    }

    public void DeleteAbility(Ability ability)
    {
        unlockedAbilities.RemoveAll(a => a.ability == ability);

        ui.UpdateUI();
    }

    public bool IsAbilityUnlocked(Ability ability)
    {
        AbilityStatus abilityStatus = unlockedAbilities.Find(a => a.ability == ability);
        return abilityStatus != null && abilityStatus.isUnlocked;
    }
}

