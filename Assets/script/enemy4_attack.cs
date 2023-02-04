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
    [SerializeField] GameObject expoParticle;
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

                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.velocity = (endAttackPos - startAttackPos) * AttackSpeed;

                Destroy(gameObject, 5f);
            }
                
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            targetPoint = collision.transform;
            attackActive = true;
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            expoParticle.GetComponent<ParticleSystem>().Play();
            SpriteRenderer _sprtr = GetComponent<SpriteRenderer>();
            _sprtr.enabled = false;
            Destroy(gameObject,0.3f);
        }
    }
}
