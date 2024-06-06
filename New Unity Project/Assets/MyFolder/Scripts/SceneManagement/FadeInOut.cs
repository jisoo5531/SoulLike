using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    ChangeScene scene;
    Image img;

    Transform playerHpBar;
    Transform enemyHpBar;

    public enum Type { FadeIn, FadeOut }
    public Type FadeIO;


    private void Awake()
    {
        scene = GetComponent<ChangeScene>();

        if (scene.currentSceneNumber != 1.5f)
        {
            playerHpBar = GameObject.Find("UI").transform.GetChild(1);
            enemyHpBar = GameObject.Find("UI").transform.GetChild(2);
        }        

        StartFadeIn();
    }

    public void StartFadeIn()
    {
        if (FadeIO == Type.FadeIn)
        {
            StartCoroutine(FadeIn());
        }
    }
    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {
        if (scene.currentSceneNumber != 1.5f)
        {
            playerHpBar.gameObject.SetActive(true);
            enemyHpBar.gameObject.SetActive(true);
        }
        

        img = GetComponentInChildren<Image>();
        Color alphaColor = img.color;

        while (true)
        {
            yield return null;
            alphaColor.a -= Time.deltaTime * 0.5f;
            img.color = alphaColor;
            if (alphaColor.a <= 0)
            {
                break;
            }
        }
    }

    IEnumerator FadeOut()
    {
        playerHpBar.gameObject.SetActive(false);
        enemyHpBar.gameObject.SetActive(false);

        img = GetComponentInChildren<Image>();
        Color alphaColor = img.color;


        while (true)
        {
            alphaColor.a += Time.deltaTime * 0.5f;
            img.color = alphaColor;
            if (alphaColor.a >= 255)
            {
                break;
            }
            yield return null;
        }
    }
}
