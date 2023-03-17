using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Security.Cryptography;
using UnityEngine;
using Debug = UnityEngine.Debug;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    //Components
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;

    //movement var
    public float speed;
    public float jumpForce;

    //groundcheck stuff
    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask isGroundLayer;
    public float groundCheckRadius;

    private int jumpcount;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        if (speed <= 0)
        {
            speed = 6.0f;
            Debug.Log("Speed was set incorrect, defaulting to " + speed.ToString());
        }

        if (jumpForce <= 0)
        {
            jumpForce = 300;
            Debug.Log("Jump force was set incorrect, defaulting to " + jumpForce.ToString());
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.2f;
            Debug.Log("Ground Check Radius was set incorrect, defaulting to " + groundCheckRadius.ToString());
        }

        if (!groundCheck)
        {
            groundCheck = GameObject.FindGameObjectWithTag("GroundCheck").transform;
            Debug.Log("Ground Check not set, finding it manually!");
        }
    }
            // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] curPlayingClip = anim.GetCurrentAnimatorClipInfo(0);
        float hInput = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

        if (curPlayingClip.Length > 0)
        {
            if (Input.GetButtonDown("Fire1") && curPlayingClip[0].clip.name != "AirAttack")
            {
                anim.SetTrigger("AirAttack");
            }
            else if (curPlayingClip[0].clip.name == "AirAttack" )
            {
                rb.velocity = Vector2.zero;
            }
            else if (Input.GetButtonDown("Fire2") && curPlayingClip[0].clip.name != "BeamAttack")
            {
                anim.SetTrigger("BeamAttack");
            }
            else if (curPlayingClip[0].clip.name == "BeamAttack")
            {
                rb.velocity = Vector2.zero;
            }
            else
            {
                Vector2 moveDirection = new Vector2(hInput * speed, rb.velocity.y);
                rb.velocity = moveDirection;
            }

        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
            jumpcount++;
        }
        else if (Input.GetButtonDown("Jump"))
        {
            if (jumpcount <= 1)
            {
                jumpcount++;
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * jumpForce);
            }
            
        }

        anim.SetFloat("hInput", Mathf.Abs(hInput));
        anim.SetBool("isGrounded", isGrounded);

        //check for flipped and create an algorithm to flip your character
        if (hInput != 0)
            sr.flipX = (hInput < 0);

        if (isGrounded)
        {
            rb.gravityScale = 1;
            jumpcount = 0;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerKiller")|| collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.instance.lives--;
        }
        if (collision.gameObject.CompareTag("beam"))
        {
            anim.SetBool("AllowBeamAttack", true);
        }
    }


    /*public void IncreaseGravity()
    {
        rb.gravityScale = 5;
    }

    // use for powerup and if you want to collide with stuff
    private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Powerup"))
            {
                //do something
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {

        }
        private void OnTriggerStay2D(Collider2D other)
        {

        }
        private void OnCollisionEnter2D(Collider2D collison)
        {

        }
        private void OnCollisionExit2D(Collider2D collison)
        {

        }
        private void OnCollisionStay2D(Collider2D collison)
        {

        }*/
}
