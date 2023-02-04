using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy4_attack : MonoBehaviour
{
    public float AttackSpeed;
    public Transform targetPoint;


    Rigidbody2D rb;
    float timer = 0;
    Vector2 startAttackPos;
    Vector2 endAttackPos;

    bool attackActive = false;
    bool readyFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(attackActive)
        {
            timer += Time.deltaTime;
            if(timer >= 0.85 && !readyFlag)
            {
                startAttackPos = transform.position;
                endAttackPos = targetPoint.position;

                readyFlag = true;
            }
            if(timer >= 1)
            {
                timer = 0;
                attackActive = false;

                rb.velocity = (endAttackPos - startAttackPos) * AttackSpeed;

                Destroy(gameObject, 5f);
            }
                
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        attackActive = true;
    }
}
