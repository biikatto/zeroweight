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

	GameObject[] activeBeams;
	public bool debug = true;

	float activeTime;

	void Start(){
		activeBeams = new GameObject[2];
	}

	void Fire(){
		if(cooldown == 0){
			activeBeams[0] = Instantiate(weaponBeam, transform.position, transform.rotation) as GameObject;
			activeBeams[1] = Instantiate(weaponBeam, transform.position, transform.rotation) as GameObject;
			activeBeams[0].transform.Translate(Vector3.left * mountWidth * 0.5f);
			activeBeams[0].transform.Translate(Vector3.forward * 50f);
			activeBeams[1].transform.Translate(Vector3.right * mountWidth * 0.5f);
			activeBeams[1].transform.Translate(Vector3.forward * 50f);

			activeBeams[0].transform.parent = transform;
			activeBeams[1].transform.parent = transform;
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
				for(int i=0;i<activeBeams.Length;i++){
					Destroy(activeBeams[i]);
				}
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
