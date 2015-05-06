using UnityEngine;
using System.Collections;

public class KineticProjectile : MonoBehaviour{
	public float kineticDamage = 15f;
	public float homingForce = 1000f;
	public float homingRadius = 1000f;
	public float range = 100f;

	// Who fired the weapon?
	private GameObject origin;
	
	private new Rigidbody rigidbody;
	private new Collider collider;

	public bool returns;
	private bool returning;

	private AudioSource source;
	public AudioClip impactSound;

	void Start(){
    	source = gameObject.AddComponent<AudioSource>();
    	returns = false;
    	returning = false;
		rigidbody = gameObject.GetComponent<Rigidbody>();
		collider = gameObject.GetComponentInChildren<Collider>();
	}

	void FixedUpdate(){
		if(!returning || !returns){
			SeekShip(FindOpponent());
		}else{
			Debug.Log("pubup");
			SeekShip(origin);
		}
		if((transform.position - origin.transform.position).sqrMagnitude > range*range){
			if(returns){
				returning = true;
			}else{
				Destroy(gameObject);
			}
			rigidbody.velocity = rigidbody.velocity * -1f;
			collider.isTrigger = true;
		}
	}

	public GameObject Origin{
	    get{ return origin; }
	    set{ origin = value; }
	}

	public void OnTriggerEnter(Collider other){
		if(other.gameObject == origin){
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter(Collision collision){
		ExplosionEffect();

		ContactPoint contact = collision.contacts[0];
		if(contact.otherCollider.gameObject.tag == "Player" && contact.otherCollider.gameObject != origin){
		    print("Hit player");
		    // Apply damage
			contact.otherCollider.gameObject.GetComponent<PlayerDelegate>().AddDamage(kineticDamage);
			// Trigger impact fx
			contact.otherCollider.gameObject.GetComponent<PlayerDelegate>().Impact(1.0f);
		    // Activate hit message on opponent
			origin.GetComponent<PlayerDelegate>().HitMessage();
		}else if(contact.otherCollider.gameObject.tag == "Shield"){
		    print("Hit shield");
		    contact.otherCollider.gameObject.transform.parent.GetComponent<Shield>().Impact(kineticDamage);
		}
		source.PlayOneShot(impactSound);
		if(returns){
			returning = true;
		}
	}

	// Propel the projectile towards the desired ship
	void SeekShip(GameObject opponent){
		if(opponent != null){
			Vector3 distanceVector = (opponent.transform.position-transform.position);
			GetComponent<Rigidbody>().AddForce(
					distanceVector * 1f/distanceVector.magnitude * homingForce * GetComponent<Rigidbody>().mass);
		}
	}

    // Check if a GameObject is visible to this camera
	bool IsVisible(Camera cam, GameObject target){
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);
		return GeometryUtility.TestPlanesAABB(planes, target.GetComponent<Collider>().bounds);
	}

    // Return GameObject corresponding to opponent in view
	GameObject FindOpponent(){
		GameObject targetShip = null;
		foreach(GameObject ship in GameObject.FindGameObjectsWithTag("Player")){
			// Don't home in on the ship that fired this
			if(ship != origin){
				if(IsVisible(gameObject.GetComponent<Camera>(), ship)){
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

	private void Return(){
		
	}

    // Triggered when this projectile explodes
	void ExplosionEffect(){
		// Add explosion effect here
	}
}
