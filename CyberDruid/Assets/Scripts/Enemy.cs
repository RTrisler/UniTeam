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

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void DealDamage(float damage)
    {
        healthBar.SetActive(true);
        health -= damage;
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
            Destroy(gameObject);
            Instantiate(lootDrop, transform.position, Quaternion.identity);
        }
    }

    private float CalculateHealthPercentage()
    {
        return health / maxHealth;
    }

}
