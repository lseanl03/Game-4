using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : EnemyBase
{
    [Header("Slime")]
    [Space(10)]
    public bool canAttack = true;
    private float initialMoveSpeed;
    public int IncreaseMaxHealth;
    public int quantityStateChange;
    public float delayAttack;
    public float checkPointRadius;
    public Transform checkPoint;
    private CircleCollider2D circleCollider;
    protected override void Awake()
    {
        base.Awake();
        circleCollider = GetComponent<CircleCollider2D>();
    }
    protected override void Start()
    {
        base.Start();
        initialMoveSpeed = moveSpeed;
        Run();
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

            Move();
            AutoChangeState();
            CheckTarget();
        }
    }
    protected override void Move()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }
    protected override void AutoChangeState()
    {
        if (skeletonAnimation.AnimationState.GetCurrent(0).IsComplete)
        {
            if (skeletonAnimation.AnimationState.GetCurrent(0).Animation == takeDamage.Animation)
            {
                Run();
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDie) return;

        if (collision.collider.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        if (collision.collider.CompareTag("Player"))
        {
            rb2d.bodyType = RigidbodyType2D.Static;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            rb2d.bodyType = RigidbodyType2D.Dynamic;
        }
    }
    void CheckTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(checkPoint.position, checkPointRadius);
        foreach (Collider2D collider in colliders)
        {
            PlayerController player = collider.GetComponent<PlayerController>();
            if (player != null)
            {
                if (canAttack) StartCoroutine(Attack());
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(checkPoint.position, checkPointRadius);
    }
    IEnumerator Attack()
    {
        canAttack = false;
        moveSpeed = 0;
        Idle();
        yield return new WaitForSeconds(delayAttack);
        skeletonAnimation.AnimationState.SetAnimation(0, attack, false);
        float duration = attack.Animation.Duration;
        yield return new WaitForSeconds(duration/3f);
        StartCoroutine(HandleAttack());
    }
    IEnumerator HandleAttack()
    {
        Idle();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(checkPoint.position, checkPointRadius);
        foreach (Collider2D collider in colliders)
        {
            PlayerController player = collider.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
        yield return new WaitForSeconds(0.5f);
        moveSpeed = initialMoveSpeed;
        skeletonAnimation.AnimationState.SetAnimation(0, run, true);
        canAttack = true;
    }
    public override void TakeDamage(int value)
    {
        currentHealth -= value;

        skeletonAnimation.state.SetAnimation(0, takeDamage, false);
        PoolManager.instance.SpawnPopupDamage(popupDamagePoint, value);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            if(CanChangeState()) ChangeState();
            else Die();
        }
        SetHealthText();
    }
    bool CanChangeState()
    {
        return currentHealth == 0 && quantityStateChange > 0;
    }
    void ChangeState()
    {
        moveSpeed = 0;
        float IndexIncrease = 0.5f;
        transform.DOScale(transform.localScale.x + IndexIncrease, 0.5f).OnComplete(() =>
        {
            quantityStateChange--;
            Run();
            moveSpeed = initialMoveSpeed;
            checkPointRadius += IndexIncrease/2;
            maxHealth += IncreaseMaxHealth;
            currentHealth = maxHealth;
            SetHealthText();
        });
    }
    protected override void Die()
    {
        circleCollider.enabled = false;
        base.Die();
    }
}
