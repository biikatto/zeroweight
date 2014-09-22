using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {
	Destruction destruction;
	public int maxHitPoints = 100;
	int hp;
	
	void Start(){
		destruction = GetComponent<Destruction>();
		hp = maxHitPoints;
	}
	void Update(){}

	void AddDamage(int amount){
		hp -= amount;
		if(hp <= 0){
			destruction.destroy();
		}
	}

	void Repair(int amount){
		hp += amount;
		if(hp > maxHitPoints){
			hp = maxHitPoints;
		}
	}
}
