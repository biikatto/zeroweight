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
		    // Apply damage
			contact.otherCollider.gameObject.GetComponent<PlayerDelegate>().AddDamage(kineticDamage);
		    // Activate hit message on opponent
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

    // Check if a GameObject is visible to this camera
	bool IsVisible(Camera cam, GameObject target){
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);
		return GeometryUtility.TestPlanesAABB(planes, target.collider.bounds);
	}

    // Return GameObject corresponding to opponent in view
	GameObject FindOpponent(){
		GameObject targetShip = null;
		foreach(GameObject ship in GameObject.FindGameObjectsWithTag("Player")){
			// Don't home in on the ship that fired this
			if(ship != origin){
				if(IsVisible(gameObject.camera, ship)){
					// Initialize targetShip
					if(targetShip == null){
						targetShip = ship;
					}else{
						// If ship is closer and visible, make it targetShip
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
		}
		return targetShip;
	}

	void ExplosionEffect(){
		// Add explosion effect here
	}
}
