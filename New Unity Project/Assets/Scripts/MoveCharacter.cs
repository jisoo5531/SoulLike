using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    float hAxis;
    float vAxis;

    Vector3 moveVec;
    Vector3 rollVec;

    bool shiftDown;
    bool spaceDown;
    bool isRoll = false;
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
        Dash();
        Roll();
        SetAnimation();
    }
    void Move()
    {
        if (shiftDown)
        {
            moveSpeed *= 2;
        }
        if (isRoll)
        {
            moveVec = rollVec;
            moveSpeed *= 1.5f;
        }
        else
        {
            moveVec = new Vector3(hAxis, 0.0f, vAxis).normalized;
        }
        this.transform.Translate(moveVec * moveSpeed * Time.deltaTime);

    }
    void Dash()
    {
        shiftDown = Input.GetButton("Dash");
    }
    void Roll()
    {
        spaceDown = Input.GetButton("Roll");
        if (spaceDown)
        {
            isRoll = true;
            rollVec = moveVec;
            Invoke("RollOut", 0.7f);
        }
        
    }
    void RollOut()
    {
        isRoll = false;
    }
    void GetInputKey()
    {
        vAxis = Input.GetAxisRaw("Vertical");
        hAxis = Input.GetAxisRaw("Horizontal");
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
