using UnityEngine;
using System;
using System.Collections;

public class PlayerGUI : MonoBehaviour{
	private SegmentedBar hpBar;
	

	void Start(){
		SegmentedBar[] bars = gameObject.GetComponentsInChildren<SegmentedBar>();
		hpBar = bars[0]; // change this when more bars are implemented
	}

	public void VelocityMeter(float amount){
	}

	public void BoostMeter(float energy){
	}

	public void HPMeter(float amount){
		hpBar.Phase(amount);
	}

	public void HitMessage(){
	}
}
