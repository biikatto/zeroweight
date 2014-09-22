using UnityEngine;
using System.Collections;

public class LaserCannon : MonoBehaviour {

	public GameObject laserBeam;
	public float cooldownTime = 2f;
	public float laserTime = 0.5f;
	float laserCountdown;
	float cooldown;
	bool checkForHit;

	GameObject[] activeBeams;
	float activeTime;

	void Start(){
		activeBeams = new GameObject[2];
	}

	void Fire(){
		activeBeams[0] = Instantiate(laserBeam, transform.position, transform.rotation) as GameObject;
		activeBeams[1] = Instantiate(laserBeam, transform.position, transform.rotation) as GameObject;
		activeBeams[0].transform.Translate(Vector3.left * 0.8f);
		activeBeams[0].transform.Translate(Vector3.forward * 50f);
		activeBeams[1].transform.Translate(Vector3.right * 0.8f);
		activeBeams[1].transform.Translate(Vector3.forward * 50f);

		activeBeams[0].transform.parent = transform;
		activeBeams[1].transform.parent = transform;
		activeTime += laserTime;
		cooldown += cooldownTime;

		checkForHit = true;
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
					Debug.Log(hit.transform.parent.name);
					hit.transform.parent.SendMessage("destroy", hit.point);
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
			Debug.Log("Miss");
			checkForHit = false;
		}
	}
	
	void Update(){
		if(Input.GetButton("Fire1") && (cooldown == 0)){
			Fire();
		}
		ManageTimers();
	}
}
