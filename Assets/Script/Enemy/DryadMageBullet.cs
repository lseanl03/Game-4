using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DryadMageBullet : MonoBehaviour
{
    public int damage;
    public float speed;
    public GameObject explosionPrefab;
    private Rigidbody2D rb2d;
    private Vector2 initPos;
    private Vector2 initScale;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        initScale = transform.localScale;
    }
    private void OnEnable()
    {
        initPos = transform.position;
        Vector3 playerPos = GameManager.instance.player.transform.position;
        playerPos.y += 0.5f;
        Vector2 direction = (playerPos - transform.position).normalized;
        rb2d.velocity = direction * speed;
    }
    private void OnDisable()
    {
        transform.localScale = initScale;
    }
    private void Update()
    {
        if(Vector2.Distance(transform.position, initPos) > 20f)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            explosion.transform.parent = transform.parent;
            gameObject.SetActive(false);

            collision.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }
}
