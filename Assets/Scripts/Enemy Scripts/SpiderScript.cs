using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D body;

    private Vector3 moveDirection = Vector3.down;
    public float minY;
    public float maxY;

    private string couroutine_Name = "ChangeMovement";

    private void Awake()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeMovement());
    }

    // Update is called once per frame
    void Update()
    {
        MoveSpider();
    }

    void MoveSpider()
    {
        Vector3 move = moveDirection * Time.smoothDeltaTime;
        Vector3 pos = transform.position + move;
        if (pos.y < minY || pos.y > maxY)
        {
            move.y = -move.y;
            moveDirection = pos.y < minY ? Vector3.up : Vector3.down;
        }
        transform.Translate(move);
    }

    IEnumerator ChangeMovement()
    {
        yield return new WaitForSeconds(Random.Range(2f, 5f));

        if (moveDirection == Vector3.down && transform.localPosition.y <= minY)
        {
            moveDirection = Vector3.up;
        }
        else
            moveDirection = Vector3.down;

        StartCoroutine(couroutine_Name);
    }

    IEnumerator SpiderDead()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == MyTags.BULLET_TAG)
        {
            anim.Play("SpiderDead");
            body.bodyType = RigidbodyType2D.Dynamic;
            StartCoroutine(SpiderDead());
        }
    }
}
