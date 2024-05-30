using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour 
{

    Vector3 Range;
    Vector3 startPos;

    Player player;

    // Use this for initialization
    void Awake () 
    {
        startPos = transform.position;
        player = FindObjectOfType<Player>();
    }
	
	// Update is called once per frame
	void OnEnable() 
    {
        transform.position = startPos;
	}


    public void CustomTeleport()
    {
        var renderers = GetComponentsInChildren<Renderer>();
        foreach (var rend in renderers) {
            rend.enabled = false;
        }

        this.transform.position = player.transform.position + player.transform.forward * 3.0f;

        foreach (var rend in renderers)
        {
            rend.enabled = true;
        }
    }
}
