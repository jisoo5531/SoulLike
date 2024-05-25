using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    #region 전역 변수

    public bool isShift;
    public bool isAttacking;

    float moveSpeed;


    Coroutine moveAniCoroutine;
    Coroutine dodgeCoroutine;
    Coroutine attackCoroutine;

    #endregion 

    #region 컴포넌트

    ThirdPersonConroller playerConroller;
    Animator anim;

    #endregion

    

    private void Awake()
    {
        Debug.Log("Player Awake 실행");
        playerConroller = FindObjectOfType<ThirdPersonConroller>();
        anim = GetComponentInChildren<Animator>();

        isAttacking = false;

        moveAniCoroutine = StartCoroutine(MoveAnimation());

        dodgeCoroutine = StartCoroutine(DodgeAnimation());
        attackCoroutine = StartCoroutine(AttackAnimation());
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
                if (moveAniCoroutine != null)
                {
                    moveAniCoroutine = StartCoroutine(MoveAnimation());
                }                
                break;
            case 1:
                if (dodgeCoroutine != null)
                {
                    dodgeCoroutine = StartCoroutine(DodgeAnimation());
                }                
                break;
            case 2:
                if (attackCoroutine != null)
                {
                    attackCoroutine = StartCoroutine(AttackAnimation());
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
    void StopMethod(int num)
    {
        switch (num)
        {
            case 0:
                if (moveAniCoroutine != null)
                {
                    StopCoroutine(moveAniCoroutine);
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
    IEnumerator MoveAnimation()
    {
        moveSpeed = 0f;
        while (true)
        {
            yield return null;

            float forward = Input.GetAxis("Vertical");
            float right = Input.GetAxis("Horizontal");
            if (forward == 0 && right == 0)
            {
                float stopSpeed = 0f;
                anim.SetFloat("MoveSpeed", Mathf.Lerp(moveSpeed, stopSpeed, Time.deltaTime));
                moveSpeed = stopSpeed;
            }
            else if (!Input.GetButton("Dash") && (forward != 0 || right != 0))
            {
                float walkSpeed = 0.5f;
                anim.SetFloat("MoveSpeed", Mathf.Lerp(moveSpeed, walkSpeed, Time.deltaTime));
                moveSpeed = walkSpeed;
            }
            else if (Input.GetButton("Dash") && (forward != 0 || right != 0))
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
    IEnumerator DodgeAnimation()
    {
        while (true)
        {
            yield return null;

            isShift = Input.GetButton("Dodge");
            if (isShift)
            {
                anim.SetTrigger("DodgeRoll");

                // 회피기 쿨타임
                yield return new WaitForSeconds(3f);
            }
            
        }
    }
    IEnumerator AttackAnimation()
    {
        while (true)
        {
            yield return null;

            if (Input.GetMouseButtonDown(0) && !isAttacking)
            {
                isAttacking = true;
                anim.SetTrigger("Attack");

                StopMethod(0);
                playerConroller.StopMethod(0);
                
                yield return new WaitForSeconds(1.5f);

                isAttacking = false;

                StartMethod(0);
                playerConroller.StartMethod(0);
                
            }
        }
    }
}
