using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject flame;
    public Transform flamePos;
   
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(Action());
    }

    IEnumerator Action()
    {

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
    IEnumerator AttackBasic()
    {
        
        anim.SetTrigger("DoClawAttack");
        yield return new WaitForSeconds(3.0f);
        StartCoroutine(Action());
    }
    IEnumerator AttackClaw()
    {
        
        anim.SetTrigger("DoBasicAttack");
        yield return new WaitForSeconds(2.0f);
        StartCoroutine(Action());
    }
    IEnumerator AttackFlame()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("DoFlameAtt");
        GameObject obj = Instantiate(flame, flamePos.position, flamePos.rotation);
        yield return new WaitForSeconds(3.0f);
        yield return new WaitForSeconds(3.0f);

        yield return new WaitForSeconds(3.0f);
        Destroy(obj);
        //anim.SetBool("DoFlameAttack", true);




        //anim.SetBool("DoFlameAttack", false);



        StartCoroutine(Action());
    }
}
