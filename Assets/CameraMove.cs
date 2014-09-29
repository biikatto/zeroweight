using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
	bool firstPerson = true;
	public Vector3 thirdPersonPosition = new Vector3(-5, 5, -30);
	public Vector3 thirdPersonRotation = new Vector3(8, 8, 0);

	void Setup(){
	}

	void FirstPerson(){
		transform.Rotate(Vector3.left, thirdPersonRotation.y);
		transform.Rotate(Vector3.down, thirdPersonRotation.x);
		transform.Translate(-1 * thirdPersonPosition);
		firstPerson = true;
	}

	void ThirdPerson(){
		transform.Translate(thirdPersonPosition);
		transform.Rotate(Vector3.up, thirdPersonRotation.x);
		transform.Rotate(Vector3.right, thirdPersonRotation.y);
		firstPerson = false;
	}
}
