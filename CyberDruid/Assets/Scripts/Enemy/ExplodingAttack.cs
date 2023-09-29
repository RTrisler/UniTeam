using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExplodingAttack : MonoBehaviour
{

    Collider2D[] inExplosionRadius = null;
    [SerializeField] private float ExplosionForceMultiplier = 5;
    [SerializeField] private float ExplosionRadius = 5;


    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Explode();
        }
    }

    void Explode()
    {
        Debug.Log("Explodin");
        inExplosionRadius = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius);

        foreach (Collider2D o in inExplosionRadius)
        {
            Rigidbody2D o_rigidbody = o.GetComponent<Rigidbody2D>();
            if (o_rigidbody != null)
            {
                Debug.Log("Rigidbody detected");
                Vector2 distanceVector = o.transform.position - transform.position;
                if (distanceVector.magnitude > 0)
                {
                    if (o_rigidbody.GetComponent<PlayerMovement>() != null)
                    {
                        PlayerStats.playerStats.DealDamage(25f);
                    }
                    Debug.Log("AddingForce");
                    float explosionForce = ExplosionForceMultiplier / distanceVector.magnitude;
                    Debug.Log("Forces going in: " + distanceVector.normalized * explosionForce);
                    o_rigidbody.AddForce(distanceVector.normalized * explosionForce);
                }
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
    }

}
