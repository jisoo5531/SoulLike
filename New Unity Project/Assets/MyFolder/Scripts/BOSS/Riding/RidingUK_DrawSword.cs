using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidingUK_DrawSword : MonoBehaviour
{
    private void Awake()
    {
        Invoke("DrawSword", 0.4f);
    }

    void DrawSword()
    {
        this.transform.GetChild(0).gameObject.SetActive(true);
    }
}
