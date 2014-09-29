using UnityEngine;
using System.Collections;

public class PlayerGUI : MonoBehaviour{
	public Texture2D guiTexture;
	public Vector2 crosshairSize = new Vector2(80, 200);
	Vector2 midScreen;

	Rect crosshairRect;

	void Start(){
		midScreen = new Vector2(Screen.width/2, Screen.height/2);
		crosshairRect = new Rect(
				midScreen.x-(crosshairSize.x/2),
				midScreen.y-(crosshairSize.y/2),
				crosshairSize.x,
				crosshairSize.y);

	}

	void OnGUI(){
		GUI.color = new Color32(255, 255, 255, 100);
		GUI.DrawTexture(crosshairRect, guiTexture);
	}
}
