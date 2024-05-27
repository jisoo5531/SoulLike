using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_DeathKnight : MonoBehaviour
{
    #region 전역 변수

    #endregion


    #region 트랜스폼 / 스크립트

    public Transform playerTrans;

    Animator anim;

    #endregion


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        LookAtPlayer();


    }

    void LookAtPlayer()
    {
        Vector3 ToTargetVec = transform.position - playerTrans.position;
        ToTargetVec = transform.InverseTransformDirection(ToTargetVec);
        ToTargetVec.Normalize();

        Vector2 ToTargetV2 = new Vector2(ToTargetVec.x, ToTargetVec.y);
        float angleToRot = Vector2.Angle(Vector2.up, ToTargetV2);
        if (ToTargetV2.x > 0)
        {
            angleToRot *= -1;
        }
        Quaternion quat = Quaternion.AngleAxis(angleToRot, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, quat, Time.deltaTime);
        

        //Vector3 ChangeTransRotation = new Vector3(playerTrans.position.x, transform.position.y, playerTrans.position.z);

        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(ChangeTransRotation), Time.deltaTime);





    }
}
