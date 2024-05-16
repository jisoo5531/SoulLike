using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    ObjectPooling objPool;
    public GameObject bullet;
    public Transform bulletPos;

    private void Awake()
    {
        objPool = FindObjectOfType<ObjectPooling>();
    }

    public void Use()
    {
        StartCoroutine(BulletMove());
    }

    IEnumerator BulletMove()
    {
        GameObject instantBullet = Instantiate(bullet, bulletPos.position, bullet.transform.rotation);
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50.0f;
        
        yield return null;
    }
}
