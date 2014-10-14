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

	private Rect playerScreen;
	private Vector2 midScreen;
	private float _boostEnergy = 1f;

	private GUISkin skin;

	private Texture2D[] accelBarTextures;
	private Texture2D[] boostBarTextures;
	private Texture2D[] hpBarTextures;

	public Vector2 accelerationBarPosition;
	public bool verticalAccelerationBar;
	public int accelerationSegmentOffset;
	private Vector2[] accelSegPos;
	
	public Vector2 boostBarPosition;
	public bool verticalBoostBar;
	public int boostSegmentOffset = 28;
	public Vector2 boostBarSize = new Vector2(64, 16);
	private Rect[] boostBarRect;
	private Vector2[] boostSegPos;

	public Vector2 hpBarPosition;
	private Vector2[] hpSegPos;

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

		accelerationBarPosition = new Vector2(
				playerScreen.xMin + 10,
				playerScreen.yMax - 10);

		boostBarPosition = new Vector2(
				playerScreen.xMin + 25,
				playerScreen.yMax - boostBarSize.y - 10);


		skin = Resources.Load<GUISkin>("defaultGUI");

		crosshairTexture = Resources.Load<Texture2D>("crosshair");

		int accelBarSegments = 6;
		int boostBarSegments = 6;
		int hpBarSegments = 10;

		accelBarTextures = new Texture2D[accelBarSegments];
		accelSegPos = new Vector2[accelBarSegments];
		for(int i=0;i<accelBarSegments;i++){
			string filename = "UI/Accel" + (i + 1);
			accelBarTextures[i] = Resources.Load<Texture2D>(filename);
			accelSegPos[i] = accelerationBarPosition;
			if(verticalAccelerationBar){
				accelSegPos[i].y += accelerationSegmentOffset;
			}else{
				accelSegPos[i].x += accelerationSegmentOffset;
			}
		}

		boostBarTextures = new Texture2D[boostBarSegments];
		boostSegPos = new Vector2[boostBarSegments];
		boostBarRect = new Rect[boostBarSegments];
		for(int i=0;i<boostBarSegments;i++){
			string filename = "UI/Cooldown" + (i + 1);
			boostBarTextures[i] = Resources.Load<Texture2D>(filename);
			Debug.Log(boostBarTextures[i]);
			boostSegPos[i] = boostBarPosition;
			if(verticalBoostBar){
				boostSegPos[i].y += boostSegmentOffset * i;
			}else{
				boostSegPos[i].x += (boostSegmentOffset * i);
			}
			boostBarRect[i] = new Rect(
					boostSegPos[i].x,
					boostSegPos[i].y,
					boostBarSize.x,
					boostBarSize.y);
			Debug.Log(boostBarRect[i]);
		}

		hpBarTextures = new Texture2D[hpBarSegments];
		for(int i=0;i<hpBarSegments;i++){
			string filename = "UI/HP" + (i + 1);
			hpBarTextures[i] = Resources.Load<Texture2D>(filename);
		}


		string[] boostBarNames = new string[6];
		string[] hpBarNames = new string[6];

		midScreen = playerScreen.center;

		//boostBarPosition = new Vector2(
		//		playerScreen.xMax - boostBarSize.x - 10,
		//		playerScreen.yMax - (boostBarSize.y + 10));

		//boostBarRect = new Rect(
		//		boostBarPosition.x,
		//		boostBarPosition.y,
		//		boostBarSize.x,
		//		boostBarSize.y);
				
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
		for(int i=0;i<boostBarTextures.Length;i++){
			GUI.DrawTexture(boostBarRect[i], boostBarTextures[i]);
		}
	}

	void boostEnergy(float energy){
		_boostEnergy = energy;
	}
}
