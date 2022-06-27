using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D body;

    private Vector3 moveDirection = Vector3.down;

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
        transform.Translate(moveDirection * Time.smoothDeltaTime);

    }

    IEnumerator ChangeMovement()
    {
        yield return new WaitForSeconds(Random.Range(2f, 5f));

        if (moveDirection == Vector3.down)
            moveDirection = Vector3.up;
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
        }
        StartCoroutine(SpiderDead());
    }
}
