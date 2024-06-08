using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSound : MonoBehaviour
{
    void StartSound(string _name)
    {
        SoundManager.instance.PlaySoundEffect(_name);
    }
    void StopSound(string _name)
    {
        SoundManager.instance.StopSoundEffect(_name);
    }
}
