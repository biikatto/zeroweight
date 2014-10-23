using UnityEngine;
using System.Collections;

public class PlayerDelegate : MonoBehaviour{

	public bool player2;
	private PlayerControl playerControl;
	private PlayerGUI playerGUI;
	private PlayerHealth playerHealth;
	private Thrust thrust;

	private Weapon leftWeapon;
	private Weapon rightWeapon;

	private ScoreKeeper score;

	private bool destroyed = false;

	void Start(){
		score = FindObjectOfType(typeof(ScoreKeeper)) as ScoreKeeper;
		playerControl = gameObject.GetComponentInChildren<PlayerControl>();	
		playerGUI = gameObject.GetComponentInChildren<PlayerGUI>();	
		playerHealth = gameObject.GetComponentInChildren<PlayerHealth>();	
		thrust = gameObject.GetComponentInChildren<Thrust>();

    	foreach(Weapon weapon in gameObject.GetComponentsInChildren<Weapon>()){
    		if(weapon.gameObject.name == "Left laser"){
    			leftWeapon = weapon;
    		}else if(weapon.gameObject.name == "Right laser"){
    			rightWeapon = weapon;
    		}
    	}
	}

	public void VelocityMeter(float amount){
		playerGUI.VelocityMeter(amount);
	}

	public void BoostMeter(float amount){
		playerGUI.BoostMeter(amount);
	}

	public void HPMeter(float amount){
		playerGUI.HPMeter(amount);
	}

	public void AddDamage(float amount){
		playerHealth.AddDamage(amount);
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
		playerControl.Destruct();
		thrust.Destruct();
		leftWeapon.Destruct();
		rightWeapon.Destruct();
		
		score.AddPoint(player2);
	}

	public void Pause(bool player1){
		score.Pause(player1);
	}

	public void HitMessage(){
		playerGUI.HitMessage();
	}
}
