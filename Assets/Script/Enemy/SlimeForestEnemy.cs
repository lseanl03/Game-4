using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeForestEnemy : EnemyBase
{
    [Header("Slime Forest")]
    [Space(10)]
    public float knockTime;
    public float explosionForce;
    public float explosionRadius;
    public Transform explosionPoint;
    public GameObject explosionSlimePrefab;
    private BoxCollider2D boxCollider2D;
    protected override void Awake()
    {
        base.Awake();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    protected override void Start()
    {
        base.Start();
        skeletonAnimation.AnimationState.SetAnimation(0, run, true);
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
            Move();
        }
    }
    protected override void AutoChangeState()
    {
        if (skeletonAnimation.AnimationState.GetCurrent(0).IsComplete)
        {
            Run();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDie) return;

        if (collision.collider.CompareTag("Wall"))
        {
            Flip();
        }
        if (collision.collider.CompareTag("Player"))
        {
            Die();
        }
    }
    protected override void Die()
    {
        boxCollider2D.enabled = false;
        rb2d.bodyType = RigidbodyType2D.Static;
        base.Die();
    }
    protected override void HandleDie()
    {       
        if (skeletonAnimation.AnimationState.GetCurrent(0).IsComplete)
        {
            CreateExplosion();
            Destroy(gameObject);
        }
    }
    public void CreateExplosion()
    {
        Instantiate(explosionSlimePrefab, explosionPoint.position, Quaternion.identity);
        CameraShake.instance.Shake(2, 0.25f);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPoint.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            PlayerController player = collider.GetComponent<PlayerController>();
            if (player != null)
            {
                Thrust(player, collider);
            }
        }
    }
    public void Thrust(PlayerController player, Collider2D collider)
    {
        Vector2 direction = collider.transform.position - explosionPoint.position;
        player.TakeDamage(damage);
        player.Knockback(knockTime);
        collider.GetComponent<Rigidbody2D>().AddForce(direction * explosionForce, ForceMode2D.Impulse);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(explosionPoint.position, explosionRadius);
    }
}
