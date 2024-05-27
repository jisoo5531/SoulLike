using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Effect
{
    public GameObject[] effct;
}

public class TestEffectScript : MonoBehaviour
{
    public Effect c_Effect;
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosXZ = new Vector3(target.position.x, transform.position.y, target.position.z);
        Vector3 inputVec = new Vector3(Input.GetAxis("Horizontal") * -1.0f, 0, Input.GetAxis("Vertical") * -1.0f);
        transform.LookAt(targetPosXZ + inputVec);
        
        

        if (Input.GetKeyDown("1"))
        {
            GameObject g_Effect = Instantiate(c_Effect.effct[0], transform.position, transform.rotation);
            Destroy(g_Effect, 10f);
        }
        if (Input.GetKeyDown("2"))
        {
            GameObject g_Effect = Instantiate(c_Effect.effct[1], transform.position, transform.rotation);
            Destroy(g_Effect, 10f);
        }
        if (Input.GetKeyDown("3"))
        {
            GameObject g_Effect = Instantiate(c_Effect.effct[2], transform.position, transform.rotation);
            Destroy(g_Effect, 10f);
        }
        if (Input.GetKeyDown("4"))
        {
            GameObject g_Effect = Instantiate(c_Effect.effct[3], transform.position, transform.rotation);
            Destroy(g_Effect, 10f);
        }
        if (Input.GetKeyDown("5"))
        {
            GameObject g_Effect = Instantiate(c_Effect.effct[4], transform.position, transform.rotation);
            Destroy(g_Effect, 10f);
        }
    }
}
