using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public GameObject weaponBeam;
	public float cooldownTime = 2f;
	public float weaponTime = 0.5f;
	public float weaponDamage = 40f;
	public float kineticForce = 10f;
	public float mountWidth = 1f;
	float weaponCountdown;
	float cooldown;
	bool checkForHit;

	public bool debug = true;

	GameObject activeBeam;
	float activeTime;

	void Start(){
	}

	void Fire(){
		if(cooldown == 0){
			activeBeam = Instantiate(weaponBeam, transform.position, transform.rotation) as GameObject;
			activeBeam.transform.Translate(
					Vector3.forward * 0.5f * beamLength);
			activeBeam.transform.parent = transform;

			activeTime += weaponTime;
			cooldown += cooldownTime;

			checkForHit = true;
		}
	}

	void ManageTimers(){
		if(activeTime > 0){
			// Hit detection
			if(checkForHit){
				int layerMask = 1 << 8; // Select 8th layer (Targets)
				RaycastHit hit;
				if(Physics.Raycast(transform.position,
 					    	transform.TransformDirection(Vector3.forward),
							out hit,
							Mathf.Infinity,
							layerMask)){
					Transform target;
					if(hit.transform.parent != null){
						target = hit.transform.parent;
					}else{
						target = hit.transform;
					}
					if(debug){
						Debug.Log("Hit");
					}
					target.SendMessage("AddDamage", weaponDamage);
					checkForHit = false;
				}
			}
			if(activeTime - Time.deltaTime <= 0){
				Destroy(activeBeam);
			}
			activeTime -= Time.deltaTime;
		}
		if(activeTime < 0){
			activeTime = 0;
		}
		if(cooldown > 0){
			cooldown -= Time.deltaTime;
		}
		if(cooldown < 0){
			cooldown = 0;
		}
		if(checkForHit & (activeTime == 0)){
			if(debug){
				Debug.Log("Miss");
			}
			checkForHit = false;
		}
	}
	
	void Update(){
		ManageTimers();
	}
}
