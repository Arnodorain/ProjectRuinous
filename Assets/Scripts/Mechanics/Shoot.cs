using UnityEngine;

public class Shoot : MonoBehaviour
{
    SpriteRenderer sr;

    [SerializeField] private Vector2 initShotVelocity = Vector2.zero;

    [SerializeField] private Transform spawnPointRight;
    [SerializeField] private Transform spawnPointLeft;

    [SerializeField] private Projectile projectilePrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (initShotVelocity == Vector2.zero)
        {
            Debug.Log("Init Shot Velocity has been changed to default value");
            initShotVelocity.x = 7.0f;
        }

        if (!spawnPointLeft || !spawnPointRight || !projectilePrefab)
        {
            Debug.Log($"Please set default values on {gameObject.name}");
        }
    }

    public void Fire()
    {
        Projectile curProjectile;

        if (!sr.flipX) // Facing Right
        {
            curProjectile = Instantiate(projectilePrefab, spawnPointRight.position, spawnPointRight.rotation);
            curProjectile.SetVelocity(initShotVelocity);
        }
        else // Facing Left
        {
            curProjectile = Instantiate(projectilePrefab, spawnPointLeft.position, spawnPointLeft.rotation);

            Vector2 reversedVelocity = new Vector2(-initShotVelocity.x, initShotVelocity.y);
            curProjectile.SetVelocity(reversedVelocity);

            // Flip the projectile sprite to face left
            SpriteRenderer projSR = curProjectile.GetComponent<SpriteRenderer>();
            if (projSR != null)
            {
                projSR.flipX = true;
            }
        }
    }

}
