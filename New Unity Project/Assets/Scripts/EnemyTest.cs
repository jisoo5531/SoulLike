using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTest : MonoBehaviour
{
    public float maxHP;
    public float currentHP;

    public Transform target;

    public Animator anim;

    Rigidbody rigid;
    BoxCollider collider;
    Material mat;
    NavMeshAgent nav;

    Vector3 dest;


    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();
        mat = GetComponent<MeshRenderer>().material;
        nav = GetComponent<NavMeshAgent>();
        dest = nav.destination;
    }

    void Update()
    {
        dest = target.position;
        nav.destination = dest;
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
