using UnityEngine;
using System.Collections;

public class PlayerGUI : MonoBehaviour{
	[Tooltip("Check for horizontal split screen, leave unchecked for vertical split.")]
	public bool horizontalSplit = false;
	[Tooltip("Check if this is script is attached to player 2.")]
	public bool player2 = false;
	public Texture2D crosshairTexture;
	[Range(0,255)]
	[Tooltip("Global transparency for GUI elements.")]
	public byte transparency = 100;

	public Vector2 crosshairSize = new Vector2(80, 200);

	[Header("Boost bar settings")]
	public Vector2 boostBarPosition;
	public Vector2 boostBarSize = new Vector2(20, 60);
	public Color32 boostBarColor = new Color32(78, 187, 255, 100);
	private Rect boostBarRect;

	private Rect playerScreen;
	private Vector2 midScreen;
	private float _boostEnergy = 1f;

	private GUISkin skin;

	Rect crosshairRect;

	void Start(){
		if(player2){
			playerScreen = new Rect(
					Screen.width/2,
					0,
					Screen.width/2,
					Screen.height);
		}else{
			playerScreen = new Rect(
					0,
					0,
					Screen.width/2,
					Screen.height);
		}

		skin = Resources.Load<GUISkin>("defaultGUI");

		crosshairTexture = Resources.Load<Texture2D>("crosshair");

		midScreen = playerScreen.center;

		boostBarPosition = new Vector2(
				playerScreen.xMax - boostBarSize.x - 10,
				playerScreen.yMax - (boostBarSize.y + 10));

		boostBarRect = new Rect(
				boostBarPosition.x,
				boostBarPosition.y,
				boostBarSize.x,
				boostBarSize.y);
				
		crosshairRect = new Rect(
				midScreen.x-(crosshairSize.x/2),
				midScreen.y-(crosshairSize.y/2),
				crosshairSize.x,
				crosshairSize.y);

	}

	void OnGUI(){
		// Set GUI color for transparency
		GUI.color = new Color32(255, 255, 255, transparency);
		GUI.DrawTexture(crosshairRect, crosshairTexture);
		drawBoostBar();
	}

	void drawBoostBar(){
		GUI.skin = skin;
		GUI.color = boostBarColor; 
		GUI.Box(boostBarRect, "");
		GUI.skin = null;
	}

	void boostEnergy(float energy){
		_boostEnergy = energy;
	}
}
