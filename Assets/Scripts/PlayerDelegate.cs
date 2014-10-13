using UnityEngine;
using System.Collections;

public class PlayerDelegate : MonoBehaviour{
	PlayerControl playerControl;
	PlayerGUI playerGUI;
	PlayerHealth playerHealth;
	Thrust thrust;

	Weapon leftWeapon;
	Weapon rightWeapon;

	private bool destroyed = false;

	void Start(){
		playerControl = gameObject.GetComponentInChildren<PlayerControl>();	
		playerGUI = gameObject.GetComponentInChildren<PlayerGUI>();	
		playerHealth = gameObject.GetComponentInChildren<PlayerHealth>();	
		thrust = gameObject.GetComponentInChildren<Thrust>();
		cameraMove = gameObject.GetComponentInChildren<CameraMove>();
		steer = gameObject.GetComponentInChildren<Steer>();

    	foreach(Weapon weapon in gameObject.GetComponentsInChildren<Weapon>()){
    		if(weapon.gameObject.name == "Left laser"){
    			leftWeapon = weapon;
    		}else if(weapon.gameObject.name == "Right laser"){
    			rightWeapon = weapon;
    		}
    	}
	}

	public void XThrust(float amount){
		thrust.XThrust(amount);
	}

	public void YThrust(float amount){
		thrust.YThrust(amount);
	}

	public void ZThrust(float amount){
		thrust.ZThrust(amount);
	}

	public void Boost(){
		thrust.Boost();
	}

	public void FireLeftWeapon(){
		leftWeapon.Fire();
	}

	public void FireRightWeapon(){
		rightWeapon.Fire();
	}

	public void Destruct(){
		steer.Destruct();
		playerControl.Destruct();
		thrust.Destruct();
		leftWeapon.Destruct();
		rightWeapon.Destruct();
	}
}
