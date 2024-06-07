using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSprintJump : MonoBehaviour
{
    Player player;

    public float damage = 10;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            player.HP -= damage;
            Debug.Log("플레이어 점프 공격 맞음");
            player.StartMethod(3);
        }
    }
}
