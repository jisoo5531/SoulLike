using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCollision : MonoBehaviour
{
    GameObject player;
    ParticleSystem p_System;
    List<ParticleSystem.Particle> collision_Obj = new List<ParticleSystem.Particle>();

    

    private void Awake()
    {
        player = GameObject.Find("Player");


        p_System = GetComponent<ParticleSystem>();
        

        p_System.trigger.AddCollider(player.transform);
        


    }


    private void OnParticleTrigger()
    {

        int numInside = p_System.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, collision_Obj);

        


        
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("플레이어 이펙트 맞았다.");
        }
    }
}
