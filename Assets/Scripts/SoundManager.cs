using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    // Audio players components.
    public AudioSource[] effectsSource;
    public AudioSource musicSource;

    public AudioSource secondSource;

    public Slider musicSlider;
    public Slider sFXSlider;

    // Random pitch adjustment range.
    public float LowPitchRange = .95f;
    public float HighPitchRange = 1.05f;

    public static SoundManager instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        //DontDestroyOnLoad permite que al cambiar escena no se destruya el gameobject
        DontDestroyOnLoad(gameObject);
    }

    // Sonido SFX por el audiosource para efectos de sonido
    public void Play(AudioClip clip)
    {
        effectsSource[0].clip = clip;
        effectsSource[0].Play();
    }

    public void PlayOneshot(AudioClip clip)
    {
        effectsSource[0].clip = clip;
        effectsSource[0].PlayOneShot(clip);
    }

    // public void PlaySteps(AudioClip clip)
    // {
    //     effectsSource[1].clip = clip;
    //     effectsSource[1].Play();
    // }

    // Musica por el audiosource para musica
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
        
    }

    public void PlaySecondSource(AudioClip clip)
    {
        secondSource.clip = clip;
        secondSource.Play();
    }

    public void StopSecondSound()
    {
        secondSource.Stop();
    }

    
    


    // Sonido SFX por el audiosource para efectos de sonido + cambio random en el pitch para que parezcan sonidos diferentes
    // public void RandomSoundEffect(params AudioClip[] clips)
    // {
    //     int randomIndex = Random.Range(0, clips.Length);
    //     float randomPitch = Random.Range(LowPitchRange, HighPitchRange);

    //     effectsSource.pitch = randomPitch;
    //     effectsSource.clip = clips[randomIndex];
    //     effectsSource.Play();
    // }







    void Update()
    {
        try
        {
            if (musicSlider == null)
            {
                musicSlider = GameObject.FindGameObjectWithTag("musicSlider").GetComponent<Slider>();
            }

            if (sFXSlider == null)
            {
                sFXSlider = GameObject.FindGameObjectWithTag("sfxSlider").GetComponent<Slider>();
            }
            musicSource.volume = musicSlider.value;
            effectsSource[0].volume = sFXSlider.value;
            effectsSource[1].volume = sFXSlider.value;
        }
        catch
        {

            //no hay sliders en esta escena
        }


    }


}