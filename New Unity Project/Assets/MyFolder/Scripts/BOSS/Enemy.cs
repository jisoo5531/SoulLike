using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
/// <summary>
/// 스킬 정보를 담고 있는 클래스
/// 쿨타임, 우선순위를 고려해 다음 실행할 스킬 결정
/// </summary>
public class Skill
{
    // 우선 순위, 쿨타임
    private string name { get; }
    private float coolTime { get; }
    private float currentCoolTime { get; set; }
    private int priority { get; }

    public Skill(string _name, float _coolTime, int _priority, bool _isPlaying = false)
    {
        this.name = _name;
        this.coolTime = _coolTime;
        this.currentCoolTime = 0;
        this.priority = _priority;
    }

    public string SkillName { get { return name; } }
    public float SkillCoolTime { get { return coolTime; } }
    public int SkillPriority { get { return priority; } }
    public float SkillCurrentCoolTime
    {
        get
        {
            return currentCoolTime;
        }
        set
        {
            if (value <= 0)
            {
                Debug.LogFormat("{0} 스킬 준비 완료", name);
            }
            else
            {
                currentCoolTime = value;
            }
        }
    }

    public void currentCoolTimeUpdate(float deltaTime)
    {
        if (currentCoolTime > 0)
        {
            currentCoolTime -= deltaTime;
        }
    }

    public bool isReady()
    {
        if (this.currentCoolTime <= 0)
        {
            return true;
        }
        return false;
    }
}

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

    public void Damaged()
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
