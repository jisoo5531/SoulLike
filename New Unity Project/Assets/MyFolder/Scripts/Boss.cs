using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyTest
{
    float moveSpeed = 15.0f;
    

    public GameObject flame;
    public Transform flamePos;
    public GameObject fireball;

    bool isFlying;

    GameObject flameBressOBJ;
    GameObject fireBallOBJ;
   

    private void Awake()
    {
        anim = GetComponent<Animator>();

        StartCoroutine(Action());

        // 테스트 코루틴
        //StartCoroutine(TestAction());
    }

    private void Update()
    {
        transform.LookAt(target);
        if (flameBressOBJ != null)
        {
            flameBressOBJ.transform.position = flamePos.position;
        }
    }
    /// <summary>
    /// 테스트
    /// </summary>    
    IEnumerator TestAction()
    {
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(AttackClaw());
    }

    /// <summary>
    /// 보스 패턴 랜덤 시작 코루틴
    /// </summary>
    IEnumerator Action()
    {
        isFlying = false;
        int random = Random.Range(0, 3);
        yield return new WaitForSeconds(0.5f);

        if (currentHP < 200)
        {
            StartCoroutine(TakeOff());
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(FlyingAction());
        }
        else
        {
            switch (random)
            {
                case 0:
                    StartCoroutine(AttackBasic());
                    break;
                case 1:
                    StartCoroutine(AttackClaw());
                    break;
                case 2:
                    StartCoroutine(AttackFlame());
                    break;
            }
        }
        
        
    }
    /// <summary>
    /// 2페이즈. 날아다닐 때 패턴 실행
    /// </summary>
    IEnumerator FlyingAction()
    {
        yield return new WaitForSeconds(0.1f);
        isFlying = true;
        

        
        StartCoroutine(AttackFlame());
    }

    IEnumerator TakeOff()
    {
        anim.SetTrigger("DoFly");
        while (true)
        {
            yield return null;

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Take Off"))
            {
                this.transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            }
            else
            {
                break;
            }
        }
        
    }

    /// <summary>
    /// 기본공격
    /// </summary>
    IEnumerator AttackBasic()
    {
        anim.SetTrigger("DoBasicAttack");
        yield return new WaitForSeconds(3.0f);
        StartCoroutine(Action());
    }
    /// <summary>
    /// 돌진 공격
    /// 테스트 코루틴 수정 필요
    /// </summary>
    IEnumerator AttackClaw()
    {
        
        anim.SetTrigger("DoDashAttack");
        Rigidbody rigid = GetComponent<Rigidbody>();

        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("DashAttack"))
            {
                yield return new WaitForSeconds(1.0f);
                rigid.AddForce(this.transform.forward * 25.0f, ForceMode.Impulse);
                rigid.AddForce(this.transform.up * 1.0f, ForceMode.Impulse);

                
            }
            else
            {
                break;
            }
        }

        StartCoroutine(Action());
    }
    /// <summary>
    /// 파이어 브레스
    /// 테스트 coroutine 수정 필요
    /// </summary>
    IEnumerator AttackFlame()
    {
        yield return new WaitForSeconds(0.5f);
        if (isFlying)
        {
            anim.SetTrigger("DoFlyFlame");
        }
        else
        {
            anim.SetTrigger("DoFlameAttack");
        }
        flameBressOBJ = Instantiate(flame, flamePos.position, flamePos.rotation);

        yield return new WaitForSeconds(3.0f);
   
        Destroy(flameBressOBJ);

        if (isFlying)
        {
            StartCoroutine(FlyingAction());
        }
        else
        {
            StartCoroutine(Action());
            //anim.SetTrigger("DoFlameAttack");
        }
        
    }
    /// <summary>
    /// 맵에 남는 불덩어리 소환
    /// </summary>
    IEnumerator SpawnObsFire()
    {
        anim.SetTrigger("DoFlameAttack");
        int count = 0;
        while (count < 50)
        {
            for (int i = 0; i < 5; i++)
            {
                
                Vector3 spawnPos = new Vector3(Random.Range(-80.0f, 80.0f), 50, Random.Range(-80.0f, 80.0f));
                yield return new WaitForSeconds(0.3f);
                fireBallOBJ = Instantiate(fireball, spawnPos, fireball.transform.rotation);
                count++;
            }
        }
    }
    /// <summary>
    /// 메테오 소환
    /// </summary>
    IEnumerator FireBall()
    {
        anim.SetTrigger("DoFlameAttack");
        int count = 0;
        while (count < 50)
        {
            for (int i = 0; i < 5; i++)
            {
                
                Vector3 spawnPos = new Vector3(Random.Range(-80.0f, 80.0f), 50, Random.Range(-80.0f, 80.0f));
                yield return new WaitForSeconds(0.3f);
                fireBallOBJ = Instantiate(fireball, spawnPos, fireball.transform.rotation);
                count++;
            }
        }
    }
}
