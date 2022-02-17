using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] AudioMixerGroup sfxGroup, musicGroup;
    [SerializeField] int audioSourceInstances = 5;

    private Queue<AudioSource> sfxLib = new Queue<AudioSource>();
    private AudioSource musicPlayer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start ()
    {
        Init();	
	}

    void Init()
    {
        for (int i = 0; i < audioSourceInstances; i++)
        {
            sfxLib.Enqueue(AudioSourceInstantiate(sfxGroup, true, "AudioSource" + i.ToString("00")));
        }

        musicPlayer = AudioSourceInstantiate(musicGroup, false, "MusicSource");
    }

    AudioSource AudioSourceInstantiate(AudioMixerGroup group, bool sfx, string name = "AudioSource")
    {
        AudioSource audio = new GameObject(name).AddComponent<AudioSource>();
        audio.outputAudioMixerGroup = group;
        audio.spatialBlend = sfx ? 1f : 0f;

        audio.loop = !sfx;

        audio.transform.SetParent(transform);

        return audio;
    }
	
	public void PlaySfx(AudioClip clip, Transform source = null)
    {
        AudioSource audio;

        if (sfxLib.Count == 0)
        {
            audio = AudioSourceInstantiate(sfxGroup, true, "AudioSource" + audioSourceInstances++);
        }
        else
        {
            audio = sfxLib.Dequeue();
        }

        audio.transform.position = source != null ? source.position : Vector3.zero;

        audio.clip = clip;
        audio.Play();

        audio.transform.SetAsLastSibling(); //for illustrate the dequeue/enqueue process

        sfxLib.Enqueue(audio);
    }

    public void PlayMusic(AudioClip music)
    {
        if (musicPlayer.clip == music)
            return;

        musicPlayer.clip = music;
        musicPlayer.Play();
    }
}
