using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioClip Normal;
    public AudioClip Battle;

    public AudioSource source;
    public AudioSource fightSource;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    public void SetFight(bool value)
    {
      /*  AudioClip clip = value ? Battle : Normal;
        source.clip = clip;
        source.Play();*/

      if (value)
      {
          fightSource.Play();
          source.Pause();
      }
      else
      {
          fightSource.Stop();
          source.Play();
      }
    }
}
