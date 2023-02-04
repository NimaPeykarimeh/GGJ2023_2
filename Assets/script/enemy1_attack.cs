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
    public float rotation;
    bool canAttackFlag = true;
    char rotationDirection;
    Transform targetPoint;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        rotation = rigidbody.rotation;
        targetPoint = GameObject.FindGameObjectWithTag("Player").transform;
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
            rigidbody.rotation = rotation;
            canAttackFlag = true;
        }
    }

    private void Update()
    {

        

        if (!canAttackFlag)
        {

            if (rotationDirection == 'L')
                rigidbody.rotation += rotationDegree * Time.deltaTime;
            else
                rigidbody.rotation -= rotationDegree * Time.deltaTime;
        }
        if (rigidbody.velocity.y < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
        if (canAttackFlag)
        {
            if (transform.position.x - targetPoint.position.x > 0)
            {
                transform.localScale = new Vector3(1, -1, 1);
            }
            if (transform.position.x - targetPoint.position.x < 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}
