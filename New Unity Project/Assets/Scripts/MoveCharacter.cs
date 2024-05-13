using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    float hAxis;
    float vAxis;

    Vector3 move;

    bool shiftDown;
    bool spaceDown;
    float moveSpeed;


    Animator anim;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveSpeed = 5.0f;
        GetInputKey();
        Move();
        SetAnimation();
    }
    void Move()
    {
        if (shiftDown)
        {
            moveSpeed *= 2;
        }

        move = new Vector3(hAxis, 0.0f, vAxis).normalized;

        this.transform.Translate(move * moveSpeed * Time.deltaTime);

    }
    void GetInputKey()
    {
        vAxis = Input.GetAxisRaw("Vertical");
        hAxis = Input.GetAxisRaw("Horizontal");
        shiftDown = Input.GetButton("Dash");
        spaceDown = Input.GetButton("Roll");
    }

    void SetAnimation()
    {
        anim.SetBool("isForward", vAxis > 0);
        anim.SetBool("isBack", vAxis < 0);
        anim.SetBool("isLeft", hAxis < 0);
        anim.SetBool("isRight", hAxis > 0);
        anim.SetBool("isDash", shiftDown);
        anim.SetBool("isRoll", spaceDown);
    }
}
