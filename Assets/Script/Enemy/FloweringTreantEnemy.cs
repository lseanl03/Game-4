using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloweringTreantEnemy : EnemyBase
{
    [Header("Flowering Treant")]
    [Space(10)]
    public float knockTime;
    public float stunTime;
    public float thrustForce;
    public Transform checkPoint;
    private BoxCollider2D boxCollider2D;

    protected override void Awake()
    {
        base.Awake();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    protected override void Start()
    {
        base.Start();
        Idle();
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
        }
    }
    protected override void AutoChangeState()
    {
        if (skeletonAnimation.AnimationState.GetCurrent(0).IsComplete)
        {
            if (skeletonAnimation.AnimationState.GetCurrent(0).Animation == takeDamage.Animation)
            {
                Idle();
            }
        }
    }
    protected override void SetHealthText()
    {
        healthText.text = "∞ / ∞";
    }
    protected override void Move()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
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
            PlayerController player = collision.collider.GetComponent<PlayerController>();
            if (player != null) player.Knockback(knockTime);

            Vector2 direction = collision.collider.transform.position - transform.position;
            collision.collider.GetComponent<Rigidbody2D>().AddForce(direction * thrustForce, ForceMode2D.Impulse);
            collision.collider.GetComponent<PlayerEffect>().Stun(stunTime);
            boxCollider2D.isTrigger = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
    protected override void Die()
    {
        boxCollider2D.enabled = false;
        base.Die();
    }
}
