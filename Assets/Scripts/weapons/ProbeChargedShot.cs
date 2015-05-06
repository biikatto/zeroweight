/* * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * ChargedShot.cs
 *
 * Charged shot weapon. Fire quickly for minimum power,
 * hold and release for charged shot.
 *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * */

using UnityEngine;
using System;
using System.Collections;

[System.Serializable]
public class ProbeChargedShot : ChargedShot{
	public bool left;

	private Animator animator;

    //-------------------------------------------
    //*********** Functions *********************
    //-------------------------------------------
    new void Start(){
    	left = true;
    	foreach(Animator candidateAnimator in transform.root.GetComponentsInChildren<Animator>()){
    		if(candidateAnimator.gameObject.name == "Ship_Bullet3"){
    			animator = candidateAnimator;
    		}
    	}

    	Debug.Log(animator);
    	animator.SetTrigger("Rest");
    	base.Start();
    }

    //----------- Firing functions --------------
    public override void EndFire(){
    	base.EndFire();
    	if(left){
    		Debug.Log("Left");
    		animator.SetTrigger("Punch Left");
    		animator.SetTrigger("Rest");
    	}else{
    		Debug.Log("Right");
    		animator.SetTrigger("Punch Right");
    		animator.SetTrigger("Rest");
    	}
    }
}
