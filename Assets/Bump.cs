using UnityEngine;
using System.Collections;

public class Bump : MonoBehaviour {
	void Start(){
	}

	void Update(){
	}

	void OnCollisionEnter(Collision collision){
		foreach(ContactPoint contact in collision.contacts){
			Debug.DrawRay(contact.point, contact.normal, Color.white);
		}
	}
}
