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
        }
    }
}
