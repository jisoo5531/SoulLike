using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_GroundedUK : Enemy
{
    Coroutine patternCoroutine;
    

    private void Awake()
    {
        anim = GetComponent<Animator>();
        weapon = GetComponentInChildren<EnemyWeapon>();

        patternCoroutine = StartCoroutine(ActionPattern());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ActionPattern()
    {
        
        int random = 0;
        //int random = Random.Range(0, 5);

        switch (random)
        {
            case 0:
                StartCoroutine(SlashCombo());
                Debug.Log(0);
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
}
