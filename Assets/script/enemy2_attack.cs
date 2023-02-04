using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy2_attack : MonoBehaviour
{
    public float throwPower;
    public Transform targetPoint;
    public Transform sourcePoint;
    public Rigidbody2D rbObject;

    Vector2 startDragPos;
    Vector2 endDragPos;
    Vector2 _velocity;

    Vector2 startPos;
    Vector2 endPos;


    bool isShooting = false;
    bool canAttackFlag = false;
    bool reloadFlag = false;
    bool stopAttackFlag = false;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        startPos = rbObject.position;
        endPos = sourcePoint.position;


        startDragPos = sourcePoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (canAttackFlag)
        {
            timer += Time.deltaTime;
            rbObject.position = Vector2.Lerp(startPos, endPos, timer);

            if (timer >= 1)
            {
                timer = 0;
                canAttackFlag = false;

                endDragPos = targetPoint.position;
                _velocity = (endDragPos - startDragPos) * throwPower;
                rbObject.velocity = _velocity;

                reloadFlag = true;
            }
        }
        if (reloadFlag)
        {
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                timer = 0;
                reloadFlag = false;
                rbObject.velocity = Vector2.zero;
                rbObject.position = startPos;
                isShooting = false;
                if (!stopAttackFlag)
                    canAttackFlag = true;

                startPos = rbObject.position;
                endPos = sourcePoint.position;
                startDragPos = sourcePoint.position;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {   
            targetPoint = collision.gameObject.transform;
            if (!isShooting)
            {
                rbObject.velocity = Vector2.zero;

                canAttackFlag = true;
                isShooting = true;
                stopAttackFlag = false;
            }
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
