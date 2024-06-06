using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPowerUpScene : MonoBehaviour
{
    public GameObject Effect;
    public GameObject BodyObject;
    public GameObject SwordObject;

    Animator anim;

    private void Awake()
    {
        StartCoroutine(PowerUp());

        anim = GetComponent<Animator>();
    }

    IEnumerator PowerUp()
    {
        yield return new WaitForSeconds(2.5f);

        var currentInstance = Instantiate(Effect) as GameObject;
        var psUpdater = currentInstance.GetComponent<PSMeshRendererUpdater>();
        psUpdater.UpdateMeshEffect(BodyObject);
        psUpdater.UpdateMeshEffect(SwordObject);

        yield return new WaitForSeconds(2f);
        
        anim.SetTrigger("DoAction");
    }
}
