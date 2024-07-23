using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Adds an option in the unity editor to create a default item
[CreateAssetMenu(fileName = "Item Data", menuName = "Item/Generic Item")]
public class ItemData : ScriptableObject
{
    public string DisplayName;
    public string Description;
    public Sprite Icon;
    public int MaxStackSize = 1;
    public GameObject EquipPrefab;
}
