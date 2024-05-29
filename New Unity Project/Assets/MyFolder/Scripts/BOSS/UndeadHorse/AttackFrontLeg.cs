using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFrontLeg : MonoBehaviour
{
    public int damage = 20;
    Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("플레이어 발 맞았다.");
            player.HP -= damage;
        }
    }
}
