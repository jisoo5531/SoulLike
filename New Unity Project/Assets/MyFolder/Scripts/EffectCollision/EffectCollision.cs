using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCollision : MonoBehaviour
{
    Player player;
    ParticleSystem p_System;
    E_G_UK_AnimationEventEffect particleInfo;

    List<ParticleSystem.Particle> collision_Obj = new List<ParticleSystem.Particle>();

    bool isColided = false;

    private void Awake()
    {
        player = FindObjectOfType<Player>();

        p_System = GetComponent<ParticleSystem>();
        particleInfo = FindObjectOfType<E_G_UK_AnimationEventEffect>();


        p_System.trigger.AddCollider(player.transform);

        isColided = false;

    }


    private void OnParticleTrigger()
    {

        int numInside = p_System.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, collision_Obj);


    }
    private void OnParticleCollision(GameObject other)
    {
        if (!isColided && other.tag == "Player")
        {
            Debug.Log("플레이어 이펙트 맞았다.");
            Debug.LogFormat("파티클 번호 : {0}, 데미지 : {1}", particleInfo.skillNum, particleInfo.effect_slots[particleInfo.skillNum].damage);

            if (player.isBlocking)
            {
                Debug.Log("플레이어 방패로 스킬 막았다.");
                player.HP -= particleInfo.effect_slots[particleInfo.skillNum].damage * 0.75f;
            }
            else
            {
                player.HP -= particleInfo.effect_slots[particleInfo.skillNum].damage;
            }

            isColided = true;
        }
    }
}
