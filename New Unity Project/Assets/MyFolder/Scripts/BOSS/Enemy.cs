using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region 전역 변수

    public float HP = 200;
    public float MaxHP = 200;

    #endregion

    [HideInInspector]
    public Animator anim;

    public Transform playerTrans;

    [HideInInspector]
    public Player player;

    [HideInInspector]
    public EnemyWeapon weapon;

    public float Get_HP()
    {
        return HP;
    }
    public float Get_MaxHP()
    {
        MaxHP = 200;
        return MaxHP;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerMelee")
        {
            HP -= FindObjectOfType<PlayerWeapon>().damage;
            Debug.Log("맞았다. 체력 : " + HP);
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
