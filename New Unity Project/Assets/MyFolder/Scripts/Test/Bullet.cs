using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public IObjectPool<GameObject> Pool { get; set; }
    public float damage;
    float lifeTime;

    private void Awake()
    {
        lifeTime = 0.0f;
    }

    private void Update()
    {
        if (lifeTime >= 10.0f)
        {
            lifeTime = 0.0f;
            Pool.Release(this.gameObject);
        }
        lifeTime += Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Floor")
        {
            if (other.gameObject.tag == "Wall")
            {
                Debug.Log("벽맞았다");
            }
            //Destroy(this.gameObject);
            Pool.Release(this.gameObject);
        }

        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("보스 총맞았다");
        }
    }

    
}
