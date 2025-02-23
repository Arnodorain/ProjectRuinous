using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
[RequireComponent(typeof(GroundCheck), typeof(Jump))]
public class PlayerControls : MonoBehaviour
{
    // Movement Variables
    [Range(3, 10)] public float speed = 5.0f;
    public float jumpForce = 8.0f;

    // Component References
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private GroundCheck gndChk;

    public bool isGrounded = false;
    private bool isAttacking = false;
    private bool isCrouching = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        gndChk = GetComponent<GroundCheck>();
    }

    void Update()
    {
        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);

        // Check if grounded
        CheckIsGrounded();

        float hInput = Input.GetAxis("Horizontal");

        if (curPlayingClips.Length > 0)
        {
            if (!(curPlayingClips[0].clip.name == "Attack"))
            {
                rb.linearVelocity = new Vector2(hInput * speed, rb.linearVelocity.y);
            }
        }

        // Allow movement only if not attacking
        if (!isAttacking && !isCrouching)
        {
            rb.linearVelocity = new Vector2(hInput * speed, rb.linearVelocity.y);
        }

        // Jumping logic
        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W)) && isGrounded && !isAttacking)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Attack animation
        if (Input.GetKeyDown(KeyCode.E) && !isAttacking)
        {
            isAttacking = true;
            anim.SetTrigger("Attack");
            rb.linearVelocity = Vector2.zero;
        }

        // Crouch animation
        if (Input.GetKey(KeyCode.S))
        {
            if (!isCrouching)
            {
                isCrouching = true;
                anim.Play("Crouch");
            }
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            isCrouching = false;
            anim.SetTrigger("Idle");
        }

        // Sprite flipping
        if (hInput > 0)
            sr.flipX = false;
        else if (hInput < 0)
            sr.flipX = true;

        // Update Animator parameters
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("speed", Mathf.Abs(hInput));
    }

    void CheckIsGrounded()
    {
        isGrounded = gndChk.isGrounded();
    }

    // Called by an Animation Event at the end of the Attack animation
    public void EndAttack()
    {
        isAttacking = false;
        anim.ResetTrigger("Attack");
        anim.SetTrigger("Idle");
    }

    public void Die()
    {
        Debug.Log("Player Died!");
        // Reload Scene (Respawn Player)
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}