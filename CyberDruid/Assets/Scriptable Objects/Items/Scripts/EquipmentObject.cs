using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]
public class EquipmentObject :ItemObject
{
    public float Attack;
    public float Defense;
    public void Awake()
    {
        type = ItemType.Equipment;
    }
}
