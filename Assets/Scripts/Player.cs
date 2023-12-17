using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //체력에 필요한 것들.
    public GameObject prfHpBar;
    public float nowHp;
    public float maxHp;
    RectTransform hpBar;
    Image nowHpBar;

    //파워바에 필요한 것들.
    public GameObject prfPowerBar;
    public static float nowPower = 0;
    public float maxPower;
    RectTransform powerBar;
    Image nowPowerBar;

    public GameObject canvas;

    public float height = 1.7f;

    new Rigidbody2D rigidbody2D;
    CapsuleCollider2D capsuleCollider2D;
    SpriteRenderer spriteRenderer;
    Animator animator;


    public float maxSpeed;
    public float jumpPower;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        hpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();
        nowHpBar = hpBar.transform.GetChild(0).GetComponent<Image>();

        powerBar = Instantiate(prfPowerBar, canvas.transform).GetComponent<RectTransform>();
        nowPowerBar = powerBar.transform.GetChild(0).GetComponent<Image>();
    }

    void Update()
    {
        Move();
        Jump();
        Sit();
        Attack();
        Hp();
        AttackPower();
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

        if(h > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if(h < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
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

    void Hp()
    {
        Vector2 hpBarPos = Camera.main.WorldToScreenPoint(new Vector2(transform.position.x, transform.position.y + height));
        hpBar.position = hpBarPos;
        nowHpBar.fillAmount = nowHp / maxHp;
    }

    void AttackPower()
    {
        Vector2 powerBarPos = Camera.main.WorldToScreenPoint(new Vector2(transform.position.x, transform.position.y + height + 0.3f));
        powerBar.position = powerBarPos;
        nowPowerBar.fillAmount = nowPower / maxPower;
        Arrow.speed = nowPowerBar.fillAmount * 30;
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
        if(Input.GetKey("s") && !animator.GetBool("isWalk"))
        {                        
            capsuleCollider2D.size = new Vector2(capsuleCollider2D.size.x, 2.0f);                        
            animator.SetBool("isSit", true);            
        }
        else
        {
            capsuleCollider2D.size = new Vector2(capsuleCollider2D.size.x, 2.8f);
            animator.SetBool("isSit", false);                        
        }

        if (animator.GetBool("isJump") || animator.GetBool("isDoubleJump") || animator.GetBool("isAttack"))
        {
            capsuleCollider2D.size = new Vector2(capsuleCollider2D.size.x, 2.8f);
            animator.SetBool("isSit", false);
        }
    }

    void Attack()
    {
        if (Input.GetMouseButton(1) && !animator.GetBool("isWalk"))
        {
            animator.SetBool("isAttack", true);
            nowPower += Time.deltaTime * 50;
        }
        else
        {
            nowPower = 0;
            animator.SetBool("isAttack", false);
        }

        if (animator.GetBool("isJump") || animator.GetBool("isDoubleJump"))
        {
            animator.SetBool("isAttack", false);
        }
    }
    

}
