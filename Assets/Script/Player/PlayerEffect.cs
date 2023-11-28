using DG.Tweening;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    public List<ItemBase> itemList = new List<ItemBase>();
    [Header("Effect")]
    public bool isStun;
    public bool isBuffShield;
    public bool isBuffAttack;
    public bool isBuffMoveSpeed;

    [Header("Stun")]
    public float countStunTime;
    public float stunTime;
    [Header("Healing")]
    public float countHealingTime;
    public float healingTime;
    [Header("BuffShield")]
    public float countBuffShieldTime;
    public float buffShieldTime;
    [Header("BuffAttack")]
    public float countBuffAttackTime;
    public float buffAttackTime;
    [Header("BuffMoveSpeed")]
    public float countBuffMoveSpeed;
    public float buffMoveSpeedTime;

    [Header("Effect")]
    [SerializeField] private GameObject stunEffect;
    [SerializeField] private GameObject healingEffect;
    [SerializeField] private GameObject BuffAtkEffect;
    [SerializeField] private GameObject BuffMoveSpeedEffect;
    [SerializeField] private GameObject BuffShieldEffect;

    private PlayerController playerController;
    private UIManager uIManager => UIManager.instance;
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    public void Stun(float time)
    {
        if (isStun) return;
        isStun = true;
        stunTime = time;
        stunEffect.SetActive(true);
        playerController.moveSpeed /= 2;
        playerController.skeletonAnimation.timeScale = 1;
    }

    public void CountCancelStun()
    {
        if (!isStun) return;
        countStunTime += Time.deltaTime;
        if (countStunTime >= stunTime)
        {
            countStunTime = 0;
            stunTime = 0;
            isStun = false;
            stunEffect.SetActive(false);
            playerController.moveSpeed *= 2;
            playerController.skeletonAnimation.timeScale = 2;
        }
    }
    public void ApplyState(ItemBase item)
    {
        playerController.TakeEffect();
        switch (item.itemType)
        {
            case ItemType.healing:
                healingTime = item.countDown;
                StartCoroutine(Healing(1));
                break;
            case ItemType.buffMoveSpeed:
                buffMoveSpeedTime = item.countDown;
                StartCoroutine(BuffMoveSpeed(2));
                uIManager.effectBar.BuffMoveSpeedState(true, buffMoveSpeedTime);
                break;
            case ItemType.buffAttack:
                buffAttackTime = item.countDown;
                StartCoroutine(BuffAttack(10));
                uIManager.effectBar.BuffAttackState(true, buffAttackTime);
                break;
            case ItemType.buffShied:
                buffShieldTime = item.countDown;
                StartCoroutine(BuffShied());
                uIManager.effectBar.BuffShieldState(true, buffShieldTime);
                break;
        }
        if (itemList.Contains(item)) return;
        itemList.Add(item);
    }
    public void RemoveState(ItemBase item)
    {
        switch (item.itemType)
        {
            case ItemType.buffMoveSpeed:
                isBuffMoveSpeed = false;
                playerController.moveSpeed -= 2;
                break;
            case ItemType.buffAttack:
                isBuffAttack = false;
                playerController.playerAttack.damage -= 10;
                break;
            case ItemType.buffShied:
                BuffShieldEffect.SetActive(false);

                isBuffShield = false;
                playerController.isShield = false;
                uIManager.effectBar.BuffShieldState(false, 0);
                break;
        }
        itemList.Remove(item);
    }
    public void HandleEffectState()
    {
        if (itemList.Count == 0) return;

        List<ItemBase> itemsToRemove = new List<ItemBase>();

        foreach (ItemBase item in itemList)
        {
            if (item.itemType == ItemType.healing)
            {
                healingTime -= Time.deltaTime;
                if (healingTime <= 0)
                {
                    itemsToRemove.Add(item);
                }
            }
            else if (item.itemType == ItemType.buffAttack)
            {
                buffAttackTime -= Time.deltaTime;
                if (buffAttackTime <= 0)
                {
                    itemsToRemove.Add(item);
                }
            }
            else if (item.itemType == ItemType.buffMoveSpeed)
            {
                if (isStun) return;
                buffMoveSpeedTime -= Time.deltaTime;
                if (buffMoveSpeedTime <= 0)
                {
                    itemsToRemove.Add(item);
                }
            }
            else if (item.itemType == ItemType.buffShied)
            {
                buffShieldTime -= Time.deltaTime;
                if (buffShieldTime <= 0)
                {
                    itemsToRemove.Add(item);
                }
            }
        }
        foreach (ItemBase itemToRemove in itemsToRemove)
        {
            RemoveState(itemToRemove);
        }
    }
    private IEnumerator Healing(int value)
    {
        playerController.playerHealth.currentHealth += value;
        if (playerController.playerHealth.currentHealth >= playerController.playerHealth.maxHealth)
        {
            playerController.playerHealth.currentHealth = playerController.playerHealth.maxHealth;
        }
        healingEffect.SetActive(true);
        yield return new WaitForSeconds(1);
        healingEffect.SetActive(false);

    }
    public IEnumerator BuffMoveSpeed(float value)
    {
        if (!isBuffMoveSpeed)
        {
            isBuffMoveSpeed = true;
            playerController.moveSpeed += value;
        }
        BuffMoveSpeedEffect.SetActive(true);
        yield return new WaitForSeconds(1);
        BuffMoveSpeedEffect.SetActive(false);
    }
    public IEnumerator BuffAttack(int value)
    {
        if (!isBuffAttack)
        {
            isBuffAttack = true;
            playerController.playerAttack.damage += value;
        }
        BuffAtkEffect.SetActive(true);
        yield return new WaitForSeconds(1);
        BuffAtkEffect.SetActive(false);
    }
    public IEnumerator BuffShied()
    {
        if (!isBuffShield)
        {
            isBuffShield = true;
            playerController.isShield = true;
        }
        BuffShieldEffect.SetActive(true);
        BuffShieldEffect.transform.localScale = Vector3.zero;
        BuffShieldEffect.transform.DOScale(1, 0.5f);
        yield return null;
    }
}
