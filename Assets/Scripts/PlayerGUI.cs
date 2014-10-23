using UnityEngine;
using System;
using System.Collections;

public class PlayerGUI : MonoBehaviour{
	[Tooltip("Check for horizontal split screen, leave unchecked for vertical split.")]
	public bool horizontalSplit = false;
	[Tooltip("Check if this is script is attached to player 2.")]
	public bool player2 = false;
	[Range(0,255)]
	[Tooltip("Global transparency for GUI elements.")]
	public byte transparency = 100;

	public Vector2 crosshairSize = new Vector2(80, 200);

	private Rect playerScreen;
	private Vector2 midScreen;

	private GUISkin skin;

	private Texture2D[] velocityBarTextures;
	private Texture2D[] boostBarTextures;
	private Texture2D[] hpBarTextures;
	private Texture2D crosshairTexture;
	private Texture2D crosshairHiliteTexture;

	private float velocity;
	public Vector2 velocityBarPosition;
	/*public*/ private bool verticalVelocityBar;
	public int velocitySegmentOffset;
	public Vector2 velocityBarSize = new Vector2(64, 16);
	private Rect[] velocityBarRect;
	private Vector2[] velocitySegPos;
	
	private float boostEnergy = 1f;
	public Vector2 boostBarPosition;
	/*public*/ private bool verticalBoostBar;
	public int boostSegmentOffset = 28;
	public Vector2 boostBarSize = new Vector2(64, 16);
	private Rect[] boostBarRect;
	private Vector2[] boostSegPos;

	private float hp = 1f;
	public Vector2 hpBarPosition;
	public Vector2 hpBarSize = new Vector2(256, 256);
	private Rect hpBarRect;

	public int crosshairNumber = 1;
	Rect crosshairRect;

	public int hitMessageFrames = 10;
	private int remainingHitMessageFrames;
	private bool displayingHitMessage = false;

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

		remainingHitMessageFrames = hitMessageFrames;


		boostBarPosition = new Vector2(
				playerScreen.xMin + 25,
				playerScreen.yMax - boostBarSize.y - 10);

		velocityBarPosition = new Vector2(
			boostBarPosition.x + 32,
			boostBarPosition.y - boostBarSize.y - 10);

		hpBarPosition = new Vector2(
			playerScreen.xMax - hpBarSize.x - 10,
			playerScreen.yMax - hpBarSize.y - 10);


		skin = Resources.Load<GUISkin>("defaultGUI");
		
		string crosshairPath = "UI/Crosshairs/" + crosshairNumber + "-1";
		string crosshairHilitePath = "UI/Crosshairs/" + crosshairNumber + "-2";

		crosshairTexture = Resources.Load<Texture2D>(crosshairPath);
		crosshairHiliteTexture = Resources.Load<Texture2D>(crosshairHilitePath);

		int velocityBarSegments = 6;
		int boostBarSegments = 6;
		int hpBarSegments = 11;

		velocityBarTextures = new Texture2D[velocityBarSegments];
		velocitySegPos = new Vector2[velocityBarSegments];
		velocityBarRect = new Rect[velocityBarSegments];
		for(int i=0;i<velocityBarSegments;i++){
			string filename = "UI/Accel" + (i + 1);
			velocityBarTextures[i] = Resources.Load<Texture2D>(filename);
			Debug.Log(velocityBarTextures);
			velocitySegPos[i] = velocityBarPosition;
			if(verticalVelocityBar){
				velocitySegPos[i].y += velocitySegmentOffset * i;
			}else{
				velocitySegPos[i].x += velocitySegmentOffset * i;
			}
			velocityBarRect[i] = new Rect(
					velocitySegPos[i].x,
					velocitySegPos[i].y,
					velocityBarSize.x,
					velocityBarSize.y);
		}

		boostBarTextures = new Texture2D[boostBarSegments];
		boostSegPos = new Vector2[boostBarSegments];
		boostBarRect = new Rect[boostBarSegments];
		for(int i=0;i<boostBarSegments;i++){
			string filename = "UI/Cooldown" + (i + 1);
			boostBarTextures[i] = Resources.Load<Texture2D>(filename);
			boostSegPos[i] = boostBarPosition;
			if(verticalBoostBar){
				boostSegPos[i].y += boostSegmentOffset * i;
			}else{
				boostSegPos[i].x += boostSegmentOffset * i;
			}
			boostBarRect[i] = new Rect(
					boostSegPos[i].x,
					boostSegPos[i].y,
					boostBarSize.x,
					boostBarSize.y);
		}

		hpBarTextures = new Texture2D[hpBarSegments];
		for(int i=0;i<hpBarSegments;i++){
			string filename = "UI/HPBar/" + (i).ToString("d2");
			hpBarTextures[i] = Resources.Load<Texture2D>(filename);
		}
		hpBarRect = new Rect(
			hpBarPosition.x,
			hpBarPosition.y,
			hpBarSize.x,
			hpBarSize.y);

		midScreen = playerScreen.center;
				
		crosshairRect = new Rect(
				midScreen.x-(crosshairSize.x/2),
				midScreen.y-(crosshairSize.y/2),
				crosshairSize.x,
				crosshairSize.y);

	}

	void OnGUI(){
		// Set GUI color for transparency
		GUI.color = new Color32(255, 255, 255, transparency);
		drawCrosshair();
		drawBoostBar();
		drawVelocityBar();
		drawHPBar();
	}

	void drawCrosshair(){
		if(displayingHitMessage){
			Debug.Log("pubup");
			GUI.DrawTexture(crosshairRect, crosshairHiliteTexture);
			if(--remainingHitMessageFrames <= 0){
				displayingHitMessage = false;
			}
		}else{
			GUI.DrawTexture(crosshairRect, crosshairTexture);
		}
	}

	void drawBoostBar(){
		int segments = (int)(boostBarTextures.Length*boostEnergy);
		for(int i=0;i<segments;i++){
			GUI.DrawTexture(boostBarRect[i], boostBarTextures[i]);
		}
	}

	void drawVelocityBar(){
		int segments = (int)(velocityBarTextures.Length*velocity);
		for(int i=0;i<segments;i++){
			GUI.DrawTexture(velocityBarRect[i], velocityBarTextures[i]);
		}
	}

	void drawHPBar(){
		GUI.DrawTexture(
			hpBarRect,
			hpBarTextures[(int)((hpBarTextures.Length-1)*hp)]);
	}

	public void VelocityMeter(float amount){
		velocity = amount;
		velocity = Math.Min(1f, velocity);
		velocity = Math.Max(0f, velocity);
	}

	public void BoostMeter(float energy){
		boostEnergy = energy;
		boostEnergy = Math.Min(1f, boostEnergy);
		boostEnergy = Math.Max(0f, boostEnergy);
	}

	public void HPMeter(float amount){
		hp = amount;
		hp = Math.Min(1f, hp);
		hp = Math.Max(0f, hp);
	}

	public void HitMessage(){
		// Confirm successful weapon hit
		Debug.Log("Weapon hit");
		displayingHitMessage = true;
		remainingHitMessageFrames = hitMessageFrames;
	}
}
