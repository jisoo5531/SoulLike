using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public enum Type { Melee, Range };
    public Type type;
    public float damage = 10;
    public float attackSpeed;
    public BoxCollider meleeArea;
        

    public void Use()
    {
        if (type == Type.Melee)
        {
            StartCoroutine(Swing());
        }
    }

    IEnumerator Swing()
    {
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;                

    }
    private void OnTriggerEnter(Collider other)
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        if (other.tag == "Player")
        {
            Debug.Log("플레이어 칼맞았다.");

            player.HP -= damage;

            meleeArea.enabled = false;
        }
    }
}
