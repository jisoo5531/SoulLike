using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public enum Type { Melee, Range };
    public Type type;
    public float sword_Damage;
    public float attackSpeed;
    public BoxCollider meleeArea;

    Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();

        sword_Damage = 15;
    }
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

        yield return new WaitForSeconds(2f);
        meleeArea.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            Debug.Log("플레이어 칼맞았다.");
            Debug.Log("칼 데미지 : " + sword_Damage);

            player.HP -= sword_Damage;

            meleeArea.enabled = false;
        }
    }
}
