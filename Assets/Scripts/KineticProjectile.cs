using UnityEngine;
using System.Collections;

public class KineticProjectile : MonoBehaviour{
	public float kineticDamage = 15f;

	void OnCollisionEnter(Collision collision){
		ExplosionEffect();

		ContactPoint contact = collision.contacts[0];
		if(contact.otherCollider.gameObject.tag == "Player"){
			contact.otherCollider.gameObject.GetComponent<PlayerDelegate>().AddDamage(kineticDamage);
		}
		Destroy(gameObject);
	}

	void ExplosionEffect(){
		// Add real explosion effect here
		Debug.Log("Boom!");
	}
}
