using UnityEngine;
using System.Collections;

public class Destruction : MonoBehaviour{
	public float explosionForce = 300f;

	void Start(){
		foreach(Transform child in transform){
			child.parent = transform;
		}
	}

	void Update(){
	}

	public void destroy(Vector3 explosionPos){
		Debug.Log(explosionPos);
		foreach(Transform child in transform){
			child.rigidbody.AddExplosionForce(
					explosionForce,
					explosionPos,
					20f);
		}
	}

	public void destroy(){
		Debug.Log("default destroy");
		destroy(transform.position);
	}
}
