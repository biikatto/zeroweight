using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public float maxHP = 100f;
	private float hp;
	private bool destroyed = false;

	void Start(){
		hp = maxHP;
	}

	public void AddDamage(float damage){
		hp -= damage;
		if(hp <= 0f){
			hp = 0f;
			if(!destroyed){
				Destruct();
			}
			destroyed = true;
		}
		//Debug.Log(hp + " HP left");
		gameObject.GetComponent<PlayerDelegate>().HPMeter(hp/maxHP);
	}

	public void Destruct(){
		Debug.Log("Player destroyed!");
		gameObject.GetComponent<PlayerDelegate>().Destruct();
	}
}
