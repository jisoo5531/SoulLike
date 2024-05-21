using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    float moveSpeed = 5.0f;

    public GameObject flame;
    public Transform flamePos;

    GameObject obj;
   
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        //StartCoroutine(Action());
        StartCoroutine(TestAction());
    }

    private void Update()
    {
        if (obj != null)
        {
            obj.transform.position = flamePos.position;
        }
    }

    IEnumerator TestAction()
    {
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(Flying());
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
        anim.SetTrigger("DoFlameAttack");
        obj = Instantiate(flame, flamePos.position, flamePos.rotation);

        yield return new WaitForSeconds(3.0f);
   
        Destroy(obj);

        StartCoroutine(Action());
    }
    IEnumerator Flying()
    {
        anim.SetTrigger("DoFly");

        while (true)
        {
            yield return null;

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Take Off"))
            {
                Debug.Log("³¯¾Ò´Ù!~");
                
                this.transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            }
            else
            {
                break;
            }
        }
        


        

    }
}
