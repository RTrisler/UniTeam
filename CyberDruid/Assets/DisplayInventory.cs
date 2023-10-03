using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayInventory : MonoBehaviour
{
    public InvetoryObject inventory;

    public int XSpace;
    public int XStart;
    public int YStart;
    public int YSpace;
    public int NumOfColumns;
    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        CreateDisplay();
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }
    public void UpdateDisplay()
    {
        for(int i =0;i < inventory.BackPack.Count; i++)
        {
            if (itemsDisplayed.ContainsKey(inventory.BackPack[i]))
            {
                itemsDisplayed[inventory.BackPack[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.BackPack[i].weight.ToString("n0");
                itemsDisplayed[inventory.BackPack[i]].transform.Find("AmountText").GetComponent<TextMeshProUGUI>().text = inventory.BackPack[i].amount.ToString("n0");
            }
            else
            {
                var obj = Instantiate(inventory.BackPack[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.BackPack[i].weight.ToString("n0");
                obj.transform.Find("AmountText").GetComponent<TextMeshProUGUI>().text = inventory.BackPack[i].amount.ToString("n0");
                itemsDisplayed.Add(inventory.BackPack[i], obj);
            }
        }
    }

    public void CreateDisplay()
    {
        for(int i=0; i< inventory.BackPack.Count; i++)
        {
            var obj = Instantiate(inventory.BackPack[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.BackPack[i].weight.ToString("n0");
            obj.transform.Find("AmountText").GetComponent<TextMeshProUGUI>().text = inventory.BackPack[i].amount.ToString("n0");
            itemsDisplayed.Add(inventory.BackPack[i], obj);
        }
    }
    public Vector3  GetPosition(int i)
    {
        return new Vector3(XStart +(XSpace * (i % NumOfColumns)),YStart +((-YSpace * (i / NumOfColumns))), 0f);
    }
}
