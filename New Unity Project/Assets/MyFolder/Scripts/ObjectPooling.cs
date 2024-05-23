using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public int pointer;

    public List<GameObject> bulletPool;
    public GameObject bullet;

    private void Awake()
    {
        pointer = 0;
        int size = 30;
        bulletPool = new List<GameObject>();

        for (int i = 0; i < size; i++)
        {
            GameObject obj = Instantiate(bullet, bullet.transform.position, bullet.transform.rotation);
            
            obj.SetActive(false);
            obj.transform.parent = this.transform;
            bulletPool.Add(obj);
        }
    }

    public void SpawnObj()
    {
        if (pointer != bulletPool.Count)
        {
            bulletPool[pointer].SetActive(true);
            pointer++;
        }
        else
        {
            pointer = 0;
            bulletPool[pointer].SetActive(true);
            pointer++;
        }
    }
}
