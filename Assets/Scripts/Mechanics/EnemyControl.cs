using UnityEngine;

public class EnemyControls : MonoBehaviour
{
    public float speed = 3.0f;
    public Transform pointA;
    public Transform pointB;

    private Transform target;

    void Start()
    {
        target = pointA;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            target = (target == pointA) ? pointB : pointA;
            Flip();
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerControls player = collision.gameObject.GetComponent<PlayerControls>();

            if (player != null)
            {
                player.Die();
            }
        }
        else if (collision.gameObject.CompareTag("Projectile")) // ✅ Check if hit by projectile
        {
            Destroy(collision.gameObject); // Destroy projectile
            Destroy(gameObject); // Destroy enemy
        }
    }
}
