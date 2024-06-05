using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    
    void Start()
    {
        //Time.timeScale = 0.3f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

   
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Time.timeScale = 0.4f;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1f;
        }
    }
}
