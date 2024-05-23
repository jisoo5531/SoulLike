using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    float moveSpeed = 5.0f;

    public GameObject flame;
    public Transform flamePos;
    public GameObject fireball;

    bool isFlying;

    GameObject flameBressOBJ;
    GameObject fireBallOBJ;
   
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        //StartCoroutine(Action());

        // 테스트 코루틴
        StartCoroutine(TestAction());
    }

    private void Update()
    {
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
        StartCoroutine(FireBall());
    }

    /// <summary>
    /// 보스 패턴 랜덤 시작 코루틴
    /// </summary>
    IEnumerator Action()
    {
        isFlying = false;
        int random = Random.Range(0, 3);
        yield return new WaitForSeconds(0.5f);

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
    /// <summary>
    /// 2페이즈. 날아다닐 때 패턴 실행
    /// </summary>
    IEnumerator FlyingAction()
    {
        isFlying = true;
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
        StartCoroutine(AttackFlame());
    }

    /// <summary>
    /// 기본공격
    /// </summary>
    IEnumerator AttackBasic()
    {
        anim.SetTrigger("DoClawAttack");
        yield return new WaitForSeconds(3.0f);
        StartCoroutine(Action());
    }
    /// <summary>
    /// 돌진 공격
    /// </summary>
    IEnumerator AttackClaw()
    {
        
        anim.SetTrigger("DoBasicAttack");
        yield return new WaitForSeconds(2.0f);
        StartCoroutine(Action());
    }
    /// <summary>
    /// 파이어 브레스
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
            //anim.SetTrigger("DoFlameAttack");
        }
        //StartCoroutine(Action());
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
