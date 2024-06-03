using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_GroundedUK : Enemy
{
    List<Skill> skillList;

    E_G_UK_AnimationEventEffect animEffect;
    Coroutine patternCoroutine;
    Coroutine B_SlashCoroutine;
    Coroutine ComboSlashCoroutine;
    Coroutine FireBirdCoroutine;
    Coroutine TeleportCoroutine;

    float distanceToPlayer;
    bool isLook = true;
    bool isPlaying;

    private void Awake()
    {
        #region Skill List

        skillList = new List<Skill>
        {
            new Skill("Basic Slash", 5f, 4),
            new Skill("Slash Combo", 10f, 3),
            new Skill("Fire Bird", 15f, 2),
            new Skill("Teleport", 20f, 1)
        };
        isPlaying = false;

        #endregion

        playerTrans = FindObjectOfType<Player>().transform;
        anim = GetComponent<Animator>();
        weapon = GetComponentInChildren<EnemyWeapon>();
        animEffect = GetComponent<E_G_UK_AnimationEventEffect>();

        //patternCoroutine = StartCoroutine(ActionPattern());
    }

    // Update is called once per frame
    void Update()
    {
        #region Skill List Update        

        foreach (Skill currentSkill in skillList)
        {
            Debug.LogFormat("{0} : {1}", currentSkill.SkillName, currentSkill.SkillCurrentCoolTime);
            if (currentSkill.SkillCurrentCoolTime > 0)
            {
                currentSkill.currentCoolTimeUpdate(Time.deltaTime);
            }
        }

        Skill nextSkill = null;
        int priortiy = int.MaxValue;
        foreach (Skill skill in skillList)
        {
            if (skill.isReady() && skill.SkillPriority < priortiy)
            {
                priortiy = skill.SkillPriority;
                nextSkill = skill;
            }
        }

        if (nextSkill != null && !isPlaying)
        {
            ExcuteSkill(nextSkill);
            //nextSkill.SkillCurrentCoolTime = nextSkill.SkillCoolTime;
        }

        #endregion


        distanceToPlayer = Vector3.Magnitude(playerTrans.localPosition - this.transform.localPosition);
        //Debug.Log("거리 : " + distanceToPlayer);


        Vector3 lookPostion = new Vector3(playerTrans.position.x, this.transform.position.y, playerTrans.position.z);
        if (isLook)
        {

            //this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(lookPostion), 10.0f * Time.deltaTime);

            this.transform.LookAt(lookPostion);
        }
    }

    void ExcuteSkill(Skill skill)
    {
        Debug.Log("현재 스킬 : " + skill.SkillName);
        if (skill.SkillName == "Basic Slash")
        {

            B_SlashCoroutine = StartCoroutine(Slash(skill));

        }
        else if (skill.SkillName == "Slash Combo")
        {
            ComboSlashCoroutine = StartCoroutine(SlashCombo(skill));

        }
        else if (skill.SkillName == "Fire Bird")
        {
            FireBirdCoroutine = StartCoroutine(Firebird(skill));

        }
        else if (skill.SkillName == "Teleport")
        {
            TeleportCoroutine = StartCoroutine(Teleport(skill));
        }
    }

    IEnumerator ActionPattern()
    {
        yield return new WaitForSeconds(1f);

        animEffect.skillNum = -1;

        // 테스트용



        // 실제로 랜덤 패턴 구현할 변수
        int random = Random.Range(0, 3);

        //StartCoroutine(SlashCombo());
        //animEffect.skillNum = 0;

        //StartCoroutine(Teleport());


        //// 실제 패턴 구현
        //if (distanceToPlayer < 13.0f)
        //{
        //    switch (random)
        //    {
        //        case 0:
        //            StartCoroutine(SlashCombo());
        //            animEffect.skillNum = 0;
        //            break;
        //        case 1:
        //            StartCoroutine(AttackJump());                    
        //            break;
        //        case 2:
        //            StartCoroutine(Firebird());
        //            animEffect.skillNum = 1;
        //            break;
        //        default:
        //            break;
        //    }
        //}
        //else
        //{            
        //    StartCoroutine(Teleport());
        //    animEffect.skillNum = 2;
        //}

    }
    IEnumerator Slash(Skill skill)
    {

        isPlaying = true;


        animEffect.skillNum = 0;
        anim.SetTrigger("DoSlash1");

        #region Sound
        yield return new WaitForSeconds(0.5f);
        SoundManager.instance.PlaySoundEffect("Slash");
        yield return new WaitForSeconds(2f);
        SoundManager.instance.StopSoundEffect("Slash");
        #endregion

        yield return new WaitForSeconds(1f);

        isPlaying = false;
        skill.SkillCurrentCoolTime = skill.SkillCoolTime;

        //StartCoroutine(ActionPattern());
    }
    IEnumerator Slash2()
    {

        anim.SetTrigger("DoSlash2");

        #region Sound
        yield return new WaitForSeconds(0.2f);
        SoundManager.instance.PlaySoundEffect("Slash");
        yield return new WaitForSeconds(2f);
        SoundManager.instance.StopSoundEffect("Slash");
        #endregion



        //StartCoroutine(ActionPattern());
    }

    IEnumerator BackJump()
    {
        anim.SetTrigger("DoBackJump");
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ActionPattern());
    }

    IEnumerator SlashCombo(Skill skill)
    {
        isPlaying = true;

        animEffect.skillNum = 0;
        anim.SetTrigger("DoSlashCombo");
        weapon.Use();

        #region Sound
        yield return new WaitForSeconds(0.18f);
        SoundManager.instance.PlaySoundEffect("Slash");
        yield return new WaitForSeconds(0.5f);
        SoundManager.instance.StopSoundEffect("Slash");
        yield return new WaitForSeconds(0.15f);

        SoundManager.instance.PlaySoundEffect("Slash");
        yield return new WaitForSeconds(0.5f);
        SoundManager.instance.StopSoundEffect("Slash");
        yield return new WaitForSeconds(0.1f);

        SoundManager.instance.PlaySoundEffect("Slash");
        yield return new WaitForSeconds(0.3f);
        SoundManager.instance.StopSoundEffect("Slash");
        yield return new WaitForSeconds(0.03f);

        SoundManager.instance.PlaySoundEffect("Slash");
        yield return new WaitForSeconds(0.3f);
        SoundManager.instance.StopSoundEffect("Slash");
        yield return new WaitForSeconds(0.1f);

        SoundManager.instance.PlaySoundEffect("Slash");
        yield return new WaitForSeconds(0.5f);
        SoundManager.instance.StopSoundEffect("Slash");
        #endregion

        yield return new WaitForSeconds(3f);


        isPlaying = false;
        skill.SkillCurrentCoolTime = skill.SkillCoolTime;
        //StartCoroutine(ActionPattern());
    }
    IEnumerator Firebird(Skill skill)
    {
        isPlaying = true;
        animEffect.skillNum = 1;
        Debug.Log("firebird");
        anim.SetTrigger("DoBackJumpFireBird");



        yield return new WaitForSeconds(4f);

        isPlaying = false;
        skill.SkillCurrentCoolTime = skill.SkillCoolTime;

        //StartCoroutine(ActionPattern());
    }
    IEnumerator Teleport(Skill skill)
    {
        isPlaying = true;


        animEffect.skillNum = 2;
        Debug.Log("teleport");

        anim.SetTrigger("DoTeleport");

        yield return new WaitForSeconds(4f);

        isPlaying = false;
        skill.SkillCurrentCoolTime = skill.SkillCoolTime;
        //StartCoroutine(ActionPattern());
    }
    IEnumerator AttackJump()
    {
        if (distanceToPlayer > 10f && distanceToPlayer < 15f)
        {
            anim.SetTrigger("DoAttackJump");
        }
        else if (distanceToPlayer <= 10f)
        {
            anim.SetTrigger("DoBackAndJumpAttack");
        }




        yield return new WaitForSeconds(2f);


        StartCoroutine(ActionPattern());
    }
}
