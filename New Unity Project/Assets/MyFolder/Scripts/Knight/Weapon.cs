using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Range };
    public Type type;
    public float damage;
    public float attackSpeed;
    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;       // 공격 이펙트 테스트

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
