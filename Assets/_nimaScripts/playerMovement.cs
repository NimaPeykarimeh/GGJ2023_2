using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Rigidbody2D rb2;
    private float movingDirX;
    [SerializeField] private float movementSpeed;
    [SerializeField] private bool isGrounded;
    private bool _jump;
    [SerializeField] int jumpCounts;
    [SerializeField] private float jumpForce;
    [SerializeField] int jumpsLeft;
    [SerializeField] Color _jumpColor;
    [SerializeField] float glideSpeed;
    [SerializeField] bool isGlideActive;
    void Start()
    {
        rb2= GetComponent<Rigidbody2D>();
        jumpsLeft = jumpCounts;
        isGrounded = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpsLeft = jumpCounts;
            isGrounded = true;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            GetComponent<SpriteRenderer>().color = _jumpColor;
        }
    }


    private void FixedUpdate()
    {
        rb2.transform.position += new Vector3(movingDirX, 0, 0) * movementSpeed * Time.fixedDeltaTime;
        if (_jump)
        {
            rb2.velocity = new Vector2(rb2.velocity.x, 0f);
            rb2.AddForce(transform.up * jumpForce,ForceMode2D.Impulse);
            _jump = false;
            jumpsLeft --;
        }
    }

    void Update()
    {
        movingDirX = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && jumpsLeft > 0)
        {
            _jump = true;
        }
        if (Input.GetButton("Jump") && !isGrounded && rb2.velocity.y < -glideSpeed && isGlideActive)
        {
            rb2.velocity = new Vector2(rb2.velocity.x, -glideSpeed);
        }

    }
}
