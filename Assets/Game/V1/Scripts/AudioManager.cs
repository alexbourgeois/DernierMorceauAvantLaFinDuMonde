using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public List<AudioSource> zombieDeath = new List<AudioSource>();
    public List<AudioSource> zombieAlive = new List<AudioSource>();

    private void Awake()
    {
        instance = this;
    }

    public void PlayZombieLifeSound()
    {
        var rnd = Random.Range(0, zombieAlive.Count);
        if(!zombieAlive[rnd].isPlaying)
            zombieAlive[rnd].Play();
    }
    public void PlayZombieDeathSound()
    {
        var rnd = Random.Range(0, zombieDeath.Count);
        if (!zombieDeath[rnd].isPlaying)
            zombieDeath[rnd].Play();
    }
}
