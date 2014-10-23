using UnityEngine;
using System.Collections;

public class KineticProjectile : MonoBehaviour{
	public float kineticDamage = 15f;
	public float homingForce = 1000f;
	public float homingRadius = 1000f;

	// Who fired the weapon?
	private GameObject origin;

	public void SetOrigin(GameObject newOrigin){
		origin = newOrigin;
	}

	void OnCollisionEnter(Collision collision){
		ExplosionEffect();

		ContactPoint contact = collision.contacts[0];
		if(contact.otherCollider.gameObject.tag == "Player"){
			contact.otherCollider.gameObject.GetComponent<PlayerDelegate>().AddDamage(kineticDamage);
			origin.GetComponent<PlayerDelegate>().HitMessage();
		}
		Destroy(gameObject);
	}

	void FixedUpdate(){
		Home();
	}

	void Home(){
		GameObject opponent = FindOpponent();
		if(opponent != null){
			Vector3 distanceVector = (opponent.transform.position-transform.position);
			rigidbody.AddForce(
				distanceVector * 1f/distanceVector.magnitude * homingForce * rigidbody.mass);
		}
	}

	GameObject FindOpponent(){
		GameObject targetShip = null;
		foreach(GameObject ship in GameObject.FindGameObjectsWithTag("Player")){
			// Don't home in on the ship that fired this
			if(ship != origin){
				// Initialize targetShip
				if(targetShip == null){
					targetShip = ship;
				}else{
					// If ship is closer, make it targetShip
					float shipDistance = Vector3.Distance(
							transform.position,
							ship.transform.position);
					float targetDistance = Vector3.Distance(
							transform.position,
							targetShip.transform.position);
					if(shipDistance < targetDistance){
						targetShip = ship;
					}
				}
			}
		}
		return targetShip;
	}

	void ExplosionEffect(){
		// Add explosion effect here
	}
}
