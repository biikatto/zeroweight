using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour{
    public AudioClip[] impact;
    public AudioClip[] laser;
    public AudioClip[] lowHealth;
    public AudioClip[] charge;
    public AudioClip[] rocket;

    private AudioSource impactSource;
    private AudioSource laserSource;
    private AudioSource lowHealthSource;
    private AudioSource chargeSource;
    private AudioSource rocketSource;

    private AudioSource defaultSource;

    void Start(){
    	defaultSource = gameObject.AddComponent<AudioSource>();
		impactSource = gameObject.AddComponent<AudioSource>();
        laserSource = gameObject.AddComponent<AudioSource>();
        lowHealthSource = gameObject.AddComponent<AudioSource>();
       	chargeSource = gameObject.AddComponent<AudioSource>();
        rocketSource = gameObject.AddComponent<AudioSource>();
    }
    // Impact
    public void PlayImpactSound(int idx, float volumeScale){
        if(impact.Length > idx){
    		PlaySound(impact[idx]);
    	}
    }

    public void PlayImpactSound(float volumeScale){
    	PlayRandomSound(impact, volumeScale);
    }

    public void PlayImpactSound(){
    	PlayRandomSound(impact);
    }

    // Laser
    public void PlayLaserSound(int idx, float volumeScale){
        if(laser.Length > idx){
    		PlaySound(laser[idx]);
    	}
    }

    public void PlayLaserSound(float volumeScale){
    	PlayRandomSound(laser, volumeScale);
    }

    public void PlayLaserSound(){
    	PlayRandomSound(laser);
    }

    // Low health
    public void PlayLowHealthSound(int idx, float volumeScale){
        if(lowHealth.Length > idx){
    		PlaySound(lowHealth[idx]);
    	}
    }

    public void PlayLowHealthSound(float volumeScale){
    	PlayRandomSound(lowHealth, volumeScale);
    }

    public void PlayLowHealthSound(){
    	PlayRandomSound(lowHealth);
    }

    // Charge
    public void PlayChargeSound(int idx, float volumeScale){
        if(charge.Length > idx){
    		PlaySound(charge[idx], chargeSource);
    	}
    }

    public void PlayChargeSound(float volumeScale){
    	PlayRandomSound(charge, volumeScale, chargeSource);
    }

    public void PlayChargeSound(){
    	PlayRandomSound(charge, chargeSource);
    }

    public void StopChargeSound(){
    	chargeSource.Stop();
    }

    // Rockets
    public void PlayRocketSound(int idx, float volumeScale){
        if(rocket.Length > idx){
    		PlaySound(rocket[idx], rocketSource);
    	}
    }

    public void PlayRocketSound(float volumeScale){
    	PlayRandomSound(rocket, volumeScale, rocketSource);
    }

    public void PlayRocketSound(){
    	PlayRandomSound(rocket, rocketSource);
    }

    // Utility functions
    public void PlayRandomSound(AudioClip[] clips, float volumeScale, AudioSource source){
        if(clips.Length > 0){
        	int idx = Random.Range(0, (clips.Length-1));
            source.PlayOneShot(clips[idx], volumeScale);
        }
    }

    public void PlayRandomSound(AudioClip[] clips, float volumeScale){
    	PlayRandomSound(clips, volumeScale, defaultSource);
    }

    public void PlayRandomSound(AudioClip[] clips){
    	PlayRandomSound(clips, 1.0f);
    }
    
    public void PlayRandomSound(AudioClip[] clips, AudioSource source){
    	PlayRandomSound(clips, 1.0f, source);
    }

    public void PlaySound(AudioClip clip, float volumeScale){
        defaultSource.PlayOneShot(clip, volumeScale);
    }

    public void PlaySound(AudioClip clip){
        defaultSource.PlayOneShot(clip, 1.0f);
    }

    public void PlaySound(AudioClip clip, float volumeScale, AudioSource source){
        source.PlayOneShot(clip, volumeScale);
    }

    public void PlaySound(AudioClip clip, AudioSource source){
        source.PlayOneShot(clip, 1.0f);
    }
}
