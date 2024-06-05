using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    #region 전역 변수

    public int MaxHP = 100;
    public float HP = 100;
    public int potionHP = 50;

    [HideInInspector] public bool isSpace;
    [HideInInspector] public bool isDrinking;
    [HideInInspector] public bool isDash;
    [HideInInspector] public bool isBlocking;

    float moveSpeed;
    

    Coroutine moveCoroutine;
    Coroutine dodgeCoroutine;
    Coroutine attackCoroutine;
    Coroutine drinkCoroutine;

    #endregion

    GameObject cameraObj;
    ThirdPersonConroller playerMoveController;    
    CapsuleCollider playercolider;
    Animator anim;
    PlayerWeapon equipWeapon;
    
    public GameObject potionEffect;
    public Transform potionPos;

    

    private void Awake()
    {
        Debug.Log("Player Awake 실행");
        cameraObj = GameObject.Find("Camera");
        playerMoveController = GetComponent<ThirdPersonConroller>();
        playercolider = GetComponent<CapsuleCollider>();
        anim = GetComponentInChildren<Animator>();

        isDash = false;
        isSpace = false;
        isDrinking = false;
        isBlocking = false;

        moveCoroutine = StartCoroutine(Move());
        dodgeCoroutine = StartCoroutine(Dodge());
        attackCoroutine = StartCoroutine(Attack());
        drinkCoroutine = StartCoroutine(DrinkPotion());
        StartCoroutine(Block());
        StartCoroutine(Death());

        equipWeapon = GetComponentInChildren<PlayerWeapon>();

        //PlayerDeath();
    }

    /// <summary>
    /// 0 : MoveAnimation 코루틴
    /// 1 : DodgeAnimation 코루틴
    /// 2 : AttackAnimation 코루틴
    /// </summary>
    /// <param name="num"></param>
    void StartMethod(int num)
    {
        switch (num)
        {
            case 0:
                if (moveCoroutine != null)
                {
                    moveCoroutine = StartCoroutine(Move());
                }                
                break;
            case 1:
                if (dodgeCoroutine != null)
                {
                    dodgeCoroutine = StartCoroutine(Dodge());
                }                
                break;
            case 2:
                if (attackCoroutine != null)
                {
                    attackCoroutine = StartCoroutine(Attack());
                }                
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 0 : MoveAnimation 코루틴
    /// 1 : DodgeAnimation 코루틴
    /// 2 : AttackAnimation 코루틴
    /// </summary>
    /// <param name="num"></param>
    public void StopMethod(int num)
    {
        switch (num)
        {
            case 0:
                if (moveCoroutine != null)
                {
                    StopCoroutine(moveCoroutine);
                }
                break;
            case 1:
                if (dodgeCoroutine != null)
                {
                    StopCoroutine(dodgeCoroutine);
                }
                break;
            case 2:
                if (attackCoroutine != null)
                {
                    StopCoroutine(attackCoroutine);
                }
                break;
            default:
                break;
        }
    }    

    public void PlayerDeath()
    {
        //CharacterController c_Controller = GetComponent<CharacterController>();
        //Rigidbody rigid = GetComponent<Rigidbody>();
        
        //StopAllCoroutines();
        //playerMoveController.StopAllCoroutines();
        //anim.SetTrigger("Death");

        //c_Controller.center = new Vector3(0, 1.6f, 0);

        CharacterController c_Controller = GetComponent<CharacterController>();
        Rigidbody rigid = GetComponent<Rigidbody>();

        StopAllCoroutines();
        playerMoveController.StopAllCoroutines();
        anim.SetTrigger("Death");

        // 비활성화하여 더 이상 움직이지 않도록 설정
        if (c_Controller != null)
        {
            c_Controller.enabled = false;
        }

        // Rigidbody를 비활성화하여 물리 효과를 막음
        if (rigid != null)
        {
            rigid.isKinematic = true;
        }

        // 필요에 따라 콜라이더를 조정
        playercolider.enabled = false;
    }

    /// <summary>
    /// 이동 애니메이션
    /// 이동 애니메이션 부드럽게 수정 필요
    /// </summary>
    /// <returns></returns>
    IEnumerator Move()
    {
        moveSpeed = 0f;
        while (true)
        {
            yield return null;

            float forward = Input.GetAxis("Vertical");
            float right = Input.GetAxis("Horizontal");
            isDash = Input.GetButton("Dash");

            if (forward == 0 && right == 0)
            {
                float stopSpeed = 0f;
                anim.SetFloat("MoveSpeed", Mathf.Lerp(moveSpeed, stopSpeed, Time.deltaTime));
                moveSpeed = stopSpeed;
            }
            else if (!isDash && (forward != 0 || right != 0))
            {
                float walkSpeed = 0.5f;
                anim.SetFloat("MoveSpeed", Mathf.Lerp(moveSpeed, walkSpeed, Time.deltaTime));
                moveSpeed = walkSpeed;
            }
            else if (isDash && (forward != 0 || right != 0))
            {
                float runSpeed = 1f;
                anim.SetFloat("MoveSpeed", Mathf.Lerp(moveSpeed, runSpeed, Time.deltaTime));
                moveSpeed = runSpeed;
            }
        }
    }

    /// <summary>
    /// 회피 애니메이션
    /// </summary>
    /// <returns></returns>
    IEnumerator Dodge()
    {
        while (true)
        {
            yield return null;

            isSpace = Input.GetButtonDown("Dodge");

            
            if (isSpace)
            {
                anim.SetTrigger("DodgeRoll");

                this.gameObject.tag = "Untagged";

                yield return new WaitForSeconds(1.5f);

                this.gameObject.tag = "Player";

                // 회피기 쿨타임
                yield return new WaitForSeconds(1.5f);
            }
            
        }
    }
    IEnumerator Attack()
    {
        while (true)
        {
            yield return null;

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("공격");
                equipWeapon.Use();

                anim.SetTrigger("Attack");

                // 이동 막기
                StopMethod(0);
                playerMoveController.StopMethod(0);
                
                yield return new WaitForSeconds(1.5f);  // 공격 대기시간


                StartMethod(0);
                playerMoveController.StartMethod(0);
            }
        }
    }
    IEnumerator Block()
    {
        bool isDownSpeed = false;
        while (true)
        {
            yield return null;

            isBlocking = Input.GetMouseButton(1);

            if (isBlocking)
            {
                moveSpeed *= 0.8f;
                isDownSpeed = true;

                Vector3 forwardDir = cameraObj.transform.forward;
                

                anim.SetLayerWeight(1, 1f);
                anim.SetBool("isBlocking", true);

            }
            else
            {
                if (isDownSpeed)
                {
                    moveSpeed /= 0.8f;
                    isDownSpeed = false;
                    anim.SetLayerWeight(1, 0f);
                }
                
                anim.SetBool("isBlocking", false);
                
            }
        }
    }
    IEnumerator DrinkPotion()
    {                
        while (true)
        {
            yield return null;

            isDrinking = Input.GetButtonDown("Drink");

            if (isDrinking)
            {
                Debug.Log("포션 먹었다.");
                anim.SetTrigger("DrinkPotion");

                anim.SetLayerWeight(1, 1f);

                GameObject potionAura = Instantiate(potionEffect, potionPos.position, potionPos.rotation);
                potionAura.transform.SetParent(potionPos);
                Destroy(potionAura, 2f);

                HP += potionHP;
                if (HP > MaxHP)
                {
                    HP = MaxHP;
                }

                yield return new WaitForSeconds(2f);

                anim.SetLayerWeight(1, 0f);                
            }
        }
    }
    IEnumerator Death()
    {        
        while (true)
        {
            yield return null;

            if (Input.GetKeyDown("q"))
            {
                HP = 0f;
            }
            if (HP <= 0f)
            {
                PlayerDeath();                
                break;
            }
        }        
    }
}
