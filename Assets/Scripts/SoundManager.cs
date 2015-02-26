using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour{
    public AudioClip[] sounds;
    private AudioSource source;

    void Start(){
        source = gameObject.GetComponent<AudioSource>();
    }

    public void PlaySound(int idx, float volumeScale){
        if(sounds.Length > 0){
            source.PlayOneShot(sounds[idx], volumeScale);
        }
    }

    public void PlaySound(int idx){
        PlaySound(idx, 1.0f);
    }

    public void PlaySound(float volumeScale){
        PlaySound(0, volumeScale);
    }

    public void PlaySound(){
        PlaySound(0, 1.0f);
    }
}
