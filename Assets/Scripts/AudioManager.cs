using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioManager : MonoBehaviour {
    //Drag a reference to the audio source which will play the sound effects.
    public AudioSource[] efxSources;
    //Drag a reference to the audio source which will play the music.
    public AudioSource musicSource;
    //Allows other scripts to call functions from the SoundManager
    public static AudioManager instance = null;
    //The lowest a sound effect will be randomly pitched.
    public float lowPitchRange = .95f;
    //The highest a sound effect will be randomly pitched.
    public float highPitchRange = 1.05f;


    void Awake() {
        //Check if there is already an instance of SoundManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }


    //Used to play single sound clips.
    public void PlaySingle(int index, AudioClip clip) {
        if (index > efxSources.Length) return;
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        efxSources[index].clip = clip;

        //Play the clip.
        efxSources[index].Play();
    }

    public void PlaySingle(AudioClip clip) {
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        efxSources[0].clip = clip;

        //Play the clip.
        efxSources[0].Play();
    }

    //RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
    public void RandomizeSfx(params AudioClip[] clips) {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        efxSources[0].pitch = randomPitch;
        efxSources[0].clip = clips[randomIndex];
        efxSources[0].Play();
    }

    public void RandomizeSfx(int index, params AudioClip[] clips) {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        efxSources[index].pitch = randomPitch;
        efxSources[index].clip = clips[randomIndex];
        efxSources[index].Play();
    }
}