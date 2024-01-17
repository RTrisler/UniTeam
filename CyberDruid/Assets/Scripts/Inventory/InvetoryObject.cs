using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName ="Inventory System/Inventory")]
public class InvetoryObject : ScriptableObject
{
    public List<InventorySlot> BackPack = new List<InventorySlot>();
    public InventorySlot SelectedItem;
    public float maxWeight = 100.0f;

    [SerializeField] private float currentWeight;
    public void CalculateCurrentWeight()
    {
        currentWeight = 0.0f;
        foreach (var slot in BackPack)
        {
            currentWeight += slot.weight;
        }
    }

    public float CurrentWeight
    {
        get { return currentWeight; }
    }

    public void AddItem(ItemObject _item, float _amount, float _weight)
    {
        if (CurrentWeight + (_amount * _weight) <= maxWeight)
        {
            bool hasItem = false;
            for (int i = 0; i < BackPack.Count; i++)
            {
                if (BackPack[i].item == _item)
                {
                    BackPack[i].AddAmount(_amount);
                    BackPack[i].AddWeight(_weight);
                    hasItem = true;
                    break;
                }
            }
            if (!hasItem)
            {
                BackPack.Add(new InventorySlot(_item, _amount, _weight));
            }
        }
        else
        {
            Debug.LogWarning("Item cannot be added. Weight limit exceeded.");
        }

        CalculateCurrentWeight();
    }

    public void RemoveItem()
    {
        /*
        for (int i = 0; i < BackPack.Count; i++)
        {
            if (BackPack[i].item == _item)
            {
                BackPack[i].AddAmount(_amount);
                BackPack[i].AddWeight(_weight);
                hasItem = true;
                break;
            }
        }
        */
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public float amount;
    public float weight;
    public InventorySlot(ItemObject _item,float _amount, float _weight)
    {
        item = _item;
        amount = _amount;
        weight = _weight;
    }
    public void AddAmount(float value)
    {
        amount += value;
    }
    public void AddWeight(float value)
    {
        weight += value;
    }
}