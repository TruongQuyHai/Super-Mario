using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D body;
    private Animator anim;

    public Transform groundCheckPosition;
    public LayerMask groundLayer;

    private bool isGrounded;
    private bool jumped;

    private float jumpPower = 12f;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.5f, groundLayer))
        //{
        //    print("Collided with ground");
        //}
        CheckIfGrounded();
        PlayerJump();
    }

    private void FixedUpdate()
    {
        PlayerWalk();
    }

    void PlayerWalk()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (h > 0)
        {
            body.velocity = new Vector2(speed, body.velocity.y);
            ChangeDirection(1f);
        }
        else if (h < 0)
        {
            body.velocity = new Vector2(-speed, body.velocity.y);
            ChangeDirection(-1f);
        }
        else
        {
            body.velocity = new Vector2(0f, body.velocity.y);
        }

        anim.SetInteger("Speed", Mathf.Abs((int)body.velocity.x));
    }

    void ChangeDirection(float direction)
    {
        Vector3 temp = transform.localScale;
        temp.x = direction;
        transform.localScale = temp;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    //if (collision.gameObject.tag == "Ground")
    //    //{
    //    //    print("Collided with the ground");
    //    //}
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //if (collision.tag == "Ground")
    //    //{
    //    //    print("Collided with tag");
    //    //}
    //}

    void CheckIfGrounded()
    {
        isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.1f, groundLayer);

        if (isGrounded)
        {
            if (jumped)
            {
                jumped = false;
                anim.SetBool("Jump", false);
            }
        }
    }

    void PlayerJump()
    {
        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                jumped = true;
                body.velocity = new Vector2(body.velocity.x, jumpPower);
                anim.SetBool("Jump", true);
            }
        }
    }
}
