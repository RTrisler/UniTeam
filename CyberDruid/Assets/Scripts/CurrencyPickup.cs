using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyPickup : MonoBehaviour
{
    public int pickupValue;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.name == "Player") {
            PlayerStats.playerStats.AddCurrency(this);
            Destroy(gameObject);
        }
    }
}
