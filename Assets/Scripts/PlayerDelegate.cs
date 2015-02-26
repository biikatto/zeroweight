using UnityEngine;
using System.Collections;

public class PlayerDelegate : MonoBehaviour{

    public bool player2;

    public float maxEnergy = 100f;
    private float energy;
    public float energyRegenRate = 10f;

    private PlayerControl playerControl;
    private PlayerGUI playerGUI;
    private PlayerHealth playerHealth;
    private Thrust thrust;

    private Weapon leftWeapon;
    private Weapon rightWeapon;

    private Shield shield;

    private SoundManager soundManager;

    private ScoreKeeper score;

    private bool destroyed = false;

    void Start(){
        energy = maxEnergy;
        score = FindObjectOfType(typeof(ScoreKeeper)) as ScoreKeeper;
        playerControl = gameObject.GetComponentInChildren<PlayerControl>();
        playerGUI = gameObject.GetComponentInChildren<PlayerGUI>();
        playerHealth = gameObject.GetComponentInChildren<PlayerHealth>();
        thrust = gameObject.GetComponentInChildren<Thrust>();
        
        shield = gameObject.AddComponent("Shield") as Shield;
        shield.PDelegate = this;

        soundManager = gameObject.GetComponentInChildren<SoundManager>();

        foreach(Weapon weapon in gameObject.GetComponentsInChildren(typeof(IWeapon))){
            if(weapon.gameObject.name == "Left laser"){
                leftWeapon = weapon;
            }else if(weapon.gameObject.name == "Right laser"){
                rightWeapon = weapon;
            }
        }
        StartCoroutine("RegenerateEnergy");
    }

    // Call this method when an ability requires energy
    // returns true and drains energy if enough is available
    // returns false and does not drain if not enough is available
    public bool UseEnergy(float amount){
        if(amount > energy){
            return false;
        }
        energy -= amount;
        return true;
    }

    private IEnumerator RegenerateEnergy(){
        while(true){
            energy += Time.deltaTime * energyRegenRate;
            energy = Mathf.Min(maxEnergy, energy);
            Debug.Log(energy);
            yield return null;
        }
    }

    // Meters
    public void VelocityMeter(float amount){
        playerGUI.VelocityMeter(amount);
    }

    public void BoostMeter(float amount){
        playerGUI.BoostMeter(amount);
    }

    public void HPMeter(float amount){
        playerGUI.HPMeter(amount);
    }

    // Controls

    // Thrust
    public void XThrust(float amount){
        thrust.XThrust(amount);
    }

    public void YThrust(float amount){
        thrust.YThrust(amount);
    }

    public void ZThrust(float amount){
        thrust.ZThrust(amount);
    }

    // Boost
    public void BoostLeft(){
        thrust.BoostLeft();
    }

    public void BoostRight(){
        thrust.BoostRight();
    }

    public void BoostUp(){
        thrust.BoostUp();
    }

    public void BoostDown(){
        thrust.BoostDown();
    }

    // Weapons
    public void BeginFireLeftWeapon(){
        leftWeapon.BeginFire();
    }

    public void EndFireLeftWeapon(){
        leftWeapon.EndFire();
    }

    public void BeginFireRightWeapon(){
        rightWeapon.BeginFire();
    }

    public void EndFireRightWeapon(){
        rightWeapon.EndFire();
    }

    // Shield
    public void BeginShieldLeft(){
        shield.BeginShieldLeft();
    }

    public void EndShieldLeft(){
        shield.EndShieldLeft();
    }

    public void BeginShieldRight(){
        shield.BeginShieldRight();
    }

    public void EndShieldRight(){
        shield.EndShieldRight();
    }

    // Damage
    public void AddDamage(float amount){
        playerHealth.AddDamage(amount);
    }

    public void Destruct(){
        playerControl.Destruct();
        thrust.Destruct();
        //leftWeapon.Destruct();
        //rightWeapon.Destruct();

        score.AddPoint(player2);
    }

    public void Pause(bool player1){
        score.Pause(player1);
    }

    public void HitMessage(){
        playerGUI.HitMessage();
    }

    // Sound
    public void PlaySound(){
        soundManager.PlaySound();
    }
}
