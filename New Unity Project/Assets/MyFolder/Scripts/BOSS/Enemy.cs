using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region 전역 변수

    public float HP;
    public float MaxHP;

    #endregion

    [HideInInspector]
    public Animator anim;
    public Transform playerTrans;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
