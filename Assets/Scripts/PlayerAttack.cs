using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attacker;
    Animator animator;
    public GameObject arrow;
    public Transform pos;

    public float loadingTime;
    public float completeLoadTime;
    public bool isShoot = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (Input.GetMouseButton(1) && animator.GetBool("isAttack"))
        {
            isShoot = true;
            if (completeLoadTime <= 0)
            {                
                if (Input.GetMouseButton(0) && isShoot)
                {   
                    Instantiate(arrow, pos.position, transform.rotation);                
                    completeLoadTime = loadingTime;
                    Player.nowPower = 0;
                }                
            }
            completeLoadTime -= Time.deltaTime;
        }
        else
        {
            completeLoadTime = loadingTime;
            isShoot = false;
        }
    }
}
