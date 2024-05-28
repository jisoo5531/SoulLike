using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    ChangeScene scene;
    Image img;

    public enum Type { FadeIn, FadeOut }
    public Type FadeIO;


    private void Awake()
    {
        scene = GetComponent<ChangeScene>();

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
        img = GetComponentInChildren<Image>();
        Color alphaColor = img.color;


        while (true)
        {
            alphaColor.a -= Time.deltaTime * 0.5f;
            img.color = alphaColor;
            if (alphaColor.a <= 0)
            {
                break;
            }
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
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
