using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{

    public IObjectPool<GameObject> Pool { get; set; }
    public float damage;
    float lifeTime = 20.0f;

    void OnTriggerEnter(Collider other)
    {
        float currentGameTime = FindObjectOfType<Setting>().gameTime;
        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Floor" || )
        {
            //Destroy(this.gameObject);
            Pool.Release(this.gameObject);
        }
    }
}
