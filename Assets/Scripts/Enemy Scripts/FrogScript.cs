using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : MonoBehaviour
{
    private Animator anim;

    private bool animation_Started;
    private bool animation_Finished;

    private int jumpedTimes;
    private bool jumpLeft = true;

    private string coroutine_Name = "FrogJump";

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(coroutine_Name);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        if (animation_Finished && animation_Started)
        {
            animation_Started = false;
            transform.parent.position = transform.position;
            transform.localPosition = Vector3.zero;
        }
    }

    IEnumerator FrogJump()
    {
        yield return new WaitForSeconds(Random.Range(1f, 4f));
        animation_Started = true;
        animation_Finished = false;

        jumpedTimes++;

        if (jumpLeft)
        {
            anim.Play("FrogJumpLeft");
        }
        else
        {
            anim.Play("FrogJumpRight");
        }
        StartCoroutine(coroutine_Name);
    }

    void AnimationFinished()
    {
        if (jumpLeft)
            anim.Play("FrogIdle");
        else anim.Play("FrogIdleRight");
        animation_Finished = true;

        if (jumpedTimes == 3)
        {
            jumpedTimes = 0;
            Vector3 tempScale = transform.localScale;
            tempScale.x *= -1;
            transform.localScale = tempScale;
            jumpLeft = !jumpLeft;
        }
    }
}
