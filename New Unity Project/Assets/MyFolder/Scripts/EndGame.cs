using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public GameObject victory;
    public GameObject defeat;
    private void Awake()
    {        
        victory.SetActive(false);
        defeat.SetActive(false);
    }

    public void Victory()
    {
        victory.SetActive(true);
    }
    public void Defeat()
    {
        if (defeat != null)
        {
            defeat.SetActive(true);
        }
     
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
