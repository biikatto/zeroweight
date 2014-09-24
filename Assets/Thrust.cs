using UnityEngine;
using System.Collections;

public class Thrust : MonoBehaviour {

	public float maxThrust = 200f;

	public float throttleSensitivity = 100f;

	public AnimationCurve boostCurve;
	private Keyframe[] ks;

	float thrustPower = 1f;
	float xThrustPower = 100f;
	float yThrustPower = 100f;
	float zThrustPower = 100f;

	float xThrust = 0f;
	float yThrust = 0f;
	float zThrust = 0f;

	float disruptTime = 0f;

	private Vector3 velocity;

	void Start(){
		thrustPower = maxThrust;
		boostCurve = new AnimationCurve(new Keyframe(0f, 0f),
										new Keyframe(0.1f, 1f), 
										new Keyframe(0.8f, 1f), 
										new Keyframe(1.0f, 0f));
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

	void XThrust(float thrust){
		xThrust = thrust;
	}

	void YThrust(float thrust){
		yThrust = thrust;
	}

	void ZThrust(float thrust){
		zThrust = thrust;
	}

	void FixedUpdate(){
		adjustThrust(Input.GetAxis("Mouse ScrollWheel"));
		if(disruptTime == 0f){
			rigidbody.AddRelativeForce(Vector3.right * xThrust * xThrustPower * thrustPower * Time.deltaTime);
			rigidbody.AddRelativeForce(Vector3.up * yThrust * yThrustPower * thrustPower * Time.deltaTime);
			rigidbody.AddRelativeForce(Vector3.forward * zThrust * zThrustPower * thrustPower * Time.deltaTime);
		}else{
			disruptTime -= Time.deltaTime;
			if(disruptTime < 0){
				disruptTime = 0;
			}
		}
	}

	public void disrupt(float time){
		disruptTime += time;
	}
}
