using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfoController : MonoBehaviour
{
    public List<EnemyBase> enemyPrefabList = new List<EnemyBase>();
    public List<EnemyInfo> enemyInfoList = new List<EnemyInfo>();
    public TabDescriptionEnemy tabDescriptionEnemy;
    public void Start()
    {
        GetEnemyInfo();
        SetDesEnemy(enemyInfoList[0]);
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            SetDesEnemy(enemyInfoList[0]);
        }
    }
    void GetEnemyInfo()
    {
        foreach (EnemyBase enemyBase in enemyPrefabList)
        {
            foreach (EnemyInfo enemyInfo in enemyInfoList)
            {
                if (enemyBase.enemyType == enemyInfo.enemyType)
                {
                    enemyInfo.enemyName = enemyBase.enemyName;
                    enemyInfo.description = enemyBase.description;
                    enemyInfo.health = enemyBase.maxHealth;
                    enemyInfo.damage = enemyBase.damage;
                    enemyInfo.killPoint = enemyBase.cost;
                }
            }
        }
    }
    public void SetDesEnemy(EnemyInfo enemyInfo)
    {
        tabDescriptionEnemy.SetName(enemyInfo.enemyName);
        tabDescriptionEnemy.SetDescription(enemyInfo.description);
        tabDescriptionEnemy.SetDamage(enemyInfo.damage);
        tabDescriptionEnemy.SetHealth(enemyInfo.health);
        tabDescriptionEnemy.SetSkillPoint(enemyInfo.killPoint);

        enemyInfo.isSelected = true;
        enemyInfo.skeletonGraphic.timeScale = 1;
        foreach(EnemyInfo enemy in enemyInfoList)
        {
            if(enemy.enemyType != enemyInfo.enemyType)
            {
                enemy.isSelected = false;
                enemy.skeletonGraphic.timeScale = 0;
            }
        }
    }
}
