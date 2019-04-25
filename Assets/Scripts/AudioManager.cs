using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance = null;
    public string mainSceneName;
    public Sound[] sound;
    AudioSource mainTheme;
    [System.Serializable]
    public struct Sound
    {
        // Las siguientes 4 variables las guardarán cada componente del array de structs.
        public string name;
        public AudioClip clip;
        public bool looping;
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

        mainTheme = GetComponent<AudioSource>();

        for(int i = 0; i < sound.Length; i++)
        {
            // Al iniciar se crean todos los nuevos componentes AudioSource según el número de componentes del array creado.
            sound[i].source = gameObject.AddComponent<AudioSource>();
            // A cada componente source de tipo AudioSource se le asigna el clip y el volumen guardado en el array, además de asignar si se ha seleccionado el loop del sonido a true.
            sound[i].source.clip = sound[i].clip;
            sound[i].source.volume = sound[i].volume;
            sound[i].source.loop = sound[i].looping;
        }
    }
    // Use this for initialization
    void Start () {
        // Comunica al GM de quien es el AudioManager.
        GameManager.instance.ThisAudioManager(this);
    }
	
	public void PlayAudio(string name)
    {
        int i = 0;
        // Busca el componente del array con nombre name.
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
            //Si el índice se sale del array y no se ha podido reproducir el audio, se comunica.
            Debug.LogWarning("No existe el componente con nombre "+name+" cuyo audio se intenta reproducir.");
        }
    }
    public void PlayMainAudio(string name)
    {
        int i = 0;
        // Busca el componente del array con nombre name.
        while (i<sound.Length && sound[i].name != name)
        {
            i++;
        }
        try
        {
            // Si el tema ya esta sonando, no lo vuelve a reproducir.
            if (mainTheme.clip != sound[i].source.clip)
            {
                mainTheme.clip = sound[i].source.clip;
                mainTheme.Play();
            }   
        }
        catch
        {
            //Si el índice se sale del array y no se ha podido reproducir el audio, se comunica.
            Debug.LogWarning("No existe el componente con nombre "+name+" cuyo audio se intenta reproducir.");
        }
    }
}
