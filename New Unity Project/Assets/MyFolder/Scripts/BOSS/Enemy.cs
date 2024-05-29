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

    [HideInInspector]
    public Player player;
    public EnemyWeapon weapon;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerMelee")
        {
            HP -= FindObjectOfType<PlayerWeapon>().damage;
            Damaged();
        }
    }

    void Damaged()
    {
        FadeInOut fadeIO = FindObjectOfType<FadeInOut>();
        ChangeScene scene = FindObjectOfType<ChangeScene>();
        
        if (HP <= 0)
        {
            fadeIO.StartFadeOut();
            scene.StartChangeScene();
        }
    }
}
