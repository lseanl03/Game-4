using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth;
    public int currentHealth;
    public HealthBar healthBar;
    public void GetHealth(int value)
    {
        currentHealth = value;
        maxHealth = currentHealth;
    }
    public void SetHealthBar()
    {
        if (healthBar == null) return;
        for (int i = 0; i < healthBar.healthList.Count; i++)
        {
            bool show = currentHealth > i ? true : false;
            healthBar.healthList[i].SetActive(show);
        }
    }
}
