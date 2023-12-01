using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TabDescriptionEnemy : MonoBehaviour
{
    public TextMeshProUGUI enemyName;
    public TextMeshProUGUI description;
    public TextMeshProUGUI health;
    public TextMeshProUGUI damage;
    public TextMeshProUGUI killPoint;
    public void SetName(string value)
    {
        enemyName.text = "Name: " + value;
    }
    public void SetDescription(string value)
    {
        description.text = "Mô tả: " + value;
    }
    public void SetHealth(int value)
    {
        health.text = "Health: " + value;
    }
    public void SetDamage(int value)
    {
        damage.text = "Damage: " + value;
    }
    public void SetSkillPoint(int value)
    {
        killPoint.text = "Kill Points: " + value;
    }
}
