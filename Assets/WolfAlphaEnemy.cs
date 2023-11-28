using DG.Tweening.Core.Easing;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class WolfAlphaEnemy : EnemyBase
{
    [Header("Wolf Alpha")]
    [Space(10)]
    public bool canSpecialAttack;
    public bool usedSpecialAttack;
    public AnimationReferenceAsset specialAttack;
    private BoxCollider2D boxCollider;
    public WolfAlphaBullet wolfAlphaBulletPrefab;
    public WolfAlphaSpecialBullet wolfAlphaSpecialBulletPrefab;
    private GameObject enemyBulletHolder;

    protected override void Awake()
    {
        base.Awake();
        boxCollider = GetComponent<BoxCollider2D>();
        enemyBulletHolder = GameObject.FindGameObjectWithTag("EnemyBulletHolder");
    }
    protected override void Start()
    {
        base.Start();
        attackTime = Random.Range(attackTime - 0.5f, attackTime + 0.5f);
        skeletonAnimation.timeScale = 2;
        skeletonAnimation.AnimationState.SetAnimation(0, idle, true);
    }
    private void Update()
    {
        if (isDie)
        {
            HandleDie();
        }
        else
        {
            if (gameManager.player.isDie) return;

            AutoChangeState();
            CountShootTime();
            ActiveSpecialShoot(currentHealth, maxHealth);
        }
    }
    protected override void AutoChangeState()
    {
        if (skeletonAnimation.AnimationState.GetCurrent(0).IsComplete)
        {
            Idle();
        }
    }
    public void CountShootTime()
    {
        if (countTime < attackTime) countTime += Time.deltaTime;
        else Attack();
    }
    public IEnumerator SpecialShoot()
    {
        countTime = 0;
        skeletonAnimation.state.SetAnimation(0, specialAttack, false);
        float duration = attack.Animation.Duration;
        yield return new WaitForSeconds(duration / 2f);
        var prefab = Instantiate(wolfAlphaSpecialBulletPrefab, firePoint);
        prefab.transform.SetParent(enemyBulletHolder.transform);
        usedSpecialAttack = true;
        canSpecialAttack = false;
    }
    public void Attack()
    {
        if (!canSpecialAttack) StartCoroutine(Shoot());
        else StartCoroutine(SpecialShoot());
    }
    public void ActiveSpecialShoot(int currentHealth, int maxHealth)
    {
        if (!usedSpecialAttack)
        {
            if (maxHealth / currentHealth >= 2)
            {
                canSpecialAttack = true;
            }
        }
    }
    public IEnumerator Shoot()
    {
        countTime = 0;
        skeletonAnimation.state.SetAnimation(0, attack, false);
        float duration = attack.Animation.Duration;
        yield return new WaitForSeconds(duration / (skeletonAnimation.timeScale * 2f));
        var prefab = Instantiate(wolfAlphaBulletPrefab, firePoint);
        prefab.transform.SetParent(enemyBulletHolder.transform);
    }
    protected override void Die()
    {
        boxCollider.enabled = false;
        base.Die();
    }
}
