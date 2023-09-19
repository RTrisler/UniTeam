using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpell : MonoBehaviour
{

    public GameObject projectile;
    public float minDamage;
    public float maxDamage;
    public float projectileForce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if right click then instantiate projectile
        if (Input.GetMouseButtonDown(1))
        {
            GameObject spell = Instantiate(projectile, transform.position, Quaternion.identity);
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 myPos = transform.position;
            Vector2 direction = (mousePos - myPos).normalized;
            spell.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
            spell.GetComponent<TestProjectile>().damage = Random.Range(minDamage, maxDamage);
        }
    }
}
