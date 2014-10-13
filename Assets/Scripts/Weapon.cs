using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public GameObject weaponBeam;
	public float cooldownTime = 2f;
	public float weaponTime = 0.5f;
	public float weaponDamage = 40f;
	public float kineticForce = 10f;
	public float mountWidth = 1f;
	public float range = Mathf.Infinity;
	
	private float weaponCountdown;
	private float cooldown;
	private bool checkForHit;
	private bool destroyed = false;

	public bool debug = true;

	private GameObject activeBeam;
	private float activeTime;
	private float beamLength;

	void Start(){}

	public void Fire(){
		if(!destroyed){
			if(cooldown == 0){
				activeBeam = Instantiate(weaponBeam, transform.position, transform.rotation) as GameObject;
				activeBeam.transform.localScale = new Vector3(1f, 1f, range);
				activeBeam.transform.Translate(Vector3.forward * 0.5f * range);
				activeBeam.transform.parent = transform;

				activeTime += weaponTime;
				cooldown += cooldownTime;

				checkForHit = true;
			}
		}
	}

	void ManageTimers(){
		// if weapon is active
		if(activeTime > 0){
			// Hit detection
			if(checkForHit){
				int layerMask = 1 << 8; // Select 8th layer (Targets)
				RaycastHit hit;
				if(Physics.Raycast(transform.position,
 					    	transform.TransformDirection(Vector3.forward),
							out hit,
							range,
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
					target.BroadcastMessage("AddDamage", weaponDamage);
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

	public void Destruct(){
		destroyed = true;
	}
}
