using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    None,
    healing, 
    buffMoveSpeed,
    buffAttack,
    buffShied,
}
public class Item : MonoBehaviour
{
    public bool canGetItem;
    public bool preparingDisable;
    public float countTime;
    public float activeTime;
    public SpriteRenderer spriteRenderer;
    public ItemBase currentItem;
    public EffectData itemData;
    [Header("Effect")]
    public float buffSpeedMoveTime;
    public float buffAttackTime;
    public float buffShieldTime;
    protected GameManager gameManager => GameManager.instance;
    private void Start()
    {
        GameObject itemHolder = GameObject.FindGameObjectWithTag("ItemHolder");
        transform.SetParent(itemHolder.transform);
        GetRandomItem(itemData);
    }
    private void Update()
    {
        if (gameManager.player.isDie) return;
        if (countTime <= activeTime)
        {
            countTime += Time.deltaTime;
            if(countTime + 3 >= activeTime && !preparingDisable)
            {
                PrepareDisable();
            }
        }
        else
        {
            spriteRenderer.DOKill();
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (canGetItem)
            {
                PlayerEffect playerEffect= collision.GetComponent<PlayerEffect>();
                playerEffect.ApplyState(currentItem);
                spriteRenderer.DOKill();
                Destroy(gameObject);
            }
        }
    }
    public void GetRandomItem(EffectData itemData)
    {
        int randomIndex = Random.Range(0, itemData.itemList.Count);
        currentItem = itemData.itemList[randomIndex];
        spriteRenderer.sprite = currentItem.itemSprite;
    }
    void PrepareDisable()
    {
        preparingDisable = true;
        spriteRenderer.DOFade(0.25f, 0.25f).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.InExpo);
    }
}