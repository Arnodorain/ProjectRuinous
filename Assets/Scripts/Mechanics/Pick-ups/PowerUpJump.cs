using UnityEngine;
using System.Collections;

public class PowerJumpBoost : MonoBehaviour
{
    [SerializeField] private float jumpBoost = 2f;
    [SerializeField] private float boostDuration = 3f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerControls pc = collision.gameObject.GetComponent<PlayerControls>();
            if (pc != null)
            {
                pc.StartCoroutine(BoostJump(pc));
            }

            Destroy(gameObject);
        }
    }

    private IEnumerator BoostJump(PlayerControls pc)
    {
        // Assuming the PlayerControls script has a jumpForce variable
        pc.jumpForce += jumpBoost;
        yield return new WaitForSeconds(boostDuration);
        pc.jumpForce -= jumpBoost;
    }
}