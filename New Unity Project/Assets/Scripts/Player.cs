using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region 카메라 관련
    CameraController cameraControl;
    Transform cameraTransform;
    Transform cameraParentTransform;
    Vector3 mouseMove;
    float mouseSensitivity = 2.0f;
    #endregion

    #region 이동
    float hAxis;
    float vAxis;
    Vector3 moveDirection;
    Vector3 moveVec;
    float moveSpeed;
    float rotateSpeed = 10.0f;
    #endregion

    #region 구르기

    Vector3 rollVec;
    bool spaceDown;
    bool isRoll = false;
    #endregion

    #region 대쉬

    bool shiftDown;
    #endregion

    #region Shot

    [HideInInspector]
    bool isFire;
    bool isSingleFire;
    float fireTimer = 0.0f;
    [SerializeField]
    GameObject shoot;
    #endregion

    Vector3 characterRotation;

    Animator anim;


    void Awake()
    {
        cameraControl = FindObjectOfType<CameraController>();
        anim = GetComponentInChildren<Animator>();


        cameraTransform = Camera.main.transform;
        cameraParentTransform = cameraTransform.parent;

        StartCoroutine(MoveCoroutine());
        StartCoroutine(AttackCoroutine());
        //StartCoroutine(AnimationCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        //GetInputKey();
        //Move();
        //Dash();
        //Roll();
        //attack();

        //MouseTurn();

        cameraControl.ZummIOControl();
        SetAnimation();
    }

    private void LateUpdate()
    {
        cameraParentTransform.position = this.transform.position + Vector3.up * 1.0f;


        mouseMove += new Vector3
            (
                -Input.GetAxisRaw("Mouse Y") * mouseSensitivity,
                Input.GetAxisRaw("Mouse X") * mouseSensitivity,
                0
            );
        

        if (mouseMove.x < -30)
        {
            mouseMove.x = -30;
        }
        if (mouseMove.x > 30)
        {
            mouseMove.x = 30;
        }

        cameraParentTransform.localEulerAngles = mouseMove;

    }

    //void ZummIOControl()
    //{
    //    Camera.main.transform.localPosition += new Vector3(0, 0, Input.GetAxisRaw("Mouse ScrollWheel") * 2.0f);

    //    if (-2 > Camera.main.transform.localPosition.z) // 좌항과 우항에 대한 연산 속도 차이
    //    {
    //        Camera.main.transform.localPosition = new Vector3(
    //                                                            Camera.main.transform.localPosition.x,
    //                                                            Camera.main.transform.localPosition.y,
    //                                                            -2
    //                                                            );
    //    }


    //    else if (Camera.main.transform.localPosition.z < -5)
    //    {
    //        Camera.main.transform.localPosition = new Vector3(
    //                                                            Camera.main.transform.localPosition.x,
    //                                                            Camera.main.transform.localPosition.y,
    //                                                            -5
    //                                                            );
    //    }

    //}

    void Turn()
    {

        Quaternion charRotation = Quaternion.LookRotation(moveVec);
        
        this.transform.rotation = Quaternion.Slerp
            (
                this.transform.rotation,
                charRotation,
                rotateSpeed * Time.deltaTime
            );
        
        
    }

    void MouseTurn()
    {
        characterRotation += new Vector3
            (
                -Input.GetAxisRaw("Mouse Y") * mouseSensitivity,
                Input.GetAxisRaw("Mouse X") * mouseSensitivity,
                0
            );
        Quaternion charRotation = Quaternion.Euler(characterRotation);
        charRotation.x = charRotation.z = 0;
        this.transform.rotation = Quaternion.Slerp
            (
                this.transform.rotation,
                charRotation,
                10.0f * Time.deltaTime
            );
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
            moveSpeed *= 1.5f;
            rollVec = moveVec;
            Invoke("RollOut", 0.7f);
        }
    }
    void RollOut()
    {
        isRoll = false;
    }
    public void attack()
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
        anim.SetBool("isRun", moveVec != Vector3.zero);
        //anim.SetBool("isBack", vAxis < 0);
        //anim.SetBool("isLeft", hAxis < 0);
        //anim.SetBool("isRight", hAxis > 0);

        if (shiftDown)
        {
            anim.SetTrigger("doDash"); 
        }
        else if (spaceDown)
        {
            anim.SetTrigger("doRoll"); 
        }
    }

    IEnumerator MoveCoroutine()
    {
        while (true)
        {
            moveSpeed = 5.0f;

            yield return null;
            GetInputKey();

            moveVec = new Vector3(hAxis, 0, vAxis).normalized;
            Turn();

            Dash();
            Roll();

            if (isRoll)
            {
                moveVec = rollVec;
            }

            //moveVec = moveDirection;

            this.transform.position += moveVec * moveSpeed * Time.deltaTime;
            //this.transform.Translate(moveVec * moveSpeed * Time.deltaTime);
        }
    }
    IEnumerator AttackCoroutine()
    {
        while (true)
        {
            yield return null;
            attack();
        }
    }
    IEnumerator AnimationCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        SetAnimation();
    }
}
