using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    #region Singleton
    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;            
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    public Sound[] effectSounds;

    public AudioSource[] audioSourceEffects;

    public string[] playSoundName;

    AudioSource[] enemySound;
    AudioSource[] playerSound;
    

    private void Start()
    {
        float currentSceneNumber = FindObjectOfType<ChangeScene>().currentSceneNumber;
        Debug.Log(currentSceneNumber);
        
        if (currentSceneNumber == 1)
        {
            enemySound = GameObject.Find("Enemy_Red").GetComponents<AudioSource>();            
        }
        else if (currentSceneNumber == 2)
        {
            enemySound = GameObject.Find("Realistic Undead Knight Grounded").GetComponents<AudioSource>();
        }
        else
        {
            return;
        }
        playerSound = GameObject.Find("Player").GetComponents<AudioSource>();

        audioSourceEffects = new AudioSource[enemySound.Length + playerSound.Length];
        int playerSoundIndex = 0;
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            if (i < enemySound.Length)
            {
                audioSourceEffects[i] = enemySound[i];
            }
            else
            {
                audioSourceEffects[i] = playerSound[playerSoundIndex];
                playerSoundIndex++;
            }
        }        
        
        playSoundName = new string[audioSourceEffects.Length];
    }

    public void PlaySoundEffect(string SE_Name)
    {
        for (int i = 0; i < effectSounds.Length; i++)
        {
            if (SE_Name == effectSounds[i].name)
            {
                for (int j = 0; j < audioSourceEffects.Length; j++)
                {
                    if (!audioSourceEffects[j].isPlaying)
                    {
                        audioSourceEffects[j].clip = effectSounds[i].clip;
                        audioSourceEffects[j].Play();
                        playSoundName[j] = effectSounds[i].name;
                        return;
                    }
                }                
            }
        }
    }
    public void StopSoundEffect(string SE_Name)
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            if (playSoundName[i] == SE_Name)
            {
                audioSourceEffects[i].Stop();
                break;
            }
        }
    }

}
