using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour{
    void Start(){
        Screen.lockCursor = true;
    }

    public void LoadGame(){
        Application.LoadLevel("Arena");
    }
}
