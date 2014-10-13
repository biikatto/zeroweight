using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public float maxHP = 100f;
	private float hp;

	void Start(){
		hp = maxHP;
	}

	void AddDamage(float damage){
		hp -= damage;
		if(hp <= 0f){
			hp = 0f;
			Destruct();
		}
		Debug.Log(hp + " HP left");
	}

	void Destruct(){
		Debug.Log("Player destroyed!");
	}
}
