using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    #region 전역 변수

    public int MaxHP = 100;
    public float HP = 100;

    [HideInInspector] public bool isSpace;
    [HideInInspector] public bool isAttacking;
    [HideInInspector] public bool isDrinking;
    [HideInInspector] public bool isDash;

    public float moveSpeed;


    Coroutine moveCoroutine;
    Coroutine dodgeCoroutine;
    Coroutine attackCoroutine;
    Coroutine drinkCoroutine;

    #endregion 

    #region 컴포넌트

    ThirdPersonConroller playerConroller;
    CapsuleCollider playercolider;
    Animator anim;
    
    PlayerWeapon equipWeapon;

    #endregion

    

    private void Awake()
    {
        Debug.Log("Player Awake 실행");
        playerConroller = GetComponent<ThirdPersonConroller>();
        playercolider = GetComponent<CapsuleCollider>();
        anim = GetComponentInChildren<Animator>();

        isDash = false;
        isSpace = false;
        isAttacking = false;
        isDrinking = false;

        moveCoroutine = StartCoroutine(Move());
        dodgeCoroutine = StartCoroutine(Dodge());
        attackCoroutine = StartCoroutine(Attack());
        drinkCoroutine = StartCoroutine(DrinkPotion());

        equipWeapon = GetComponentInChildren<PlayerWeapon>();
    }

    //private void OnEnable()
    //{
    //    Debug.Log("온");
    //    moveCoroutine = StartCoroutine(Move());
    //    dodgeCoroutine = StartCoroutine(Dodge());
    //    attackCoroutine = StartCoroutine(Attack());
    //}

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
                playerConroller.StopMethod(0);
                
                yield return new WaitForSeconds(1.5f);  // 공격 대기시간


                StartMethod(0);
                playerConroller.StartMethod(0);
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
                anim.SetTrigger("DrinkPotion");
                anim.SetLayerWeight(1, 1f);

                yield return new WaitForSeconds(2f);

                anim.SetLayerWeight(1, 0f);                
            }
        }
    }
}
