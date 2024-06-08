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
    bool isRun;
    bool isLook;
    bool isPlaying;

    private void Awake()
    {
        #region Skill List

        skillList = new List<Skill>
        {
            new Skill("Teleport", 35f, 1),
            new Skill("Slash Combo", 12f, 3),
            new Skill("JumpAttack", 13f, 2),
            new Skill("Fire Bird", 15f, 3),
            new Skill("Basic Slash", 3f, 3),
            new Skill("Basic Slash_2", 3f, 3),

            //test            
            //new Skill("Basic Slash_2", 3f, 1)
            //new Skill("PowerUP", 3f, 1)
            //new Skill("Fire Bird", 6f, 1)            
        };

        isLook = true;
        isRun = false;
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
            //Debug.LogFormat("{0} : {1}", currentSkill.SkillName, currentSkill.SkillCurrentCoolTime);
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

        distanceToPlayer = Vector3.Magnitude(playerTrans.position - this.transform.position);

        //Vector3 lookPosition = new Vector3(playerTrans.position.x, this.transform.position.y, playerTrans.position.z);
        
        if (Input.GetKeyDown("p"))
        {
            isLook = !isLook;
        }

        if (isLook)
        {
            Vector3 dir = (playerTrans.position - this.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            lookRotation.x = lookRotation.z = 0;
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, lookRotation, 100.0f * Time.deltaTime);

            //this.transform.LookAt(lookPosition);
        }
    }

    void ExcuteSkill(Skill skill)
    {
        isLook = false;
        isPlaying = true;
        Debug.Log("현재 스킬 : " + skill.SkillName);

        if (currentSkillCoroutine != null)
        {
            StopCoroutine(currentSkillCoroutine);
        }

        switch (skill.SkillName)
        {
            case "Basic Slash":
                currentSkillCoroutine = StartCoroutine(Slash(skill));
                break;
            case "Basic Slash_2":
                currentSkillCoroutine = StartCoroutine(Slash2(skill));
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
            case "JumpAttack":
                currentSkillCoroutine = StartCoroutine(AttackJump(skill));
                break;
        }
    }

    void FinishSkillExecution(Skill skill)
    {
        isPlaying = false;
        skill.SkillCurrentCoolTime = skill.SkillCoolTime;
        isLook = true;
    }

    /// <summary>
    /// 참격 1번
    /// </summary>
    /// <param name="skill"></param>
    /// <returns></returns>
    IEnumerator Slash(Skill skill)
    {

        anim.SetTrigger("DoSlash1");
        animEffect.skillNum = 0;
        weapon.Use();

        yield return new WaitForSeconds(2f);

        FinishSkillExecution(skill);
    }

    /// <summary>
    /// 참격 2번
    /// </summary>
    /// <param name="skill"></param>
    /// <returns></returns>
    IEnumerator Slash2(Skill skill)
    {

        animEffect.skillNum = 0;
        anim.SetTrigger("DoSlash2");
        weapon.Use();


        yield return new WaitForSeconds(3f);

        FinishSkillExecution(skill);
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

        yield return new WaitForSeconds(6f);

        FinishSkillExecution(skill);
    }
    IEnumerator Firebird(Skill skill)
    {

        animEffect.skillNum = 1;
        Debug.Log("firebird");
        anim.SetTrigger("DoBackJumpFireBird");


        yield return new WaitForSeconds(5f);

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
    IEnumerator AttackJump(Skill skill)
    {
        Debug.Log(distanceToPlayer);

        while (true)
        {
            if (distanceToPlayer > 13f)
            {
                isLook = true;
                anim.SetBool("isRun", true);
                isRun = true;
            }
            else
            {
                isLook = false;
                anim.SetBool("isRun", false);
                isRun = false;
                break;
            }

            yield return null;
        }
        
        if (distanceToPlayer > 10f && distanceToPlayer < 13f)
        {
            if (isRun)
            {
                anim.SetBool("isRun", false);
                isRun = false;
            }
            anim.SetTrigger("DoAttackJump");

            yield return new WaitForSeconds(1f);

            weapon.Use();            
        }
        else if (distanceToPlayer <= 10f)
        {
            anim.SetTrigger("DoBackAndJumpAttack");

            yield return new WaitForSeconds(3f);

            weapon.Use();            
        }
        yield return new WaitForSeconds(4f);
        
        FinishSkillExecution(skill);        
    }
    IEnumerator LookAtPlayer(float waitTime)
    {
        isLook = true;

        yield return new WaitForSeconds(waitTime);

        isLook = false;
    }
    IEnumerator DontLookAtPlayer(float waitTime)
    {
        isLook = false;

        yield return new WaitForSeconds(waitTime);

        isLook = true;
    }
}
