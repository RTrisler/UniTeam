using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{

    public static PlayerStats playerStats;

    public GameObject player;


    public float health;
    public float maxHealth;
    public TextMeshProUGUI healthText;
    public Slider healthSlider;

    public float specialCharge;
    public float maxSpecialCharge;
    public Slider specialSlider;

    public int coins;
    public TextMeshProUGUI currencyText;

    void Awake()
    {
        if (playerStats != null)
        {
            Destroy(playerStats);
        }
        else
        {
            playerStats = this;
        }
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        specialCharge = 0f;
        SetHealthUI();
    }

    #region Health
    private void SetHealthUI()
    {
        healthSlider.value = CalculateHealthPercentage();
        healthText.text = Mathf.Ceil(health).ToString() + " / " + Mathf.Ceil(maxHealth).ToString();
    }

    public void DealDamage(float damage)
    {
        health -= damage;
        CheckDeath();
        SetHealthUI();
    }

    public void HealCharacter(float heal)
    {
        health += heal;
        CheckOverheal();
        SetHealthUI();
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
            health = 0;
            Destroy(player);
        }
    }

    private float CalculateHealthPercentage()
    {
        return (health / maxHealth);
    }

    #endregion

    #region Special

    private void SetSpecialUI()
    {
        specialSlider.value = CalculateSpecialPercentage();
    }

    private float CalculateSpecialPercentage()
    {
        return (specialCharge / maxSpecialCharge);
    }

    public void AddCharge(float charge)
    {
        specialCharge += charge;
        CheckOverCharge();
        SetSpecialUI();
    }

    public void ResetCharge()
    {
        specialCharge = 0f;
        SetSpecialUI();
    }

    private void CheckOverCharge()
    {
        if (specialCharge >= maxSpecialCharge)
        {
            specialCharge = maxSpecialCharge;
            Debug.Log("special: " + specialCharge);
        }
    } 

    #endregion

    #region Currency
    public void AddCurrency(CurrencyPickup currency)
    {
        coins += currency.pickupValue;
        currencyText.text = coins.ToString();
    }
    #endregion
}
