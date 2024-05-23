using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform bulletPos;

    public void Use()
    {
        StartCoroutine(BulletMove());
    }

    IEnumerator BulletMove()
    {

        //GameObject instantBullet = Instantiate(bullet, bulletPos.position, bullet.transform.rotation);
        var instantBullet = ObjectPoolManager.instance.Pool.Get();
        instantBullet.transform.position = bulletPos.transform.position;
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50.0f;

        yield return null;
    }
}
