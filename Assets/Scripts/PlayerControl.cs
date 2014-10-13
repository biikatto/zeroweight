using UnityEngine;
using System.Collections;
 
[AddComponentMenu("Camera/PlayerControl")]
public class PlayerControl : MonoBehaviour
{
	PlayerDelegate playerDelegate;

    Vector2 _smoothMouse;

    float _rollAbsolute;
    float _smoothRoll;
 
 	public bool player2 = false;
    public bool lockCursor = true;
    public Vector2 sensitivity = new Vector2(0.4f, 0.4f);
    public Vector2 smoothing = new Vector2(8, 8);
    public float rollSensitivity = 10f;
    public float rollSmoothing = 1f;

    bool firstPerson = true;

    private bool destroyed = false;

    private Hashtable inputList;
 
    void Start(){
    	playerDelegate = gameObject.GetComponent<PlayerDelegate>();

    	inputList = new Hashtable();
    	inputList.Add("X thrust", "X thrust");
    	inputList.Add("Y thrust", "Y thrust");
    	inputList.Add("Z thrust", "Z thrust");
    	inputList.Add("Roll thrust", "Roll thrust");
    	inputList.Add("Mouse X", "Mouse X");
    	inputList.Add("Mouse Y", "Mouse Y");
    	inputList.Add("Fire left", "Fire left");
    	inputList.Add("Fire right", "Fire right");
    	inputList.Add("Camera select", "Camera select");
    	inputList.Add("Boost", "Boost");
    	if(player2){
    		foreach(string key in ((Hashtable)inputList.Clone()).Keys){
    			inputList[key] = inputList[key] + " 2";
    			Debug.Log(inputList[key]);
    		}
    	}
    }
 
    void FixedUpdate(){
    	if(!destroyed){
			if(Input.GetButton((string)inputList["Fire left"])){
				playerDelegate.FireLeftWeapon();
			}
			if(Input.GetButton((string)inputList["Fire right"])){
				playerDelegate.FireRightWeapon();
			}
			if(Input.GetButtonDown((string)inputList["Camera select"])){
				if(firstPerson){
					gameObject.BroadcastMessage("ThirdPerson");
					firstPerson = false;
				}else{
					gameObject.BroadcastMessage("FirstPerson");
					firstPerson = true;
				}
			}

			if(Input.GetButtonDown((string)inputList["Boost"])){
				transform.BroadcastMessage("Boost");
			}

    		playerDelegate.XThrust(Input.GetAxis((string)inputList["X thrust"]));
			playerDelegate.YThrust(Input.GetAxis((string)inputList["Y thrust"]));
			playerDelegate.ZThrust(Input.GetAxis((string)inputList["Z thrust"]));

        	// Ensure the cursor is always locked when set
        	Screen.lockCursor = lockCursor;
 	 
        	// Get raw mouse input for a cleaner reading on more sensitive mice.
        	Vector2 mouseDelta = new Vector2(Input.GetAxisRaw((string)inputList["Mouse X"]), Input.GetAxisRaw((string)inputList["Mouse Y"]));
        	float rollDelta = Input.GetAxis((string)inputList["Roll thrust"]);
 	 
        	// Scale input against the sensitivity setting and multiply that against the smoothing value.
        	mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity.x * smoothing.x, sensitivity.y * smoothing.y));
        	rollDelta = rollDelta * rollSensitivity * rollSmoothing;

 	 
        	// Interpolate controls over time to apply smoothing delta.
        	_smoothMouse.x = Mathf.Lerp(_smoothMouse.x, mouseDelta.x, 1f / smoothing.x);
        	_smoothMouse.y = Mathf.Lerp(_smoothMouse.y, mouseDelta.y, 1f / smoothing.y);
        	_smoothRoll = Mathf.Lerp(_smoothRoll, rollDelta, 1f / rollSmoothing);
 	 
        	transform.Rotate(-_smoothMouse.y, _smoothMouse.x, _smoothRoll);  
			//rigidbody.AddTorque(transform.forward * rollThrust * rollPower);
		}
    }

    public void Destruct(){
    	destroyed = true;
    }
}
