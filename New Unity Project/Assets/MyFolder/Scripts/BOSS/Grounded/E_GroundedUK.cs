using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_GroundedUK : Enemy
{
    float distanceToPlayer;

    Coroutine patternCoroutine;
    E_G_UK_AnimationEventEffect animEffect;

    bool isLook = true;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        weapon = GetComponentInChildren<EnemyWeapon>();
        animEffect = GetComponent<E_G_UK_AnimationEventEffect>();

        patternCoroutine = StartCoroutine(ActionPattern());
    }

    // Update is called once per frame
    void Update()
    {

        distanceToPlayer = Vector3.Magnitude(playerTrans.localPosition - this.transform.localPosition);
        //Debug.Log("거리 : " + distanceToPlayer);
        
        
        Vector3 loopPostion = new Vector3(playerTrans.position.x, this.transform.position.y, playerTrans.position.z);
        if (isLook)
        {
            this.transform.LookAt(loopPostion);
        }
    }

    IEnumerator ActionPattern()
    {
        yield return new WaitForSeconds(1f);

        animEffect.skillNum = -1;

        // 테스트용

        // 실제로 랜덤 패턴 구현할 변수
        int random = Random.Range(0, 3);

        if (distanceToPlayer < 13.0f)
        {
            switch (random)
            {
                case 0:
                case 1:
                    StartCoroutine(SlashCombo());
                    animEffect.skillNum = 0;
                    break;
                case 2:
                    StartCoroutine(Firebird());
                    animEffect.skillNum = 1;
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (random)
            {
                case 0:
                case 1:
                    StartCoroutine(AttackRunJump());
                    break;
                case 2:
                    StartCoroutine(Teleport());
                    animEffect.skillNum = 2;
                    break;
                default:
                    break;
            }
        }
               
    }
    IEnumerator SlashCombo()
    {
        anim.SetTrigger("DoSlashCombo");
        weapon.Use();

        yield return new WaitForSeconds(5f);

        StartCoroutine(ActionPattern());
    }
    IEnumerator Firebird()
    {
        anim.SetTrigger("DoFireBird");        

        yield return new WaitForSeconds(6f);

        StartCoroutine(ActionPattern());
    }
    IEnumerator Teleport()
    {        
        anim.SetTrigger("DoTeleport");                

        yield return new WaitForSeconds(3f);

        StartCoroutine(ActionPattern());
    }
    IEnumerator AttackRunJump()
    {
        while (true)
        {
            yield return null;
            if (distanceToPlayer > 13.0f)
            {
                anim.SetBool("isRun", true);
            }
            else
            {
                anim.SetBool("isRun", false);
                break;
            }
        }
        anim.SetTrigger("DoAttackJump");
        
        
        yield return new WaitForSeconds(2f);        

        
        StartCoroutine(ActionPattern());
    }
}
