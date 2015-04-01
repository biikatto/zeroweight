using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour{
    public void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void LoadGame(){
        Application.LoadLevel("Arena");
    }
}
