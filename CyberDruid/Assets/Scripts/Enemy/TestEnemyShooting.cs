using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyShooting : EnemyAttack
{

    public GameObject projectile;

    public float minDamage;
    public float maxDamage;
    public float projectileForce;
    public float cooldown;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        StartCoroutine(ShootPlayer());
    }

    IEnumerator ShootPlayer()
    { 
        yield return new WaitForSeconds(cooldown);
        if (player != null)
        {
            GameObject spell = Instantiate(projectile, transform.position, Quaternion.identity);
            Vector2 myPos = transform.position;
            Vector2 targetPos = player.transform.position;
            Vector2 direction = (targetPos - myPos).normalized;
            spell.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
            spell.GetComponent<TestEnemyProjectile>().damage = Random.Range(minDamage, maxDamage);
            StartCoroutine(ShootPlayer());
        }
    }
}
