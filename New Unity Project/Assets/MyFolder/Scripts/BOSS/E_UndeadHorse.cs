using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_UndeadHorse : Enemy
{
    #region 전역 변수

    public float moveSpeed = 10.0f;

    Coroutine turnCoroutine;
    Coroutine moveCoroutine;

    #endregion


    #region 트랜스폼 / 스크립트

    


    #endregion



    private void Awake()
    {
        anim = GetComponent<Animator>();


    }


    void Update()
    {
        // 보스 회전 테스트
        if (Input.GetKeyDown("1"))
        {
            turnCoroutine = StartCoroutine(TurnCoroutine());
        }
        if (Input.GetKeyDown("2"))
        {
            moveCoroutine = StartCoroutine(MoveCoroutine());
        }

    }



    IEnumerator MoveCoroutine()
    {
        float movePow = 0.0f;
        //this.transform.position = Vector3.MoveTowards(this.transform.position, playerTrans.position, moveSpeed * Time.deltaTime);

        while (true)
        {
            yield return null;

            Vector3 distance = playerTrans.position - this.transform.position;

            movePow = Mathf.Lerp(movePow, 1f, 2.0f * Time.deltaTime);
            anim.SetFloat("MovePow", movePow);


            this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            if (distance.magnitude <= 10)
            {
                break;
            }
        }
        anim.SetFloat("MovePow", 0f);
    }

    //IEnumerator MoveAnimation()
    //{
    //    while (true)
    //    {
    //        yield return null;

    //        movePow = Mathf.Lerp(movePow, 0f, Time.deltaTime);
    //        anim.SetFloat("MovePow", movePow);
    //    }
    //}



    /// <summary>
    /// 캐릭터 방향에 따라 보스 회전
    /// **** 수정 ****
    /// 오른쪽 왼쪽 회전 원래대로 import
    /// </summary>
    /// <returns></returns>
    IEnumerator TurnCoroutine()
    {
        Vector3 ToTargetVec = playerTrans.position - transform.position;
        ToTargetVec = transform.InverseTransformDirection(ToTargetVec);
        ToTargetVec.Normalize();

        Vector2 ToTargetV2 = new Vector2(ToTargetVec.x, ToTargetVec.y);

        float angleToRot = Vector2.Angle(Vector2.up, ToTargetV2);
        if (ToTargetV2.x < 0)
        {
            angleToRot *= -1;
        }
        anim.SetFloat("MoveRotation", angleToRot);

        Vector3 changeRotation = new Vector3(playerTrans.position.x, transform.position.y, playerTrans.position.z);
        transform.LookAt(changeRotation);

        //while (true)
        //{
        //    yield return null;
        //    Vector3 changeRotation = new Vector3(playerTrans.position.x, transform.position.y, playerTrans.position.z);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(changeRotation), Time.deltaTime);

        //    RaycastHit hit;
        //    if (Physics.Raycast(transform.position, transform.forward, out hit))
        //    {
        //        if (hit.collider.tag == "Player")
        //        {
        //            break;
        //        }
        //    }
        //}

        //transform.LookAt(changeRotation);

        yield return new WaitForSeconds(1f);



        anim.SetFloat("MoveRotation", 0f);

        yield return new WaitForSeconds(1f);
    }
}
