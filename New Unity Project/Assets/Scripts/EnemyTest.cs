using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public float maxHP;
    public float currentHP;
    
    public Animator anim;

    Rigidbody rigid;
    BoxCollider collider;
    Material mat;


    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();
        mat = GetComponent<MeshRenderer>().material;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            currentHP -= other.GetComponent<Bullet>().damage;
            StartCoroutine(Damaged());
        }
    }
    IEnumerator Damaged()
    {
        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        if (currentHP > 0)
        {
            mat.color = Color.white;
        }
        else
        {
            mat.color = Color.gray;
            Destroy(this.gameObject, 4.0f);
        }
    }
}
