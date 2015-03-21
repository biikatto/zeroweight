using UnityEngine;
using System;
using System.Collections;

public class Thrust : MonoBehaviour {

	public float maxThrust = 200f;
	public float throttleSensitivity = 100f;

	// Boost
	public AnimationCurve boostCurve;

	// Public maxima
	public float boostThrust = 2.5f;
	public float boostEnergyCost = 10f;

	// Thrust power
	private float thrustPower = 1f;
	private float xThrustPower = 100f;
	private float yThrustPower = 100f;
	private float zThrustPower = 100f;

	// Thrust input
	private float xThrust = 0f;
	private float yThrust = 0f;
	private float zThrust = 0f;

	private float disruptTime = 0f;

	private Vector3 velocity;

	private PlayerDelegate pDelegate;

	void Start(){
		thrustPower = maxThrust;
		boostCurve = new AnimationCurve(new Keyframe(0f, 0f),
										new Keyframe(0.1f, 1f), 
										new Keyframe(0.8f, 1f), 
										new Keyframe(1.0f, 0f));
		pDelegate = gameObject.GetComponentInChildren<PlayerDelegate>();	
	}

	public void Destruct(){
	}

	void adjustThrust(float adjustment){
		thrustPower += adjustment * throttleSensitivity;
		if(thrustPower > maxThrust){
			thrustPower = maxThrust;
		}
		if(thrustPower < 0){
			thrustPower = 0;
		}
	}

	public void XThrust(float thrust){
		xThrust = thrust;
	}

	public void YThrust(float thrust){
		yThrust = thrust;
	}

	public void ZThrust(float thrust){
		zThrust = thrust;
	}

	private void Boost(Vector3 thrust){
		if(pDelegate.UseEnergy(boostEnergyCost)){
			GetComponent<Rigidbody>().AddRelativeForce(thrust);
		}
	}

	public void BoostLeft(){
		Boost(Vector3.left * boostThrust);
	}
	
	public void BoostRight(){
		Boost(Vector3.right * boostThrust);
	}

	public void BoostUp(){
		Boost(Vector3.up * boostThrust);
	}

	public void BoostDown(){
		Boost(Vector3.down * boostThrust);
	}

	public void OldBoost(){
		//if(!boosting){
		//	if(boostCooldown <= 0){
		//		//Debug.Log("Boost begin");
		//		boosting = true;
		//		boostTime = 0f;
		//	}else{
		//		//Debug.Log("Boost cooldown not finished.");
		//	}
		//}else{
		//	//Debug.Log("Still boosting.");
		//}
	}

	private void manageBoost(){
		//if(boosting){
		//	boostPower = boostCurve.Evaluate(boostTime/boostLength);
		//	//Debug.Log(1f-(boostTime/boostLength));
		//	transform.GetComponent<PlayerDelegate>().BoostMeter(1f-(boostTime/boostLength));
		//	boostTime += Time.deltaTime;
		//	if(boostTime >= boostLength){
		//		boosting = false;
		//		boostTime = 0f;
		//		boostCooldown = boostCooldownLength;
		//		//Debug.Log("Boost end");
		//	}
		//}else if(boostCooldown > 0f){
		//	boostCooldown -= Time.deltaTime;
		//	//Debug.Log(1f-(float)boostCooldown/boostCooldownLength);
		//	transform.GetComponent<PlayerDelegate>().BoostMeter(Math.Min(1f,1f-(float)boostCooldown/boostCooldownLength));
		//}else{
		//	transform.GetComponent<PlayerDelegate>().BoostMeter(1f);
		//}
	}

	void FixedUpdate(){
		adjustThrust(Input.GetAxis("Mouse ScrollWheel"));
		//manageBoost();
		if(disruptTime <= 0f){
			GetComponent<Rigidbody>().AddRelativeForce(Vector3.right * (xThrust * xThrustPower * thrustPower /* * (1f + boostPower * boostThrust)*/) * Time.deltaTime);
			GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * (yThrust * yThrustPower * thrustPower /* * (1f + boostPower * boostThrust)*/) * Time.deltaTime);
			GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * (zThrust * zThrustPower * thrustPower /* * (1f + boostPower * boostThrust)*/) * Time.deltaTime);
		}else{
			disruptTime -= Time.deltaTime;
			if(disruptTime < 0f){
				disruptTime = 0f;
			}
		}
		if(Time.frameCount%3 == 0){
			//Debug.Log(rigidbody.velocity.magnitude/110f);
			transform.GetComponent<PlayerDelegate>().VelocityMeter(GetComponent<Rigidbody>().velocity.magnitude/110f);
		}
	}


	public void disrupt(float time){
		disruptTime += time;
	}
}
