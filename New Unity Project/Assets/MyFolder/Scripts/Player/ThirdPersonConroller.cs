using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonConroller : MonoBehaviour
{
    #region 전역 변수

    public float speed = 6.0f;
    public float turnSpeed = 10.0f;
    public float gravity = 9.8f;

    Coroutine moveCoroutine;

    #endregion

    #region 벡터 / 트랜스폼

    private Vector3 moveDirection = Vector3.zero;

    public Transform cameraTransform;  // 카메라 트랜스폼
    #endregion

    #region 컴포넌트 / 스크립트

    private CharacterController characterController;

    Player player;

    #endregion

    

    private void Awake()
    {        
        characterController = GetComponent<CharacterController>();
        player = GetComponent<Player>();

        moveCoroutine = StartCoroutine(MoveCoroutine());
    }

    /// <summary>
    /// 0 : MoveCoroutine
    /// </summary>
    /// <param name="number"></param>
    public void StartMethod(int number)
    {
        switch (number)
        {
            case 0:
                if (moveCoroutine != null)
                {
                    moveCoroutine = StartCoroutine(MoveCoroutine());
                }
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 0 : MoveCoroutine
    /// </summary>
    /// <param name="number"></param>
    public void StopMethod(int number)
    {
        switch (number)
        {
            case 0:
                StopCoroutine(moveCoroutine);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 캐릭터 이동 관련 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator MoveCoroutine()
    {
        while (true)
        {
            yield return null;

            // 입력 받아오기
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (horizontal != 0 || vertical != 0)
            {
                // 카메라의 회전 방향에 맞춰 캐릭터 이동 방향 설정
                Vector3 forward = cameraTransform.forward;
                Vector3 right = cameraTransform.right;
                forward.y = 0;
                right.y = 0;
                forward.Normalize();
                right.Normalize();

                Vector3 direction = (forward * vertical + right * horizontal).normalized;
                if (direction.magnitude >= 0.1f)
                {
                    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSpeed, 0.1f);
                    transform.rotation = Quaternion.Euler(0, angle, 0);

                    moveDirection = direction * speed;
                }
            }
            else
            {
                moveDirection = Vector3.zero;
            }


            // 중력 적용
            //moveDirection.y -= gravity * Time.deltaTime;

            // 캐릭터 이동
            characterController.Move(moveDirection * Time.deltaTime);

        }
    }

}
