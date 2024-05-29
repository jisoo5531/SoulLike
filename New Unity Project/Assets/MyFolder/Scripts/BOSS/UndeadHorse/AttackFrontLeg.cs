using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFrontLeg : MonoBehaviour
{
    public int damage = 20;
    Player player;
    BoxCollider frontLegRange;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        frontLegRange = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("플레이어 발 맞았다.");
            player.HP -= damage;
            frontLegRange.enabled = false;
        }
    }
}
