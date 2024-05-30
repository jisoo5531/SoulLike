using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EffectSlots
{
    public Effect_Info[] Effects;
}
[System.Serializable]
public class Effect_Info
{
    public GameObject Effect;
    public Transform StartPositionRotation;
    public float DestroyAfter = 10;
    public bool UseLocalPosition = true;
}

public class E_G_UK_AnimationEventEffect : MonoBehaviour
{
    public EffectSlots[] effect_slots;

    public int skillNum;

    void InstantiateEffect(int EffectNumber)
    {
        if (skillNum >= 0)
        {
            
            if (effect_slots[skillNum].Effects == null || effect_slots[skillNum].Effects.Length <= EffectNumber)
            {
                Debug.LogError("Incorrect effect number or effect is null");
            }

            var instance = Instantiate(effect_slots[skillNum].Effects[EffectNumber].Effect, effect_slots[skillNum].Effects[EffectNumber].StartPositionRotation.position, effect_slots[skillNum].Effects[EffectNumber].StartPositionRotation.rotation);

            if (effect_slots[skillNum].Effects[EffectNumber].UseLocalPosition)
            {
                instance.transform.parent = effect_slots[skillNum].Effects[EffectNumber].StartPositionRotation.transform;
                instance.transform.localPosition = Vector3.zero;
                instance.transform.localRotation = new Quaternion();
            }
            Destroy(instance, effect_slots[skillNum].Effects[EffectNumber].DestroyAfter);
        }        
    }
}
