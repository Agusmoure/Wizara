﻿using System.Collections;
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
        // Las siguientes 3 variables las guardarán cada componente del array de structs.
        public string name;
        public AudioClip clip;
        [Range(0,1)]
        public float volume;
        [HideInInspector]
        // A las variables de tipo AudioSource se les puede asignar un clip y un volumen entre otros valores (tienen su propio .clip y .volume que en este caso estará vacío hasta más adelante).
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

        for(int i = 0; i < sound.Length; i++)
        {
            // Al iniciar se crean todos los nuevos componentes AudioSource según el número de componentes del array creado.
            sound[i].source = gameObject.AddComponent<AudioSource>();
            // A cada componente source de tipo AudioSource se le asigna el clip y el volumen guardado en el array.
            sound[i].source.clip = sound[i].clip;
            sound[i].source.volume = sound[i].volume;
        }
    }
    // Use this for initialization
    void Start () {
        // Comunica al GM de quien es el AudioManager.
        GameManager.instance.ThisAudioManager(this);
        // theme.Play();
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
            sound[i].source.Play();
        }
        catch
        {
            Debug.LogWarning("No existe el audio con nombre "+name+" que se intenta reproducir.");
        }
    }
}
