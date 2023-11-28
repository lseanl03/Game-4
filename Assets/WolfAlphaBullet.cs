using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAlphaBullet : MonoBehaviour
{
    public int damage;
    public float speed;
    public GameObject explosionPrefab;
    private Rigidbody2D rb2d;
    private Vector2 initPos;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        initPos = transform.position;
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position, initPos) > 20f)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            explosion.transform.parent = transform.parent;
            Destroy(gameObject);
            collision.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }
}
