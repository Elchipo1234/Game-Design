using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator anim;
    private float moveInput;
    private bool facingRight = true;
    private int lastInput;

    [SerializeField] private Transform attackPoint;

    private bool attacking = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (attacking)
            return;
        // 1. Get Input (A/D or Left/Right Arrows)
        moveInput = Input.GetAxisRaw("Horizontal");
        if (moveInput != 0)
        {
            lastInput = (int)moveInput;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Attack");
        }

        // 2. Control Animations
        // If moveInput is not 0, the player is moving
        bool isMoving = moveInput != 0;
        anim.SetBool("isRunning", isMoving);
        anim.SetFloat("Speed", rb.linearVelocity.x);

        // 3. Flip the character based on direction
        if (moveInput > 0 && !facingRight) {
            Flip();
        } else if (moveInput < 0 && facingRight) {
            Flip();
        }
    }

    public void StartAttack()
    {
        rb.linearVelocity = Vector2.zero;
        attacking = true;
    }

    public void AttackHitCollider()
    {
        var attackHits = Physics2D.OverlapCircle(attackPoint.position, 0.2f, LayerMask.GetMask("Enemy"));
        
        // check damage script from projectile in Tower Defense or Top Down

        Debug.Log("Attacking");
    }

    public void EndAttack()
    {
        attacking = false;
    }
    

    void FixedUpdate()
    {
        if (attacking)
            return;
        // 4. Apply Physics Movement
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    void Flip()
    {
        // Toggle the facing direction and rotate the player transform
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
    
    
}