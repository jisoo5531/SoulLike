using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    ObjectPooling instancePool;



    private void Awake()
    {
        instancePool = FindObjectOfType<ObjectPooling>();
    }

    public void Shooting()
    {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            instancePool.SpawnObj();
            yield return null;
        }
    }
}
