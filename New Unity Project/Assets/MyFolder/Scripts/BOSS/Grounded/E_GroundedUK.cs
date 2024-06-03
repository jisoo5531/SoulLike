using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_GroundedUK : Enemy
{
    List<Skill> skillList;
    E_G_UK_AnimationEventEffect animEffect;
    Coroutine patternCoroutine;
    Coroutine currentSkillCoroutine;

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

        if (!isPlaying)
        {
            Skill nextSkill = null;
            int priority = int.MaxValue;
            foreach (Skill skill in skillList)
            {
                if (skill.isReady() && skill.SkillPriority < priority)
                {
                    priority = skill.SkillPriority;
                    nextSkill = skill;
                }
            }

            if (nextSkill != null)
            {
                ExcuteSkill(nextSkill);
            }
        }

        #endregion

        distanceToPlayer = Vector3.Magnitude(playerTrans.localPosition - this.transform.localPosition);

        Vector3 lookPosition = new Vector3(playerTrans.position.x, this.transform.position.y, playerTrans.position.z);
        if (isLook)
        {
            this.transform.LookAt(lookPosition);
        }
    }

    void ExcuteSkill(Skill skill)
    {
        Debug.Log("현재 스킬 : " + skill.SkillName);
        if (currentSkillCoroutine != null)
        {
            StopCoroutine(currentSkillCoroutine);
        }

        isPlaying = true;
        switch (skill.SkillName)
        {
            case "Basic Slash":
                currentSkillCoroutine = StartCoroutine(Slash(skill));
                break;
            case "Slash Combo":
                currentSkillCoroutine = StartCoroutine(SlashCombo(skill));
                break;
            case "Fire Bird":
                currentSkillCoroutine = StartCoroutine(Firebird(skill));
                break;
            case "Teleport":
                currentSkillCoroutine = StartCoroutine(Teleport(skill));
                break;
        }
    }

    void FinishSkillExecution(Skill skill)
    {
        isPlaying = false;
        skill.SkillCurrentCoolTime = skill.SkillCoolTime;
    }

    IEnumerator Slash(Skill skill)
    {
        animEffect.skillNum = 0;
        anim.SetTrigger("DoSlash1");

        #region Sound
        yield return new WaitForSeconds(0.5f);
        SoundManager.instance.PlaySoundEffect("Slash");
        yield return new WaitForSeconds(2f);
        SoundManager.instance.StopSoundEffect("Slash");
        #endregion

        yield return new WaitForSeconds(1f);

        FinishSkillExecution(skill);
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
    }

    IEnumerator BackJump()
    {
        anim.SetTrigger("DoBackJump");
        yield return new WaitForSeconds(0.5f);
        //StartCoroutine(ActionPattern());
    }

    IEnumerator SlashCombo(Skill skill)
    {
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

        FinishSkillExecution(skill);
    }

    IEnumerator Firebird(Skill skill)
    {
        animEffect.skillNum = 1;
        Debug.Log("firebird");
        anim.SetTrigger("DoBackJumpFireBird");

        yield return new WaitForSeconds(10f);

        FinishSkillExecution(skill);
    }

    IEnumerator Teleport(Skill skill)
    {
        animEffect.skillNum = 2;
        Debug.Log("teleport");

        anim.SetTrigger("DoTeleport");

        yield return new WaitForSeconds(6f);

        FinishSkillExecution(skill);
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

        //StartCoroutine(ActionPattern());
    }
}
