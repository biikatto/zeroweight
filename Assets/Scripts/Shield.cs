using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

    private GameObject leftShield;
    private GameObject rightShield;
    private PlayerDelegate pDelegate;

    private bool upkeepActive = false;

    [SerializeField]
        private float energyCost = 15f;

    public float EnergyCost{
        get{ return energyCost; }
        set{ energyCost = value; }
    }

    public PlayerDelegate PDelegate{
        get{ return pDelegate; }
        set{ pDelegate = value; }
    }

    private void Start(){
        leftShield = Instantiate(Resources.Load("Prefabs/Player1/HalfShield"), transform.position, Quaternion.Euler(0, -90, 0)) as GameObject;
        leftShield.transform.parent = transform;
        leftShield.tag = "Shield";

        rightShield = Instantiate(Resources.Load("Prefabs/Player1/HalfShield"), transform.position, Quaternion.Euler(0, 90, 0)) as GameObject;
        rightShield.transform.parent = transform;
        rightShield.tag = "Shield";

        leftShield.SetActive(false);
        rightShield.SetActive(false);
    }

    public void BeginShieldLeft(){
        if(!leftShield.active){
            leftShield.SetActive(true);
            StartCoroutine("EnergyUpkeep");
        }
    }

    public void EndShieldLeft(){
        if(leftShield.active){
            leftShield.SetActive(false);
        }
    }

    public void BeginShieldRight(){
        if(!rightShield.active){
            rightShield.SetActive(true);
            StartCoroutine("EnergyUpkeep");
        }
    }

    public void EndShieldRight(){
        if(rightShield.active){
            rightShield.SetActive(false);
        }
    }

    // Begin both shields
    public void BeginShield(){
        BeginShieldLeft();
        BeginShieldRight();
    }

    // End both shields
    public void EndShield(){
        EndShieldLeft();
        EndShieldRight();
    }

    // Drain energy, if none is available, deactivate shields
    public void Impact(float damage){
        if(!pDelegate.UseEnergy(damage)){
            EndShield();
        }
    }

    private IEnumerator EnergyUpkeep(){
        if(!upkeepActive){
            upkeepActive = true;
            while(upkeepActive){
                // Both shields active
                if(leftShield.active && rightShield.active){
                    if(!pDelegate.UseEnergy(Time.deltaTime * energyCost * 2)){
                        leftShield.SetActive(false);
                        rightShield.SetActive(false);
                        upkeepActive = false;
                    }
                }else{
                    // One shield active
                    if(leftShield.active){
                        if(!pDelegate.UseEnergy(Time.deltaTime * energyCost)){
                            Debug.Log("Insufficient energy, deactivating left shield");
                            leftShield.SetActive(false);
                            upkeepActive = false;
                        }
                    }else if(rightShield.active){
                        if(!pDelegate.UseEnergy(Time.deltaTime * energyCost)){
                            Debug.Log("Insufficient energy, deactivating right shield");
                            rightShield.SetActive(false);
                            upkeepActive = false;
                        }
                    }
                    // No shields active
                    else{
                        upkeepActive = false;
                    }
                }
                yield return null;
            }
        }
    }
}
