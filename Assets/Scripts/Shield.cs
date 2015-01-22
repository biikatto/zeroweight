using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

    private GameObject leftShield;
    private GameObject rightShield;
    private PlayerDelegate pDelegate;

    private bool upkeepActive = false;

    [SerializeField]
    private float energyCost = 1f;

    public float EnergyCost{
        get{ return energyCost; }
        set{ energyCost = value; }
    }

    private void Start(){
        leftShield = Instantiate(Resources.Load("Prefabs/Player1/HalfShield"), transform.position, Quaternion.Euler(0, 90, 0)) as GameObject;
        leftShield.transform.parent = transform;
        rightShield = Instantiate(Resources.Load("Prefabs/Player1/HalfShield"), transform.position, Quaternion.Euler(0, -90, 0)) as GameObject;
        rightShield.transform.parent = transform;

        leftShield.SetActive(false);
        rightShield.SetActive(false);
    }

    public void BeginShieldLeft(){
        leftShield.SetActive(true);
        StartCoroutine("EnergyUpkeep");
    }
    
    public void EndShieldLeft(){
        leftShield.SetActive(false);
    }

    public void BeginShieldRight(){
        rightShield.SetActive(true);
        StartCoroutine("EnergyUpkeep");
    }
    
    public void EndShieldRight(){
        rightShield.SetActive(false);
    }

    private IEnumerator EnergyUpkeep(){
        if(!upkeepActive){
            upkeepActive = true;
            while(upkeepActive){
                if(leftShield.active && rightShield.active){
                    pDelegate.UseEnergy(Time.deltaTime * energyCost * 2);
                }else if(leftShield.active || rightShield.active){
                    pDelegate.UseEnergy(Time.deltaTime * energyCost);
                }else{
                    upkeepActive = false;
                    break;
                }
                yield return null;
            }
        }
    }
}
