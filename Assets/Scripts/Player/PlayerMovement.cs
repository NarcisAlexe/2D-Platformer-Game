using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header ("-------Movement Parameters-------")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpingPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [Header("-------Audio Parameters-------")]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip dashSound;

    private BoxCollider2D boxCollider;
    private Rigidbody2D body;
    private Animator anim;
    private float horizontalInput;
    private bool doubleJump;

    [Header("-------Dashing Parameters-------")]
    [SerializeField] private TrailRenderer trailRend;
    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashingPower = 24f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (isDashing) 
        {
            return;
        }

        horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        //Flip player when moving left-right
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        if (IsGrounded() && !Input.GetKey(KeyCode.Space))
        {
            doubleJump = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded() || doubleJump)
            {
                SoundManager.instance.PlaySound(jumpSound);
                Jump();

                doubleJump = !doubleJump;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0f) 
        {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y * 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash) 
        {
            StartCoroutine(Dash());
        }

        if (OnWall())
            body.velocity = new Vector2(0, body.velocity.y);

        //Set animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", IsGrounded());
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpingPower);
        anim.SetTrigger("jump");
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool OnWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && IsGrounded();
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        //Store the original gravity of the player
        float originalGravity = body.gravityScale;

        //Start the dash
        body.gravityScale = 0;
        anim.SetBool("dash", true);
        SoundManager.instance.PlaySound(dashSound);
        body.velocity = new Vector3(transform.localScale.x * dashingPower, 0, 0);
        trailRend.emitting = true;
        yield return new WaitForSeconds(dashingTime);

        //Stop the dashing
        anim.SetBool("dash", false);
        trailRend.emitting = false;

        //Reset the player gravity
        body.gravityScale = originalGravity;

        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
