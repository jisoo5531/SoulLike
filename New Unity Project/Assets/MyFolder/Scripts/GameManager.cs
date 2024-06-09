using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    FadeInOut fadeIO;

    void Start()
    {
        fadeIO = FindObjectOfType<FadeInOut>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    public void ClickStartButton()
    {
        fadeIO.StartFadeIn();
    }
}
