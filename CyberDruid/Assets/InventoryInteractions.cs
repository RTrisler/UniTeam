using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInteractions : MonoBehaviour
{
    // Start is called before the first frame update
    public InvetoryObject inventory;
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("We hit the trigger fucker");
        var item = other.GetComponent<Item>();
        if (item)
        {
            Debug.Log("Adding item to inventory");

            inventory.AddItem(item.item, 1,item.item.weight);
            Destroy(other.gameObject);
        }
    }
    //private void OnApplicationQuit()
    //{
        //inventory.BackPack.Clear();
    //}
}
