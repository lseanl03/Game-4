using System.Collections;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int health = 100;
    public static Test instance;
    private Rigidbody2D rb2d;
    public float moveSpeed = 5;
    public bool canDash = true;
    public bool dashing = false;
    private float directionX = 0f;
    private Vector2 dashingDirection;
    public float dashingVelocity = 50;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        directionX = Input.GetAxisRaw("Horizontal");

        if (!dashing)
        {
            rb2d.velocity = new Vector2(directionX * moveSpeed, rb2d.velocity.y);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Dash();
        }
    }

    void Dash()
    {
        if (canDash)
        {
            StartCoroutine(DashCoroutine());
        }
    }

    IEnumerator DashCoroutine()
    {
        canDash = false;
        dashing = true;

        dashingDirection = new Vector2(directionX, 0f);

        if (dashingDirection == Vector2.zero)
        {
            dashingDirection = new Vector2(transform.localScale.x, 0f);
        }

        rb2d.velocity = dashingDirection * dashingVelocity;

        yield return new WaitForSeconds(0.2f);
        dashing = false;
        canDash = true;
    }
}
