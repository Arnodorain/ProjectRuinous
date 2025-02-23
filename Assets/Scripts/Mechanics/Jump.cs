using UnityEngine;

public class Jump : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerControls pc;
    Animator animator; // Reference to Animator

    [SerializeField, Range(2, 5)] private float jumpHeight = 5;
    [SerializeField, Range(1, 5)] private float jumpFallForce = 20;

    float timeHeld;
    float maxHoldTime = 0.5f;
    float jumpInputTime = 0.0f;
    float calculatedJumpForce;

    public bool jumpCancelled = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PlayerControls>();
        animator = GetComponent<Animator>(); // Get Animator component

        calculatedJumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rb.gravityScale));
    }

    void Update()
    {
        if (pc.isGrounded)
        {
            jumpCancelled = false;
            animator.SetBool("isFalling", false); // Stop falling animation when grounded
        }

        if (Input.GetButton("Jump"))
        {
            jumpInputTime = Time.time;
            timeHeld += Time.deltaTime;
        }

        if (Input.GetButtonUp("Jump"))
        {
            timeHeld = 0;
            jumpInputTime = 0;

            if (rb.linearVelocity.y < -10) return;
            jumpCancelled = true;
        }

        if (jumpInputTime != 0 && (jumpInputTime + timeHeld) < (jumpInputTime + maxHoldTime))
        {
            if (pc.isGrounded)
            {
                rb.linearVelocity = Vector2.zero;
                rb.AddForce(new Vector2(0, calculatedJumpForce), ForceMode2D.Impulse);
                animator.SetTrigger("Jump"); // Play jump animation
            }
        }

        // Detect Falling Animation
        if (rb.linearVelocity.y < -0.1f && !pc.isGrounded)
        {
            animator.SetBool("isFalling", true); // Play falling animation
        }

        if (jumpCancelled)
        {
            rb.AddForce(Vector2.down * jumpFallForce);
        }
    }
}

