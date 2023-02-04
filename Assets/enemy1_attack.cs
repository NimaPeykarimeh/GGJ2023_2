using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy1_attack : MonoBehaviour
{
    public Transform playerPos;
    public float jumpPower;
    public float rotationDegree;

    Rigidbody2D rigidbody;
    Vector2 startPos;
    bool canAttackFlag = true;
    char rotationDirection;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (canAttackFlag)
            {
                Vector2 targetVector = playerPos.position - transform.position;
                if (targetVector.x > 0)
                    rotationDirection = 'R';
                else
                    rotationDirection = 'L';

                targetVector.x *= 0.2f;
                
                rigidbody.bodyType = RigidbodyType2D.Dynamic;
                rigidbody.AddForce(targetVector * jumpPower, ForceMode2D.Impulse);
                canAttackFlag = false;
            }
        }
        else if (collision.gameObject.CompareTag("rePositionTrigger"))
        {
            rigidbody.velocity = Vector2.zero;
            rigidbody.bodyType = RigidbodyType2D.Kinematic;
            rigidbody.position = startPos;
            canAttackFlag = true;
            rigidbody.rotation = 0;
        }
    }

    private void Update()
    {
        if(!canAttackFlag)
        {
            if (rotationDirection == 'L')
                rigidbody.rotation += rotationDegree * Time.deltaTime;
            else
                rigidbody.rotation -= rotationDegree * Time.deltaTime;
        }
    }
}
