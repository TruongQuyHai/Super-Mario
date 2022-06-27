using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailScript : MonoBehaviour
{

    public float moveSpeed = 1f;
    private Rigidbody2D body;
    private Animator anim;

    public LayerMask playerLayer;

    private bool moveLeft;
    private bool canMove;
    private bool stunned;

    public Transform top_Collision, right_Collision, down_Collision, left_Collision;
    public Vector3 left_Collision_Position, right_Collision_Position;


    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        left_Collision_Position = left_Collision.position;
        right_Collision_Position = right_Collision.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        moveLeft = true;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (moveLeft)
            {
                body.velocity = new Vector2(-moveSpeed, body.velocity.y);
            }
            else
            {
                body.velocity = new Vector2(moveSpeed, body.velocity.y);
            }
        }

        CheckCollision();
    }

    void CheckCollision()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(left_Collision.position, Vector2.left, 0.1f, playerLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(right_Collision.position, Vector2.right, 0.1f, playerLayer);

        Collider2D topHit = Physics2D.OverlapCircle(top_Collision.position, 0.05f, playerLayer);

        if (topHit != null)
        {
            if (topHit.gameObject.tag == MyTags.PLAYER_TAG)
            {
                if (!stunned)
                {
                    topHit.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 7f);
                    canMove = false;
                    stunned = true;
                    body.velocity = new Vector2(0, 0);
                    anim.Play("Stunned");

                    // BEETLE CODE
                    if (tag == MyTags.BEETLE_TAG)
                    {
                        StartCoroutine(Dead(0.5f));
                    }
                }
            }
        }

        if (leftHit)
        {
            if (leftHit.collider.gameObject.tag == MyTags.PLAYER_TAG)
            {
                if (!stunned)
                {
                    // apply damge to player
                }
                else
                {
                    if (tag != MyTags.BEETLE_TAG)
                    {
                        body.velocity = new Vector2(15f, body.velocity.y);
                        StartCoroutine(Dead(3f));
                    }
                }
            }
        }

        if (rightHit)
        {
            if (rightHit.collider.gameObject.tag == MyTags.PLAYER_TAG)
            {
                if (!stunned)
                {
                    // apply damge to player
                }
                else
                {
                    if (tag != MyTags.BEETLE_TAG)
                    {
                        body.velocity = new Vector2(-15f, body.velocity.y);
                        StartCoroutine(Dead(3f));
                    }
                }
            }
        }

        if (!Physics2D.Raycast(down_Collision.position, Vector2.down, 0.1f))
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        moveLeft = !moveLeft;
        Vector3 tempScale = transform.localScale;

        if (moveLeft)
        {
            tempScale.x = Mathf.Abs(tempScale.x);
            left_Collision.position = left_Collision_Position;
            right_Collision.position = right_Collision_Position;
        }
        else
        {
            tempScale.x = -Mathf.Abs(tempScale.x);
            left_Collision.position = right_Collision_Position;
            right_Collision.position = left_Collision_Position;
        }

        transform.localScale = tempScale;
    }

    IEnumerator Dead(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == MyTags.BULLET_TAG)
        {
            if (tag == MyTags.BEETLE_TAG)
            {
                anim.Play("Stunned");

                canMove = false;
                body.velocity = new Vector2(0, 0);

                StartCoroutine(Dead(0.4f));
            }
            else if (tag == MyTags.SNAIL_TAG)
            {
                if (!stunned)
                {
                    anim.Play("Stunned");
                    stunned = true;
                    canMove = false;
                    body.velocity = new Vector2(0, 0);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
