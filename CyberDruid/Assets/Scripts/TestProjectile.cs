using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectile : MonoBehaviour
{
    public float damage; 

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name != "Player")
        {
            if (collision.GetComponent<TestProjectile>() != null || collision.GetComponent<FloatToPlayer>() != null)
            {
                Debug.Log("Collided with own projectile");
                return;
            }

            if(collision.GetComponent<Enemy>() != null)
            {
                collision.GetComponent<Enemy>().DealDamage(damage);
                PlayerStats.playerStats.AddCharge(12.5f);
            }
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        Physics2D.IgnoreLayerCollision(9, 8);
        Physics2D.IgnoreLayerCollision(9, 9);
    }
}
