using UnityEngine;
using System.Collections;

public class LaserCannon : MonoBehaviour {

	public GameObject laserBeam;
	public float cooldownTime = 2f;
	public float laserTime = 0.5f;
	float laserCountdown;
	float cooldown;

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
	}

	void ManageTimers(){
		if(activeTime > 0){
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
	}
	
	void Update(){
		if(Input.GetButton("Fire1") && (cooldown == 0)){
			Fire();
		}
		ManageTimers();
	}
}
