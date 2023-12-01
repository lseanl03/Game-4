using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AxieName
{
    None,
    Buba,
    Olek,
    Pomodoro,
    Momo,
}
[CreateAssetMenu(fileName = "AxieList", menuName = "AxieGame/Axie")]
public class AxieData : ScriptableObject
{
    [Header("Speed")]
    public int speedCost;
    public float speedUpgradeValue;
    public float minSpeed;
    public float maxSpeed;

    [Header("Rate Fire")]
    public int rateFireCost;
    public float rateFireUpgradeValue;
    public float minRateFire;
    public float maxRateFire;
    
    [Header("Health")]
    public int healthCost;
    public int healthUpgradeValue;
    public int minHealth;
    public int maxHealth;
    
    [Header("Attack")]
    public int attackCost;
    public int attackUpgradeValue;
    public int minAttack;
    public int maxAttack;

    public List<AxieBase> AxieList;
}
[System.Serializable]   
public class AxieBase
{
    public AxieName axieName;
    [TextArea] public string AxieDescribe;
    public float speed;
    public float rateFire;
    public int health;
    public int attackDamage;
    public AnimationReferenceAsset idle;
    public AnimationReferenceAsset run;
    public AnimationReferenceAsset move;
    public AnimationReferenceAsset attack;
    public AnimationReferenceAsset takeDamage;
    public AnimationReferenceAsset die;
    public AnimationReferenceAsset takeEffect;
    public SkeletonDataAsset skeletonDataAsset;
}
