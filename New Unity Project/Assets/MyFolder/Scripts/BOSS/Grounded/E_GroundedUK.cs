using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_GroundedUK : Enemy
{
    Coroutine patternCoroutine;
    E_G_UK_AnimationEventEffect animEffect;

    bool isLook;


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
        Vector3 loopPostion = new Vector3(playerTrans.position.x, this.transform.position.y, playerTrans.position.z);
        if (isLook)
        {
            this.transform.LookAt(loopPostion);
        }
    }

    IEnumerator ActionPattern()
    {
        isLook = true;
        animEffect.skillNum = -1;

        // 테스트용
        int random = 0;

        // 실제로 랜덤 패턴 구현할 변수
        //int random = Random.Range(0, 5);

        switch (random)
        {
            case 0:
                StartCoroutine(SlashCombo());
                animEffect.skillNum = 0;
                break;
            case 1:
                StartCoroutine(Firebird());
                animEffect.skillNum = 1;
                break;
            case 2:
                StartCoroutine(Teleport());
                animEffect.skillNum = 2;
                break;
            case 3:
                StartCoroutine(AttackJump());
                break;
            default:
                break;
        }
        yield return new WaitForSeconds(0.1f);
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
    IEnumerator AttackJump()
    {
        anim.SetTrigger("DoAttackJump");

        

        yield return new WaitForSeconds(2f);

        
        StartCoroutine(ActionPattern());
    }
}
