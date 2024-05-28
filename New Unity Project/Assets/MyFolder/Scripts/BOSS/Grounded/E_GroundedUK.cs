using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_GroundedUK : Enemy
{
    Coroutine patternCoroutine;
    E_G_UK_AnimationEventEffect animEffect;


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
        
    }

    IEnumerator ActionPattern()
    {
        animEffect.skillNum = -1;
        int random = 0;
        //int random = Random.Range(0, 5);

        switch (random)
        {
            case 0:
                StartCoroutine(SlashCombo());
                animEffect.skillNum = 0;
                break;
            case 1:

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
    IEnumerator SwordEmission()
    {
        yield return new WaitForSeconds(0.1f);
    }
}
