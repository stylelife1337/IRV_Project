using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAction : Actions
{
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] bool isMusic;

    private AudioManager manager;

    // Use this for initialization
    void Start ()
    {
        manager = AudioManager.Instance;	
	}

    public override void Act()
    {
        if (!isMusic)
            manager.PlaySfx(audioClips[Random.Range(0, audioClips.Length)], transform);
        else
            manager.PlayMusic(audioClips[0]);
    }
}
