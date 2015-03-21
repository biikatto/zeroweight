using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour{

	public int maxHitPoints = 100;
	public float explosionForce = 300f;
	public bool debug = true;

	int hp;
	Vector3[] childPositions;
	Quaternion[] childRotations;

	bool destroyed = false;

	void Start(){
		hp = maxHitPoints;
		childPositions = new Vector3[transform.childCount];
		childRotations = new Quaternion[transform.childCount];
		for(int i=0;i<transform.childCount;i++){
			Transform child = transform.GetChild(i);
			//child.rigidbody.isKinematic = true;
			childPositions[i] = child.transform.position;
			childRotations[i] = child.transform.rotation;
		}
	}

	void Update(){
		if(debug){
			if(Input.GetButtonDown("Debug")){
				Reset();
			}
		}
	}

	public void Explode(Vector3 explosionPos){
		if(!destroyed){
			foreach(Transform child in transform){
				//child.rigidbody.isKinematic = false;
				child.GetComponent<Rigidbody>().AddExplosionForce(
						explosionForce,
						explosionPos,
						20f);
			}
		}
	}

	public void Explode(){
		Explode(transform.position);
		destroyed = true;
	}

	void Reset(){
		hp = maxHitPoints;
		for(int i=0;i<transform.childCount;i++){
			Transform child = transform.GetChild(i);
			//child.rigidbody.isKinematic = true;
			child.position = childPositions[i];
			child.rotation = childRotations[i];
		}
	}

	void AddDamage(int amount){
		hp -= amount;
		if(hp <= 0){
			Explode();
		}
	}

	void Repair(int amount){
		hp += amount;
		if(hp > maxHitPoints){
			hp = maxHitPoints;
		}
	}
}
