using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsContent : TabContent
{
    [Header("Speed")]
    public float speedUpgradeValue;
    public Button speedUpgradeButton;
    public Slider speedSlider;
    public TextMeshProUGUI speedUpgradeCostText;

    [Header("Rate Fire")]
    public float rateFireUpgradeValue;
    public Button rateFireUpgradeButton;
    public Slider rateFireSlider;
    public TextMeshProUGUI rateFireUpgradeCostText;

    [Header("Health")]
    public int healthUpgradeValue;
    public Button healthUpgradeButton;
    public Slider healthSlider;
    public TextMeshProUGUI healthUpgradeCostText;
    [Header("Attack")]
    public int attackUpgradeValue;
    public Button attackUpgradeButton;
    public Slider attackSlider;
    public TextMeshProUGUI attackUpgradeCostText;

    private AxieManager axieManager => AxieManager.instance;
    private GameManager gameManager => GameManager.instance;

    public void Awake()
    {
        speedUpgradeButton.onClick.AddListener(UpdateSpeed);
        rateFireUpgradeButton.onClick.AddListener(UpdateRateFire);
        healthUpgradeButton.onClick.AddListener(UpdateHealth);
        attackUpgradeButton.onClick.AddListener(UpdateAttack);
    }
    public void UpdateSpeed()
    {
        if (speedSlider.value >= speedSlider.maxValue) return;
        if(axieManager.upgradePoints >= axieManager.axieData.speedCost)
        {
            speedSlider.value += speedUpgradeValue;
            axieManager.currentAxie.speed = speedSlider.value;
            axieManager.currentAxie.TakeEffect();
            axieManager.UpgradePoint(-axieManager.axieData.speedCost);
            axieManager.UpdateCurrentAxieData(axieManager.currentAxie);
        }
        else
        {
            Debug.Log("khong du upgrade points");
        }
    }
    public void UpdateRateFire()
    {
        if (rateFireSlider.value >= rateFireSlider.maxValue) return;
        if(axieManager.upgradePoints >= axieManager.axieData.rateFireCost)
        {
            rateFireSlider.value += rateFireUpgradeValue;
            axieManager.currentAxie.rateFire = Mathf.Abs(rateFireSlider.value);
            axieManager.currentAxie.TakeEffect();
            axieManager.UpgradePoint(-axieManager.axieData.rateFireCost);
            axieManager.UpdateCurrentAxieData(axieManager.currentAxie);
        }
        else
        {
            Debug.Log("khong du upgrade points");
        }
    }
    public void UpdateHealth()
    {
        if (healthSlider.value >= healthSlider.maxValue) return;
        if (axieManager.upgradePoints >= axieManager.axieData.healthCost)
        {
            healthSlider.value += healthUpgradeValue;
            axieManager.currentAxie.health = Mathf.FloorToInt(healthSlider.value);
            axieManager.currentAxie.TakeEffect();
            axieManager.UpgradePoint(-axieManager.axieData.healthCost);
            axieManager.UpdateCurrentAxieData(axieManager.currentAxie);
        }
        else
        {
            Debug.Log("khong du upgrade points");
        }
    }
    public void UpdateAttack()
    {
        if (attackSlider.value >= attackSlider.maxValue) return;
        if (axieManager.upgradePoints >= axieManager.axieData.attackCost)
        {
            attackSlider.value += attackUpgradeValue;
            axieManager.currentAxie.attackDamage = Mathf.FloorToInt(attackSlider.value);
            axieManager.currentAxie.TakeEffect();
            axieManager.UpgradePoint(-axieManager.axieData.attackCost);
            axieManager.UpdateCurrentAxieData(axieManager.currentAxie);
        }
        else
        {
            Debug.Log("khong du upgrade points");
        }

    }
    public void GetSpeed(float currentValue, float minValue, float maxValue, float upgradeValue)
    {
        speedSlider.minValue = minValue;
        speedSlider.maxValue = maxValue;
        speedSlider.value = currentValue;

        speedUpgradeValue = upgradeValue;
    }
    public void GetRateFire(float currentValue, float minValue, float maxValue, float upgradeValue)
    {
        rateFireSlider.minValue = minValue;
        rateFireSlider.maxValue = maxValue;
        rateFireSlider.value = currentValue;
        if (currentValue > 0) rateFireSlider.value = -currentValue;

        rateFireUpgradeValue = upgradeValue;
    }
    public void GetHealth(int currentValue, int minValue, int maxValue, int upgradeValue)
    {
        healthSlider.minValue = minValue;
        healthSlider.maxValue = maxValue;
        healthSlider.value = currentValue;

        healthUpgradeValue = upgradeValue;
    }
    public void GetAttack(int currentValue, int minValue, int maxValue, int upgradeValue)
    {
        attackSlider.minValue = minValue;
        attackSlider.maxValue = maxValue;
        attackSlider.value = currentValue;

        attackUpgradeValue = upgradeValue;
    }
    public void GetSpeedUpgradeCost(int value)
    {
        speedUpgradeCostText.text = "Needs " + value.ToString() + " upgrade points";
    }
    public void GetRateFireUpgradeCost(int value)
    {
        rateFireUpgradeCostText.text = "Needs " + value.ToString() + " upgrade points";
    }
    public void GetHealthUpgradeCost(int value)
    {
        healthUpgradeCostText.text = "Needs " + value.ToString() + " upgrade points";
    }
    public void GetAttackUpgradeCost(int value)
    {
        attackUpgradeCostText.text = "Needs " + value.ToString() + " upgrade points";
    }
}
