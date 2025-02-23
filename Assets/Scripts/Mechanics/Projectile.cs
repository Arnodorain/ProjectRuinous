using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    [SerializeField, Range(1, 20)] private float lifetime = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        rb.gravityScale = 0; // Disable gravity so it moves in a straight line
        Destroy(gameObject, lifetime);
    }

    public void SetVelocity(Vector2 velocity)
    {
        GetComponent<Rigidbody2D>().linearVelocity = velocity;
    }
    public void Flip()
    {
        if (sr != null)
        {
            sr.flipX = !sr.flipX;
        }
    }

    // Made sure the projectile gets destory when colliding with an object outside of the player
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pickup"))
        {
            Destroy(gameObject);
        }
    }
}
