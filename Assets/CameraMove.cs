using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
	bool firstPerson = true;
	public float cameraDistance = 10;

	void Setup(){
	}

	void Update(){
		if(Input.GetButtonDown("Camera select")){
			//Debug.Log("Camera move down");
			if(firstPerson){
				transform.Translate(Vector3.back * cameraDistance);
				firstPerson = false;
			}else{
				transform.Translate(Vector3.forward * cameraDistance);
				firstPerson = true;
			}
		}
	}
}
