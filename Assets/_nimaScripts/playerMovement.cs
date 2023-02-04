using System.Collections;
using System.Collections.Generic;
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
    void Start()
    {
        rb2= GetComponent<Rigidbody2D>();
        jumpsLeft = jumpCounts;
        isGrounded = true;
        _audioSource= GetComponent<AudioSource>();
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

            if (!isGrounded)
            {
                transform.Find("jumpEffect").GetComponent<ParticleSystem>().Play();
                _audioSource.clip = onAirJumpSound;
                _audioSource.Play();
            }
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
