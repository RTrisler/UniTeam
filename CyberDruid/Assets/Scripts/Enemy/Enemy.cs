using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Enemy : MonoBehaviour
{

    public GameObject enemyPrefab;
    public int cost;
    public float health;
    public float maxHealth;
    public GameObject healthBar;
    public Slider healthBarSlider;
    public GameObject lootDrop;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void DealDamage(float damage)
    {
        healthBar.SetActive(true);
        health -= damage;
        animator.Play("Damage", -1, 0f);
        CheckDeath();
        healthBarSlider.value = CalculateHealthPercentage();
    }

    public void HealCharacter(float heal)
    {
        health += heal;
        CheckOverheal();
        healthBarSlider.value = CalculateHealthPercentage();
    }

    public void CheckOverheal()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void CheckDeath()
    {
        if (health <= 0)
        {
            animator.SetTrigger("Death");
        }
    }

    public void DestoryEnemy()
    {
        Instantiate(lootDrop, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private float CalculateHealthPercentage()
    {
        return health / maxHealth;
    }

}
