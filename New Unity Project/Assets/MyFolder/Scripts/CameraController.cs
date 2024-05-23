using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float mouseSensitivity = 2.0f;

    Transform playerTrans;
    Transform cameraTransform;
    Transform cameraParentTransform;
    CharacterController cController;

    Vector3 mouseMove;


    private void Awake()
    {
        playerTrans = transform;
        cameraTransform = Camera.main.transform;
        cameraParentTransform = cameraTransform.parent;
        cController = GetComponent<CharacterController>();
    }

    private void LateUpdate()
    {        
        cameraParentTransform.position = playerTrans.position + Vector3.up * 1.0f;
    }


    void Balance()
    {
        if (playerTrans.eulerAngles.x != 0 || playerTrans.eulerAngles.z != 0)
        {
            playerTrans.eulerAngles = new Vector3(0, playerTrans.eulerAngles.y, 0);
        }
    }

    public void ZummIOControl()
    {
        Camera.main.transform.localPosition += new Vector3(0, 0, Input.GetAxisRaw("Mouse ScrollWheel") * 2.0f);

        if (-2 < Camera.main.transform.localPosition.z) // 좌항과 우항에 대한 연산 속도 차이
        {
            Camera.main.transform.localPosition = new Vector3(
                                                                Camera.main.transform.localPosition.x,
                                                                Camera.main.transform.localPosition.y,
                                                                -2
                                                                );
        }


        else if (Camera.main.transform.localPosition.z < -5)
        {
            Camera.main.transform.localPosition = new Vector3(
                                                                Camera.main.transform.localPosition.x,
                                                                Camera.main.transform.localPosition.y,
                                                                -5
                                                                );
        }

    }
}
