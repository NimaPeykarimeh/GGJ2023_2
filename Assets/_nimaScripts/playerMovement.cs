using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    [Header("Movement Setting")]
    [SerializeField] public float movementSpeed;
    [SerializeField] int jumpCounts;
    [SerializeField] private float jumpForce;
    [SerializeField] float glideSpeed;
    [SerializeField] bool isGlideActive;

    [Header("Other")]
    [SerializeField] private bool isGrounded;
    [SerializeField] int jumpsLeft;
    [SerializeField] Color _jumpColor;
    [SerializeField] AudioClip onAirJumpSound;
    private Rigidbody2D rb2;
    private float movingDirX;
    private bool _jump;
    AudioSource _audioSource;
    Animator animator;
    void Start()
    {
        rb2= GetComponent<Rigidbody2D>();
        jumpsLeft = jumpCounts;
        isGrounded = true;
        _audioSource= GetComponent<AudioSource>();
        animator= GetComponent<Animator>();
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




    private void FixedUpdate()
    {
        rb2.transform.position += new Vector3(movingDirX, 0, 0) * movementSpeed * Time.fixedDeltaTime;
        if (_jump)
        {
            
            rb2.velocity = new Vector2(rb2.velocity.x, 0f);
            rb2.AddForce(transform.up * jumpForce,ForceMode2D.Impulse);
            _jump = false;
            jumpsLeft --;

            if (!isGrounded)
            {
                transform.Find("jumpEffect").GetComponent<ParticleSystem>().Play();
                _audioSource.clip = onAirJumpSound;
                _audioSource.Play();
            }
            isGrounded = false;
        }
    }

    void Update()
    {

        movingDirX = Input.GetAxis("Horizontal");
        
        animator.SetFloat("moveSpeed",Mathf.Abs(movingDirX));

        animator.SetBool("isGrounded", isGrounded);
        if (movingDirX > 0)
        {
            transform.localScale = new Vector3(1,1,1);

        }
        if (movingDirX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
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
