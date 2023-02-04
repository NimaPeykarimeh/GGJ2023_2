using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy5_attack : MonoBehaviour
{
    public float AttackSpeed;
    public Transform targetPoint;


    Rigidbody2D rb;
    float timer = 0;
    Vector2 startAttackPos;
    Vector2 endAttackPos;

    bool attackActive = false;
    bool readyFlag = false;
    bool reoladFlag = false;
    bool stopAttackFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attackActive)
        {
            timer += Time.deltaTime;
            if (timer >= 0.85 && !readyFlag)
            {
                startAttackPos = transform.position;
                endAttackPos = targetPoint.position;

                readyFlag = true;
            }
            if (timer >= 1)
            {
                timer = 0;
                attackActive = false;

                Vector2 _velocity = (endAttackPos - startAttackPos) * AttackSpeed;
                _velocity.y += 1;
                
                rb.velocity = _velocity;

                reoladFlag = true;
                readyFlag = false;
            }
        }
        if(reoladFlag)
        {
            timer += Time.deltaTime;
            if (timer >= 3f)
            {
                if (!stopAttackFlag)
                    attackActive = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            stopAttackFlag = false;
            attackActive = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            stopAttackFlag = true;
        }
    }
}
