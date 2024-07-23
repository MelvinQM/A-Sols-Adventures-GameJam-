using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ranged Weapon Item Data", menuName = "Item/Ranged WeaponItem Data")]
public class RangedWeaponItemData : ItemData
{
    [Header("Ranged Weapon Data")]
    public float FireRate;
    public GameObject ProjectilePrefab;
    public ItemData ProjectileItemData;

    public void Fire (Vector3 spawnPosition, Quaternion spawnRotation, Character.Team team)
    {

    }

}

