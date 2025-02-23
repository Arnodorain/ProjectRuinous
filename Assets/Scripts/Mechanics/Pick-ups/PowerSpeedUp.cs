using UnityEngine;
using System.Collections;

public class PowerSpeedUp : MonoBehaviour
{
    [SerializeField] private float speedBoost = 5f;
    [SerializeField] private float boostDuration = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerControls pc = collision.gameObject.GetComponent<PlayerControls>();
            if (pc != null)
            {
                pc.StartCoroutine(BoostSpeed(pc));
            }

            Destroy(gameObject);
        }
    }

    private IEnumerator BoostSpeed(PlayerControls pc)
    {
        pc.speed += speedBoost;
        yield return new WaitForSeconds(boostDuration);
        pc.speed -= speedBoost;
    }
}
