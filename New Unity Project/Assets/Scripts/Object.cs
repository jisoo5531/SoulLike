using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Floor" || other.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
    }
}
