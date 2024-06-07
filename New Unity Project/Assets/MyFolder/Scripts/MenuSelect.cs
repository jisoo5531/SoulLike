using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelect : MonoBehaviour
{
    ChangeScene sceneChange;
    FadeInOut fadeIO;

    Transform StartGame;
    Transform ExitGame;

    bool isStart;
    bool isExit;

    private void Awake()
    {
        sceneChange = FindObjectOfType<ChangeScene>();
        fadeIO = FindObjectOfType<FadeInOut>();

        StartGame = transform.GetChild(1).GetChild(0);
        ExitGame = transform.GetChild(2).GetChild(0);

        isStart = true;
        isExit = false;        
    }


    void Update()
    {
        InputKey();
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
    void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            isStart = !isStart;
            isExit = !isExit;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (isStart)
            {
                
                sceneChange.StartChangeScene();
                // 게임 스타트
            }
            else if (isExit)
            {

            }
        }
    }
}
