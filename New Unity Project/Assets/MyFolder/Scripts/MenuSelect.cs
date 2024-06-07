using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelect : MonoBehaviour
{
    Transform StartGame;
    Transform ExitGame;

    bool isStart;
    bool isExit;

    private void Awake()
    {
        StartGame = transform.GetChild(1).GetChild(0);
        ExitGame = transform.GetChild(2).GetChild(0);

        isStart = true;
        isExit = false;        
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            isStart = !isStart;
            isExit = !isExit;
        }
        if (isStart)
        {
            StartGame.gameObject.SetActive(true);
            ExitGame.gameObject.SetActive(false);
        }
        if (isExit)
        {
            StartGame.gameObject.SetActive(false);
            ExitGame.gameObject.SetActive(true);
        }
    }
}
