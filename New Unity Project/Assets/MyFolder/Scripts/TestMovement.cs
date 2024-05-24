using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    public float speed = 6.0f;
    public float turnSpeed = 10.0f;
    public float gravity = 9.8f;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 입력 받아오기
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // 캐릭터 회전
        Vector3 direction = new Vector3(horizontal, 0, vertical);
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSpeed, 0.1f);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            // 이동 방향 설정
            moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            moveDirection *= speed;
        }

        // 중력 적용
        moveDirection.y -= gravity * Time.deltaTime;

        // 캐릭터 이동
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
