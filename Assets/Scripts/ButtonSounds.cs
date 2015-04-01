using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonSounds : MonoBehaviour,
							UnityEngine.EventSystems.ISelectHandler,
							UnityEngine.EventSystems.ISubmitHandler{
	public AudioClip selectionSound;
	public AudioClip activationSound;
	private AudioSource source;

	public void Start(){
		source = gameObject.GetComponent<AudioSource>();
	}

	public void OnSelect(UnityEngine.EventSystems.BaseEventData baseEvent){
		source.PlayOneShot(selectionSound);
		Debug.Log("boop");
	}

	public void OnSubmit(UnityEngine.EventSystems.BaseEventData baseEvent){
		source.PlayOneShot(activationSound);
		Debug.Log("beep");
	}
}
