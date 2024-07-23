using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEquipItem : EquipItem
{
    [SerializeField] private Transform muzzle;
    [SerializeField] private AudioClip shootSFX;
    private float lastAttackTime;
    public override void OnUse ()
    {
        RangedWeaponItemData i = item as RangedWeaponItemData;
        if(Time.time - lastAttackTime < i.FireRate)
        return;

        // Return if we don't have a Projectile in our Inventory

        lastAttackTime = Time.time;

        // Spawn the Projectile
        // Remove the Projectile from the Inventory
        // Play the sound effect
    }

}
