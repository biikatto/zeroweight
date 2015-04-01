using UnityEngine;

public class CursorSettings : MonoBehaviour{
		
    public void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}
}
