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
        
        
        Vector3 lookPostion = new Vector3(playerTrans.position.x, this.transform.position.y, playerTrans.position.z);
        if (isLook)
        {
            
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(lookPostion), 10.0f * Time.deltaTime);
            
            //this.transform.LookAt(lookPostion);
        }
    }

    IEnumerator ActionPattern()
    {
        yield return new WaitForSeconds(1f);

        animEffect.skillNum = -1;

        // 테스트용
        


        // 실제로 랜덤 패턴 구현할 변수
        int random = Random.Range(0, 3);

        StartCoroutine(AttackJump());
        //animEffect.skillNum = 2;

        //// 실제 패턴 구현
        //if (distanceToPlayer < 13.0f)
        //{
        //    switch (random)
        //    {
        //        case 0:
        //            StartCoroutine(SlashCombo());
        //            animEffect.skillNum = 0;
        //            break;
        //        case 1:
        //            StartCoroutine(AttackJump());                    
        //            break;
        //        case 2:
        //            StartCoroutine(Firebird());
        //            animEffect.skillNum = 1;
        //            break;
        //        default:
        //            break;
        //    }
        //}
        //else
        //{            
        //    StartCoroutine(Teleport());
        //    animEffect.skillNum = 2;
        //}

    }
    void Slash()
    {
        anim.SetTrigger("DoSlash1");

        StartCoroutine(ActionPattern());
    }

    IEnumerator BackJump()
    {
        anim.SetTrigger("DoBackJump");
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ActionPattern());
    }

    IEnumerator SlashCombo()
    {
        anim.SetTrigger("DoSlashCombo");
        weapon.Use();

        yield return new WaitForSeconds(4f);

        StartCoroutine(ActionPattern());
    }
    IEnumerator Firebird()
    {

        anim.SetTrigger("DoBackJumpFireBird");




        yield return new WaitForSeconds(4f);

        StartCoroutine(ActionPattern());
    }
    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(1f);

        anim.SetTrigger("DoTeleport");                

        yield return new WaitForSeconds(3f);

        StartCoroutine(ActionPattern());
    }
    IEnumerator AttackJump()
    {
        if (distanceToPlayer > 10f && distanceToPlayer < 15f)
        {
            anim.SetTrigger("DoAttackJump");
        }
        else if (distanceToPlayer <= 10f)
        {
            anim.SetTrigger("DoBackAndJumpAttack");            
        }


        

        yield return new WaitForSeconds(2f);        

        
        StartCoroutine(ActionPattern());
    }
}
