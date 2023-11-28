using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack")]
    public int damage;
    public float countTime;
    public float attackTime;

    public Transform firePoint;
    PlayerController playerController;
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    private void Start()
    {
        UIManager.instance.normalAttack.onClick.AddListener(Attack);
    }
    public void GetAttackTime(float value)
    {
        attackTime = value;

        countTime = attackTime;
    }
    public void GetDamage(int value)
    {
        damage = value;
    }
    public void CountShootTime()
    {
        if (countTime < attackTime) countTime += Time.deltaTime;
    }
    public bool CanShoot()
    {
        return countTime >= attackTime && !playerController.isDie && !playerController.playerEffect.isStun;
    }
    public IEnumerator Shoot()
    {
        countTime = 0;
        playerController.skeletonAnimation.state.SetAnimation(0, playerController.attack, false);
        float duration = playerController.attack.Animation.Duration;
        yield return new WaitForSeconds(duration / (playerController.skeletonAnimation.timeScale * 1.5f));
        PoolManager.instance.SpawnPlayerBullet(firePoint, damage);
    }
    public void Attack()
    {
        if (CanShoot())
        {
            StartCoroutine(Shoot());
        }
    }

}
