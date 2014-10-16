using UnityEngine;
using System.Collections;

public class ScoreKeeper : MonoBehaviour{
	int player1Points;
	int player2Points;
	private bool player1isWinner = true;

	private bool paused = false;
	private bool hasBeenPaused = false;
	private bool player1paused = false;

	public float scoreScreenTime = 5f;
	float scoreScreenStart;

	private GUIStyle roundFGStyle;
	private GUIStyle roundBGStyle;

	private GUIStyle p1scoreFGStyle;
	private GUIStyle p1scoreBGStyle;

	private GUIStyle p2scoreFGStyle;
	private GUIStyle p2scoreBGStyle;

	bool displayingScoreMessage = false;

	void Start(){
		DontDestroyOnLoad(gameObject);
		if(GameObject.FindObjectsOfType(typeof(ScoreKeeper)).Length > 1){
			if((player1Points == 0) && (player2Points == 0)){
				Destroy(this);
			}
		}

		roundFGStyle = new GUIStyle(GUIStyle.none);
		roundFGStyle.alignment = TextAnchor.MiddleCenter;
		roundFGStyle.font = Resources.Load("UI/Typefaces/Devil Breeze Bold") as Font;
		roundFGStyle.fontSize = 50;
		roundFGStyle.normal.textColor = Color.white;

		roundBGStyle = new GUIStyle(roundFGStyle);
		roundBGStyle.normal.textColor = Color.black;
		roundBGStyle.contentOffset = new Vector2(-4, -2);

		p1scoreFGStyle = new GUIStyle(roundFGStyle);
		p1scoreFGStyle.fontSize = 22;
		p1scoreFGStyle.alignment = TextAnchor.MiddleRight;
		p1scoreFGStyle.contentOffset = new Vector2(-5, 0);

		p1scoreBGStyle = new GUIStyle(roundBGStyle);
		p1scoreBGStyle.fontSize = 22;
		p1scoreBGStyle.alignment = TextAnchor.MiddleRight;
		p1scoreBGStyle.contentOffset = new Vector2(-5, 0);

		p2scoreFGStyle = new GUIStyle(p1scoreFGStyle);
		p2scoreFGStyle.alignment = TextAnchor.MiddleLeft;
		p2scoreFGStyle.contentOffset = new Vector2(5, 0);

		p2scoreBGStyle = new GUIStyle(p1scoreBGStyle);
		p2scoreBGStyle.alignment = TextAnchor.MiddleLeft;
		p2scoreBGStyle.contentOffset = new Vector2(5, 0);
	}

	void OnGUI(){
		if(displayingScoreMessage){
			DisplayScoreMessage();
		}
		DisplayScore();
	}

	void Update(){
		if(displayingScoreMessage){
			if((scoreScreenStart + scoreScreenTime) < Time.time){
				Application.LoadLevel("Arena");
				displayingScoreMessage = false;
			}
		}
	}

	void DisplayScore(){
		GUI.Label(new Rect(0,0,Screen.width/2,p1scoreBGStyle.lineHeight),
				  player1Points.ToString(),
				  p1scoreBGStyle);
		GUI.Label(new Rect(0,0,Screen.width/2,p1scoreFGStyle.lineHeight),
				  player1Points.ToString(),
				  p1scoreFGStyle);
		GUI.Label(new Rect(Screen.width/2,0,Screen.width/2,p2scoreBGStyle.lineHeight),
				  player2Points.ToString(),
				  p2scoreBGStyle);
		GUI.Label(new Rect(Screen.width/2,0,Screen.width/2,p2scoreFGStyle.lineHeight),
				  player2Points.ToString(),
				  p2scoreFGStyle);
	}

	void DisplayScoreMessage(){
		string message;
		if(player1isWinner){
			message = "Player 1 wins the round!";
		}else{
			message = "Player 2 wins the round!";
		}
		GUI.Box(
			new Rect(0, 0, Screen.width, Screen.height),
			message, roundBGStyle);
		GUI.Box(
			new Rect(0, 0, Screen.width, Screen.height),
			message, roundFGStyle);
	}

	public void Pause(bool player1){
		if(player1){
			Debug.Log("1");
			Debug.Log(player1paused);
		}else{
			Debug.Log("2");
			Debug.Log(player1paused);
		}
		if((!hasBeenPaused) |
				(paused && (player1paused && player1)) |
				(paused && (!player1paused && !player1)) |
				(!paused)){
			_pause();
			if(!paused){
				player1paused = player1;
				paused = true;
				hasBeenPaused = true;
			}else{
				paused = false;
			}
		}
	}

	void _pause(){
		if(Time.timeScale > 0f){
			Debug.Log("Pause");
			Time.timeScale = 0f;
		}else{
			Debug.Log("Unpause");
			Time.timeScale = 1f;
		}
	}

	public void AddPoint(bool player1won){
		if(player1won){
			player1Points++;
			player1isWinner = true;
		}else{
			player2Points++;
			player1isWinner = false;
		}
		scoreScreenStart = Time.time;
		displayingScoreMessage = true;
	}
}
