using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region 변수

    float moveSpeed;

    #endregion 

    #region 컴포넌트

    Animator anim;

    #endregion

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        StartCoroutine(MoveAnimation());
    }

    /// <summary>
    /// 이동 관련 코루틴
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
}
