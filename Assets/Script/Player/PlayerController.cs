using NUnit.Framework;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isDie;
    public bool isShield;
    public bool canMove = true;

    [Header("Move")]
    public float moveSpeed;

    [Header("Component")]
    public Transform popupDamagePoint;
    [SerializeField] private FixedJoystick fixedJoystick;


    [Header("Animation")]
    public AnimationReferenceAsset idle;
    public AnimationReferenceAsset run;
    public AnimationReferenceAsset move;
    public AnimationReferenceAsset attack;
    public AnimationReferenceAsset takeDamage;
    public AnimationReferenceAsset die;
    public AnimationReferenceAsset takeEffect;
    public SkeletonAnimation skeletonAnimation;
    
    public PlayerHealth playerHealth;
    public PlayerAttack playerAttack;
    public PlayerEffect playerEffect;
    private Rigidbody2D rb2d;
    protected GameManager gameManager => GameManager.instance;

    void Awake()
    {   
        playerHealth = GetComponent<PlayerHealth>();
        playerAttack = GetComponent<PlayerAttack>();
        playerEffect = GetComponent<PlayerEffect>();

        rb2d = GetComponent<Rigidbody2D>();
        skeletonAnimation = GetComponent<SkeletonAnimation>();
    }
    void Start()
    {
        Run();
    }
    void Update()
    {
        if (isDie)
        {
            HandleDie();
        }
        else
        {
            Control();
            AutoChangeState();

            playerAttack.CountShootTime();
            playerEffect.HandleEffectState();
            playerEffect.CountCancelStun();
        }

        if(gameManager.startedGame) playerHealth.SetHealthBar();
    }
    public void GetSkeletonDataAsset(SkeletonDataAsset skeletonDataAsset)
    {
        skeletonAnimation.skeletonDataAsset = skeletonDataAsset;
        skeletonAnimation.Initialize(true);
        skeletonAnimation.state.SetAnimation(0, move, false);
    }
    public void GetMoveSpeed(float value)
    {
        moveSpeed = value;
    }
    void AutoChangeState()
    {
        if (skeletonAnimation.AnimationState.GetCurrent(0).IsComplete && !isDie)
        {
            if (playerEffect.isStun)
            {
                Run();
            }
            else
            {
                if (rb2d.velocityX == 0 && rb2d.velocityY == 0)
                {
                    Run();
                }
                else if (rb2d.velocityX != 0 || rb2d.velocityY != 0)
                {
                    Move();
                }
            }
        }

    }
    public void TakeDamage(int value)
    {
        if (HaveShield()) return;

        playerHealth.currentHealth -= value;
        TakeDamage();
        PoolManager.instance.SpawnPopupDamage(popupDamagePoint, value);
        if (playerHealth.currentHealth <= 0)
        {
            playerHealth.currentHealth = 0;
            Die();
        }
    }
    public void Knockback(float knockTime)
    {
        StartCoroutine(KnockbackAction(knockTime));
    }
    public IEnumerator KnockbackAction(float knockTime)
    {
        canMove = false;
        yield return new WaitForSeconds(knockTime);
        canMove = true;
    }
    void Control()
    {
        if (!canMove) return;
        float horizontal = fixedJoystick.Horizontal;
        float vertical = fixedJoystick.Vertical;
        rb2d.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
    }
    void Run()
    {
        skeletonAnimation.timeScale= 2;
        skeletonAnimation.state.SetAnimation(0, run, true);
    }
    void Move()
    {
        skeletonAnimation.timeScale = 2;
        skeletonAnimation.state.SetAnimation(0, move, true);
    }
    public void TakeDamage()
    {
        skeletonAnimation.state.SetAnimation(0, takeDamage, false);
    }
    public void TakeEffect()
    {
        skeletonAnimation.state.SetAnimation(0, takeEffect, false);
    }
    void Die()
    {
        isDie = true;
        rb2d.velocity = Vector3.zero;
        skeletonAnimation.timeScale = 1;
        skeletonAnimation.state.SetAnimation(0, die, false);
    }
    void HandleDie()
    {
        float duration = die.Animation.Duration;
        float currentAnimationTime = skeletonAnimation.AnimationState.GetCurrent(0).AnimationTime;
        if (currentAnimationTime >= duration / 2)
        {
            skeletonAnimation.timeScale = 0;
            gameManager.time = 0;
        }
    }
    bool HaveShield()
    {
        if (isShield)
        {
            foreach(ItemBase item in playerEffect.itemList)
            {
                if(item.itemType == ItemType.buffShied)
                {
                    playerEffect.RemoveState(item);
                    return true;
                }
            }
        }
        return false;
    }
}
