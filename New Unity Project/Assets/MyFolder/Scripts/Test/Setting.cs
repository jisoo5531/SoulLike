using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    public float gameTime;

    private void Awake()
    {
        gameTime = 0.0f;
    }


    void Update()
    {
        gameTime += Time.deltaTime;
    }
}
