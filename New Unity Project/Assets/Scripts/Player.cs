using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float hAxis;
    float vAxis;

    Vector3 moveVec;
    Vector3 rollVec;

    bool shiftDown;
    bool spaceDown;
    bool isFire;
    bool isSingleFire;

    bool isRoll = false;
    float moveSpeed;
    float fireTimer = 0.0f;


    [SerializeField]
    GameObject shoot;
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
        attack();
        SetAnimation();
    }
    void Move()
    {

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
        if (shiftDown)
        {
            moveSpeed *= 2;
        }
    }
    void Roll()
    {

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
    void Jump()
    {

    }
    void attack()
    {
        if (isFire)
        {
            anim.SetLayerWeight(1, 1);
            if (anim.GetCurrentAnimatorStateInfo(1).IsName("ShootAutoshot_AR"))
            {
                if (fireTimer > 0.15f)
                {
                    shoot.GetComponent<Shoot>().Use();
                    fireTimer = 0.0f;
                }
                
                fireTimer += Time.deltaTime;
                return;
            }
            anim.SetTrigger("doShot");
        }
        else
        {
            anim.SetLayerWeight(1, 0);
            anim.SetTrigger("doNotShot");
        }
        if (isSingleFire)
        {
            SingleAttack();
        }
    }
    void SingleAttack()
    {
        anim.SetTrigger("doSingleShot");
        shoot.GetComponent<Shoot>().Use();
    }
    void GetInputKey()
    {
        vAxis = Input.GetAxisRaw("Vertical");
        hAxis = Input.GetAxisRaw("Horizontal");
        spaceDown = Input.GetButton("Roll");
        shiftDown = Input.GetButton("Dash");
        isFire = Input.GetMouseButton(0);
        isSingleFire = Input.GetMouseButtonDown(1);
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
