using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_RidingUK : Enemy
{
    private void Awake()
    {
        anim = GetComponent<Animator>();

        DrawSword();
    }


    void DrawSword()
    {
        anim.SetTrigger("DoDrawSword");
    }
}
