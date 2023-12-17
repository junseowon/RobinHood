using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Arrow : MonoBehaviour
{
    public static float speed;
    void Start()
    {
        Invoke("DestroyArrow", 2);
    }

    void Update()
    {
        if(transform.rotation.y == 0)
        {
            transform.Translate(transform.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(transform.right * (-1) * speed * Time.deltaTime);
        }
        
    }

    void DestroyArrow()
    {
        Destroy(gameObject);
    }
}
