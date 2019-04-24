using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance = null;
    public Sound[] sound;
    AudioSource theme;
    [System.Serializable]
    public struct Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0,1)]
        public float volume;
        [HideInInspector]
        public AudioSource source;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // Use this for initialization
    void Start () {
        theme.Play();
	}
	
	public void PlayAudio(string name)
    {
        int i = 0;
        while (i<sound.Length && sound[i].name != name)
        {
            i++;
        }
        try
        {
            sound[i].source = GetComponent<AudioSource>();
            sound[i].source.clip = sound[i].clip;
            sound[i].source.volume = sound[i].volume;
        }
    }
}
