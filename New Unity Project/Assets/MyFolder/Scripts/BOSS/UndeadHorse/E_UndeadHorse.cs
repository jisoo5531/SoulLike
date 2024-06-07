using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_UndeadHorse : Enemy
{
    #region 전역 변수

    public float moveSpeed = 10.0f;
    public float bodyCrushDamage = 15.0f;
    public float detectionRange;
    public LayerMask playerLayer;

    float distancePlayer;

    bool isLook;

    BoxCollider horseColider;
    BoxCollider Attack_Sprint_Jump;

    #endregion


    #region 트랜스폼 / 스크립트




    #endregion

    bool isTurnning;


    private void Awake()
    {
        detectionRange = 100f;

        HP = 100;
        MaxHP = 100;

        BoxCollider Attack_Sprint_Jump = transform.GetChild(3).GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();
        horseColider = GetComponent<BoxCollider>();
        player = FindObjectOfType<Player>();

        isLook = true;
        isTurnning = false;
        if (Attack_Sprint_Jump != null)
        {            
            Attack_Sprint_Jump.enabled = true;
        }

        StartCoroutine(StartAction());

    }

    void Update()
    {
        distancePlayer = Vector3.Magnitude(playerTrans.position - this.transform.position);
        //Debug.Log(distancePlayer);

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
            toRotation.x = toRotation.z = 0;
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, toRotation, 50.0f * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        int random = Random.Range(0, 4);
        if (other.tag == "Player")
        {
            Debug.Log("플레이어 부딪힘");
            horseColider.enabled = false;
            player.HP -= bodyCrushDamage;

            if (random == 0)
            {
                player.StartMethod(3);
            }
        }
        else if (other.tag == "PlayerMelee")
        {
            HP -= FindObjectOfType<PlayerWeapon>().damage;
            Debug.Log("맞았다. 체력 : " + HP);
            Damaged();
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
            anim.SetBool("TurnLeft", false);
            anim.SetBool("TurnRight", false);
        }
    }
    IEnumerator CheckForPlayer()
    {
        anim.SetBool("isTurnRunning", true);
        isTurnning = true;

        Debug.Log("Turn 시작");
        while (true)
        {
            Debug.Log("감지 중");
            Vector3 rayPosition = this.transform.position + new Vector3(0, 1, 0);
            Ray ray = new Ray(rayPosition, transform.forward);
            RaycastHit hit;

            Debug.DrawRay(rayPosition, transform.forward * detectionRange, Color.red);

            if (Physics.Raycast(ray, out hit, detectionRange, playerLayer))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    anim.SetBool("isTurnRunning", false);
                    isTurnning = false;
                    Debug.Log("플레이어 감지");

                    break;
                }
            }
            yield return null;
        }
    }

    IEnumerator StartAction()
    {
        yield return new WaitForSeconds(1f);

        anim.SetTrigger("StartAction");

        yield return new WaitForSeconds(1f);

        StartCoroutine(HorseActionPattern());
    }

    IEnumerator HorseActionPattern()
    {

        yield return new WaitForSeconds(3f);

        Debug.Log("패턴 테스트");


        if (!horseColider.enabled)
        {
            horseColider.enabled = true;
        }

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
        Debug.Log("점프 공격 테스트");
        if (distancePlayer < 30.0f && distancePlayer > 15.0f)
        {
            Debug.Log("점프 공격 한다.");
            anim.SetTrigger("DoSprintJump");
            Attack_Sprint_Jump.enabled = true;
        }


        StartCoroutine(CheckForPlayer());


        yield return new WaitUntil(() => !isTurnning);

        Debug.Log("turn 끝");
        
        Debug.Log("점프 콜라이더 테스트");

        StartCoroutine(HorseActionPattern());
    }
}
