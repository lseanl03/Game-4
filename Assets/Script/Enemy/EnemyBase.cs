using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("Base")]
    [Space(10)]
    public bool isDie;
    public bool isFacingRight;
    public int cost;

    [Header("Move")]
    public float moveSpeed;

    [Header("Health")]
    public int maxHealth;
    public int currentHealth;
    public TextMeshPro healthText;

    [Header("Attack")]
    public int damage;
    public float countTime;
    public float attackTime;
    public Transform firePoint;

    [Header("Animation")]
    public AnimationReferenceAsset idle;
    public AnimationReferenceAsset run;
    public AnimationReferenceAsset attack;
    public AnimationReferenceAsset takeDamage;
    public AnimationReferenceAsset die;

    [Header("Component")]
    public Transform popupDamagePoint;
    protected SkeletonAnimation skeletonAnimation;
    protected Rigidbody2D rb2d;
    protected GameManager gameManager => GameManager.instance;
    protected virtual void Awake()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        rb2d = GetComponent<Rigidbody2D>();
    }
    protected virtual void Start()
    {
        SetIniHealth();
        SetHealthText();
    }
    public void SetIniHealth()
    {
        currentHealth = maxHealth;
    }
    protected virtual void SetHealthText()
    {
        healthText.text = currentHealth + " / " + maxHealth;
    }
    protected virtual void Move()
    {
        float speed = isFacingRight ? moveSpeed : -moveSpeed;
        rb2d.velocity = new Vector2(speed, 0f);
    }
    public void Idle()
    {
        skeletonAnimation.state.SetAnimation(0, idle, true);
    }
    public void Run()
    {
        skeletonAnimation.state.SetAnimation(0, run, true);
    }
    protected virtual void Die()
    {
        isDie = true;
        skeletonAnimation.state.SetAnimation(0, die, false);
    }
    protected virtual void HandleDie()
    {
        if (skeletonAnimation.AnimationState.GetCurrent(0).IsComplete && gameObject != null)
        {
            Destroy(gameObject);
        }
    }
    protected virtual void AutoChangeState() { }
    public virtual void TakeDamage(int value)
    {
        currentHealth -= value;

        skeletonAnimation.state.SetAnimation(0, takeDamage, false);
        PoolManager.instance.SpawnPopupDamage(popupDamagePoint, value);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        SetHealthText();
    }
    protected virtual void Flip()
    {
        isFacingRight = !isFacingRight;

        //flip obj
        Vector2 newEnemyScale = transform.localScale;
        newEnemyScale.x *= -1f;
        transform.localScale = newEnemyScale;

        //flip health text
        Vector2 newHealthTextScale = healthText.transform.localScale;
        newHealthTextScale.x *= -1f;
        healthText.transform.localScale = newHealthTextScale;
    }
    protected void OnDestroy()
    {
        GameManager.instance.UpdateScore(cost);
    }
}
