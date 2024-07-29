using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class AbilityStatus
{
    public Ability ability;
    public bool isUnlocked;
    public float cooldownTimer;
    public float activeTimer;
    public bool isCooldownUIActive;

}
public class AbilityManager : MonoBehaviour
{
    [SerializeField] private List<AbilityStatus> unlockedAbilities;
    [SerializeField] private AbilityBar ui;

    // public UnityEvent OnAbilitiesUpdated = new UnityEvent();
    // public UnityEvent OnAbilitiesUsed = new UnityEvent();

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
                    }
                    else {
                        status.ability.state = Ability.AbilityState.Cooldown;
                        status.cooldownTimer = status.ability.cooldownTime;
                    }
                break;
                case Ability.AbilityState.Cooldown: 
                    if(status.cooldownTimer > 0) {
                        //OnAbilitiesUpdated.Invoke();
                        status.cooldownTimer -= Time.deltaTime;
                        if (!status.isCooldownUIActive) {
                            ui.StartCooldown(status.ability.abilityName, status.ability.cooldownTime);
                            status.isCooldownUIActive = true;
                        }
                    }
                    else {
                        status.ability.state = Ability.AbilityState.Ready;
                        status.isCooldownUIActive = false;
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

