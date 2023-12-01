using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public bool isSelected;
    public EnemyType enemyType;
    public string enemyName;
    public int health;
    public int damage;
    public int killPoint;
    [TextArea] public string description;
    public SkeletonGraphic skeletonGraphic;
    public EnemyInfoController enemyInfoController;

    public void OnPointerClick(PointerEventData eventData)
    {
        enemyInfoController.SetDesEnemy(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(skeletonGraphic.timeScale != 1)
        {
            skeletonGraphic.timeScale = 1;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (skeletonGraphic.timeScale != 0 && !isSelected)
        {
            skeletonGraphic.timeScale = 0;
        }
    }

    private void Awake()
    {
        skeletonGraphic = transform.GetComponentInChildren<SkeletonGraphic>();
    }
}
