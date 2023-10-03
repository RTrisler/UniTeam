using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public InvetoryObject inventory;
    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();
        if (item)
        {
            inventory.AddItem(item.item, 1,item.item.weight);
            Destroy(other.gameObject);
        }
    }
    //private void OnApplicationQuit()
    //{
        //inventory.BackPack.Clear();
    //}
}
