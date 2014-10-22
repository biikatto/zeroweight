// Disrupt engines and add extra reactionary force upon ship collisions

using UnityEngine;
using System.Collections;

public class Bump : MonoBehaviour {
	Thrust thrust;
	public float disruptTime = 0.15f;

	void Start(){
		// Find our Thrust component
		thrust = GetComponent<Thrust>();
	}

	void Update(){
		if(Input.GetButtonDown("Debug")){
			Debug.Log(rigidbody.GetPointVelocity(transform.position));
		}
	}

	void OnCollisionEnter(Collision collision){
		float bumpForce = 1f;
		foreach(ContactPoint contact in collision.contacts){
			Debug.DrawRay(contact.point, contact.normal, Color.white);
			rigidbody.AddForce(contact.normal * bumpForce * -1f);
		}
		thrust.disrupt(disruptTime);
	}
}
