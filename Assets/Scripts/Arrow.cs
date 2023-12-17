using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Arrow : MonoBehaviour
{
    new Rigidbody2D rigidbody2D;

    public static float speed;
    private float crrent_speed;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        
    }

    private void Start()
    {
        crrent_speed = speed;
        Invoke("DestroyArrow", 5);
    }

    void Update()
    {
        if(crrent_speed > 0)
        {
            if (transform.rotation.y == 0)
            {
                transform.Translate(transform.right * crrent_speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(transform.right * (-1) * crrent_speed * Time.deltaTime);
            }
            crrent_speed -= Time.deltaTime * 7;
        }
        
        
    }

    void DestroyArrow()
    {
        Destroy(gameObject);
    }
}
