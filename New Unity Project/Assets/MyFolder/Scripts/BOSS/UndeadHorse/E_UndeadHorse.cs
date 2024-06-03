using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_UndeadHorse : Enemy
{
    #region 전역 변수

    public float moveSpeed = 10.0f;

    float distancePlayer = 0.0f;

    bool isLook;
    
    Coroutine turnCoroutine;
    Coroutine moveCoroutine;

    BoxCollider horseColider;

    #endregion


    #region 트랜스폼 / 스크립트




    #endregion

   
    private void Awake()
    {
        isLook = false;

        HP = 200;
        MaxHP = 200;

        anim = GetComponent<Animator>();
        horseColider = GetComponent<BoxCollider>();
        player = FindObjectOfType<Player>();

        StartCoroutine(HorseActionPattern());

    }

    void Update()
    {
        distancePlayer = Vector3.Magnitude(playerTrans.position - this.transform.position);

        
        // 수정 필요
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("B 눌렀다.");
            isLook = true;
        }
            
        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("V 눌렀다.");
            isLook = false;
        }

        if (isLook)
        {
            CrossDot();
            Vector3 dir = (playerTrans.position - this.transform.position).normalized;
            Quaternion toRotation = Quaternion.LookRotation(dir);
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, toRotation, 50.0f * Time.deltaTime);            
        }
    }    
    /// <summary>
    /// 외적을 구해 플레이어가 어느 방향에 있는지 구하기
    /// </summary>
    void CrossDot()
    {
        Vector3 forward = this.transform.forward;
        Vector3 dir = (playerTrans.position - this.transform.position).normalized;

        Vector3 cross = Vector3.Cross(forward, dir);

        if (cross.y < 0)
        {
            anim.SetBool("TurnRight", false);
            anim.SetBool("TurnLeft", true);
        }
        else
        {
            anim.SetBool("TurnLeft", false);
            anim.SetBool("TurnRight", true);
        }
        if (cross.y > -0.1 && cross.y < 0.1)
        {
            isLook = false;
            anim.SetBool("TurnLeft", false);
            anim.SetBool("TurnRight", false);
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

    IEnumerator HorseActionPattern()
    {        
        
        //isLook = true;

        //while (true)
        //{
        //    yield return new WaitForSeconds(2f);            

        //    if (distancePlayer <= 10)
        //    {
        //        StartCoroutine(AttackFrontLeg());

        //    }
        //    else if (distancePlayer > 25 && distancePlayer < 31)
        //    {
        //        StartCoroutine(AttackSprintJump());
        //    }

        //    yield return null;
        //}
        yield return new WaitForSeconds(0.1f);
        
        StartCoroutine(AttackSprintJump());
    }

    IEnumerator AttackFrontLeg()
    {        
        BoxCollider Attack_F_Leg = GameObject.Find("AttackFront Pos").GetComponent<BoxCollider>();

        isLook = false;
        anim.SetTrigger("DoAttackFrontLeg");
        Attack_F_Leg.enabled = true;

        yield return new WaitForSeconds(1.5f);

        Attack_F_Leg.enabled = false;

        yield return new WaitForSeconds(1f);

        isLook = true;
        //StartCoroutine(HorseActionPattern());
    }
    
    IEnumerator AttackSprintJump()
    {
        BoxCollider Attack_Sprint_Jump = GameObject.Find("SprintJumpAttack Pos").GetComponent<BoxCollider>();

        isLook = false;
        horseColider.enabled = false;
        anim.SetTrigger("DoSprintJump");

        Attack_Sprint_Jump.enabled = true;

        yield return new WaitForSeconds(1.5f);

        horseColider.enabled = true;
        Attack_Sprint_Jump.enabled = false;

        yield return new WaitForSeconds(1f);

        
        isLook = true;

        StartCoroutine(HorseActionPattern());
    }
}
