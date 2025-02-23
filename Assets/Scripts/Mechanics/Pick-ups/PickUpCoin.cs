using UnityEngine;

public class PickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Check if the player touches the pickup
        {
            Destroy(gameObject); // Destroy the pickup
        }
    }
}
