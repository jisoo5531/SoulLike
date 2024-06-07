using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    Player player;
    public float currentSceneNumber;

    [Tooltip("FadeOut 시 넘어갈 씬 넘버를 입력 / 없으면 0")]
    public float fadeOutSceneNum;        

    /// <summary>
    /// Fade In / Out
    /// </summary>
    /// <param name="num"></param>
    public void StartChangeScene()
    {
        if (currentSceneNumber == 0.5f)
        {
            SceneManager.LoadScene("Phase1");
        }
        StartCoroutine(PhaseSceneChange());
    }

    IEnumerator PhaseSceneChange()
    {
        if (currentSceneNumber != 0.5f && currentSceneNumber != 1.5f)
        {
            player = FindObjectOfType<Player>();

            player.StopAllCoroutines();
        }
        

        yield return new WaitForSeconds(4f);

       
        if (fadeOutSceneNum == 1.5f)
        {
            SceneManager.LoadScene("Phase1_End");
        }
        else if (fadeOutSceneNum == 2)
        {
            SceneManager.LoadScene("Phase2");
        }
    }

}
