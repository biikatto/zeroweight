// Disrupt engines and add extra reactionary force upon ship collisions

using UnityEngine;
using System.Collections;

public class Bump : MonoBehaviour {
	Thrust thrust;
	public float disruptTime = 0.15f;
	public float bumpForce = 1f;

	void Start(){
		// Find our Thrust component
		thrust = GetComponent<Thrust>();
	}

	void Update(){
		// Debug
		if(Input.GetButtonDown("Debug")){
			Debug.Log(GetComponent<Rigidbody>().GetPointVelocity(transform.position));
		}
	}

	void OnCollisionEnter(Collision collision){
		foreach(ContactPoint contact in collision.contacts){
			Debug.DrawRay(contact.point, contact.normal, Color.white);
			GetComponent<Rigidbody>().AddForce(contact.normal * bumpForce * -1f);
		}
		thrust.disrupt(disruptTime);
	}
}
