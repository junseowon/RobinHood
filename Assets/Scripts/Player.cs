using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    new Rigidbody2D rigidbody2D;
    new CapsuleCollider2D collider2D;
    SpriteRenderer spriteRenderer;
    Animator animator;

    public float maxSpeed;
    public float jumpPower;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        collider2D = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        Move();
        Jump();
        Sit();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        rigidbody2D.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if(rigidbody2D.velocity.x > maxSpeed) //오른쪽 최대 스피드
        {
            rigidbody2D.velocity = new Vector2(maxSpeed, rigidbody2D.velocity.y);
        }
        else if (rigidbody2D.velocity.x < maxSpeed * (-1)) //왼쪽 최대 스피드
        {
            rigidbody2D.velocity = new Vector2(maxSpeed * (-1), rigidbody2D.velocity.y);
        }

        if(rigidbody2D.velocity.y < 0)
        {
            RaycastHit2D raycastHit2D = Physics2D.Raycast(rigidbody2D.position, Vector3.down, 2, LayerMask.GetMask("Platform"));

            if (raycastHit2D.collider != null)
            {
                if (raycastHit2D.distance < 1.7f)
                {
                    animator.SetBool("isJump", false);
                    animator.SetBool("isDoubleJump", false);
                }
            }
        }
    }

    void Jump()
    {
        //점프
        if (Input.GetButtonDown("Jump") && !animator.GetBool("isJump"))
        {
            rigidbody2D.AddForce(Vector2.up* jumpPower, ForceMode2D.Impulse);
            animator.SetBool("isJump", true);
        }
        else if (Input.GetButtonDown("Jump") && animator.GetBool("isJump") && !animator.GetBool("isDoubleJump"))
        {
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetBool("isDoubleJump", true);
        }
    }

    void Move()
    {
        //속력 멈춤 제어
        if (Input.GetButtonUp("Horizontal"))
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.normalized.x * 0.5f, rigidbody2D.velocity.y);
        }

        //방향 전환
        if (Input.GetButtonDown("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

        if (rigidbody2D.velocity.normalized.x == 0)
        {
            animator.SetBool("isWalk", false);
        }
        else
        {
            animator.SetBool("isWalk", true);
        }
    }

    void Sit()
    {
        
    }

}
