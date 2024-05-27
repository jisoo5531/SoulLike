using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_GroundedUK : Enemy
{
    Coroutine patternCoroutine;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        patternCoroutine = StartCoroutine(ActionPattern());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ActionPattern()
    {
        while (true)
        {
            yield return null;
            int random = 0;
            //int random = Random.Range(0, 5);

            switch (random)
            {
                case 0:
                    StartCoroutine(SlashCombo());
                    break;
                default:
                    break;
            }
        }
    }
    IEnumerator SlashCombo()
    {
        anim.SetTrigger("DoSlashCombo");

        yield return new WaitForSeconds(7f);
    }
}
