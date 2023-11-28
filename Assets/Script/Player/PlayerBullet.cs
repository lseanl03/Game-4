using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int damage;
    public float speed;
    public GameObject explosionPrefab;
    private Rigidbody2D rb2d;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        rb2d.velocity = new Vector2(speed, 0f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Wall"))
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            explosion.transform.parent = transform.parent;
            gameObject.SetActive(false);
        }
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyBase>().TakeDamage(damage);
        }
    }
}
