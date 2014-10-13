using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public float maxHP = 100f;
	private float hp;

	void Start(){
		hp = maxHP;
	}

	public void AddDamage(float damage){
		hp -= damage;
		if(hp <= 0f){
			hp = 0f;
			Destruct();
		}
		Debug.Log(hp + " HP left");
	}

	public void Destruct(){
		Debug.Log("Player destroyed!");
		gameObject.GetComponent<PlayerDelegate>().Destruct();
	}
}
