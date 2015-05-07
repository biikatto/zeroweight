using UnityEngine;
using System.Collections;
 
[AddComponentMenu("Camera/PlayerControl")]
public class PlayerControl : MonoBehaviour{
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

    private string controllerName;
    private bool isXboxController;
    private int joystickNumber;

    public float dpadThreshold;
    public float fireThreshold;

    private bool leftWeaponFiring;
    private bool rightWeaponFiring;

    private bool boosting;

    void Start(){
    	playerDelegate = gameObject.GetComponent<PlayerDelegate>();

    	inputList = new Hashtable();
    	inputList.Add("X thrust", "X thrust");
    	inputList.Add("Z thrust", "Z thrust");
    	inputList.Add("Roll thrust", "Roll thrust");
    	inputList.Add("Mouse X", "Mouse X");
    	inputList.Add("Mouse Y", "Mouse Y");
    	inputList.Add("Fire left", "Fire left");
    	inputList.Add("Fire right", "Fire right");
    	inputList.Add("Shield left", "Shield left");
    	inputList.Add("Shield right", "Shield right");
    	inputList.Add("Camera select", "Camera select");
    	inputList.Add("Boost left", "Boost left");
    	inputList.Add("Boost right", "Boost right");
    	inputList.Add("Boost up", "Boost up");
    	inputList.Add("Boost down", "Boost down");
    	inputList.Add("Pause", "Pause");
    	foreach(string key in ((Hashtable)inputList.Clone()).Keys){
    		if(player2){
    			inputList[key] = inputList[key] + " 2";
    		}
    	}

		if(player2){
    		controllerName = Input.GetJoystickNames()[1];
    	}
    	else{
    		controllerName = Input.GetJoystickNames()[0];
    	}
    	if(controllerName == "Controller (Xbox 360 Wireless Receiver for Windows)"){
    		isXboxController = true;
    		Debug.Log("Xbox controller");
    	}else{
    		isXboxController = false;
    		Debug.Log("Some other controller");
    	}
    	if(player2){
    		joystickNumber = 2;
    	}else{
    		joystickNumber = 1;
    	}

    	dpadThreshold = 0.1f;
    	fireThreshold = 0.1f;
    	
    	leftWeaponFiring = false;
    	rightWeaponFiring = false;

    	boosting = false;
    }

    private void XboxUpdate(){
    	//---------------- Weapons ----------------------------------
    	if(Input.GetAxis("Xbox "+joystickNumber+" left trigger") > fireThreshold){
    		if(!leftWeaponFiring){
				playerDelegate.BeginFireLeftWeapon();
				leftWeaponFiring = true;
			}
		}
    	if(Input.GetAxis("Xbox "+joystickNumber+" left trigger") < fireThreshold){
    		if(leftWeaponFiring){
				playerDelegate.EndFireLeftWeapon();
				leftWeaponFiring = false;
			}
		}
    	if(Input.GetAxis("Xbox "+joystickNumber+" right trigger") > fireThreshold){
    		if(!rightWeaponFiring){
				playerDelegate.BeginFireRightWeapon();
				rightWeaponFiring = true;
			}
		}
    	if(Input.GetAxis("Xbox "+joystickNumber+" right trigger") < fireThreshold){
    		if(rightWeaponFiring){
				playerDelegate.EndFireRightWeapon();
				rightWeaponFiring = false;
			}
		}

    	//---------------- Shields ----------------------------------
    	if(Input.GetButtonDown("Xbox "+joystickNumber+" left bumper")){
			playerDelegate.BeginShieldLeft();
		}
    	if(Input.GetButtonUp("Xbox "+joystickNumber+" left bumper")){
			playerDelegate.EndShieldLeft();
		}
    	if(Input.GetButtonDown("Xbox "+joystickNumber+" right bumper")){
			playerDelegate.BeginShieldRight();
		}
    	if(Input.GetButtonUp("Xbox "+joystickNumber+" right bumper")){
			playerDelegate.EndShieldRight();
		}

    	//---------------- Camera ----------------------------------
		//if(Input.GetButtonDown((string)inputList["Camera select"])){
		//	if(firstPerson){
		//		gameObject.BroadcastMessage("ThirdPerson");
		//		firstPerson = false;
		//	}else{
		//		gameObject.BroadcastMessage("FirstPerson");
		//		firstPerson = true;
		//	}
		//}

    	//---------------- Pause ----------------------------------
    	if(Input.GetButtonDown("Xbox "+joystickNumber+" start")){
			playerDelegate.Pause(!player2);
		}

    	//---------------- Boost ----------------------------------
    	if(!boosting){
    		if(Input.GetAxis("Xbox "+joystickNumber+" dpad x") * -1.0f > dpadThreshold){
				playerDelegate.BoostLeft();
				boosting = true;
			}

    		if(Input.GetAxis("Xbox "+joystickNumber+" dpad x") > dpadThreshold){
				playerDelegate.BoostRight();
				boosting = true;
			}

    		if(Input.GetAxis("Xbox "+joystickNumber+" dpad y") > dpadThreshold){
				playerDelegate.BoostUp();
				boosting = true;
			}

    		if(Input.GetAxis("Xbox "+joystickNumber+" dpad y") * -1.0f > dpadThreshold){
				playerDelegate.BoostDown();
				boosting = true;
			}
		}else{
    		if(Mathf.Abs(Input.GetAxis("Xbox "+joystickNumber+" dpad x")) < dpadThreshold){
    			if(Mathf.Abs(Input.GetAxis("Xbox "+joystickNumber+" dpad y")) < dpadThreshold){
    				boosting = false;
    			}
    		}
		}

    	//---------------- Thrust ----------------------------------
    	playerDelegate.XThrust(Input.GetAxis("Xbox "+joystickNumber+" left x"));
    	playerDelegate.ZThrust(Input.GetAxis("Xbox "+joystickNumber+" left y"));
	}

	void Update(){
    	if(!destroyed){
    		if(isXboxController){
    			XboxUpdate();
    		}else{
    			//---------------- Weapons ----------------------------------
				if(Input.GetButtonDown((string)inputList["Fire left"])){
					playerDelegate.BeginFireLeftWeapon();
				}
				if(Input.GetButtonUp((string)inputList["Fire left"])){
					playerDelegate.EndFireLeftWeapon();
				}
				if(Input.GetButtonDown((string)inputList["Fire right"])){
					playerDelegate.BeginFireRightWeapon();
				}
				if(Input.GetButtonUp((string)inputList["Fire right"])){
					playerDelegate.EndFireRightWeapon();
				}

    			//---------------- Shields ----------------------------------
				if(Input.GetButtonDown((string)inputList["Shield left"])){
					playerDelegate.BeginShieldLeft();
				}
				if(Input.GetButtonUp((string)inputList["Shield left"])){
					playerDelegate.EndShieldLeft();
				}
				if(Input.GetButtonDown((string)inputList["Shield right"])){
					playerDelegate.BeginShieldRight();
				}
				if(Input.GetButtonUp((string)inputList["Shield right"])){
					playerDelegate.EndShieldRight();
				}

    			//---------------- Camera ----------------------------------
				if(Input.GetButtonDown((string)inputList["Camera select"])){
					if(firstPerson){
						gameObject.BroadcastMessage("ThirdPerson");
						firstPerson = false;
					}else{
						gameObject.BroadcastMessage("FirstPerson");
						firstPerson = true;
					}
				}

    			//---------------- Pause ----------------------------------
				if(Input.GetButtonDown((string)inputList["Pause"])){
					playerDelegate.Pause(!player2);
				}

    			//---------------- Boost ----------------------------------
				if(Input.GetButtonDown((string)inputList["Boost left"])){
					Debug.Log("Boost input");
					playerDelegate.BoostLeft();
				}

				if(Input.GetButtonDown((string)inputList["Boost right"])){
					playerDelegate.BoostRight();
				}

				if(Input.GetButtonDown((string)inputList["Boost up"])){
					playerDelegate.BoostUp();
				}

				if(Input.GetButtonDown((string)inputList["Boost down"])){
					playerDelegate.BoostDown();
				}

    			//---------------- Thrust ----------------------------------
    			playerDelegate.XThrust(Input.GetAxis((string)inputList["X thrust"]));
				playerDelegate.ZThrust(Input.GetAxis((string)inputList["Z thrust"]));
			}
		}
	}

	void FixedUpdate(){
    	if(!destroyed){

        	// Ensure the cursor is always locked when set
        	if(lockCursor){
        		Cursor.lockState = CursorLockMode.Locked;
        		Cursor.visible = false;
        	}

			Vector2 mouseDelta;
			float rollDelta = 0.0f;
        	if(isXboxController){
        		mouseDelta = new Vector2(Input.GetAxisRaw("Xbox "+joystickNumber+" right x"), Input.GetAxisRaw("Xbox "+joystickNumber+" right y"));
        	}else{
        		mouseDelta = new Vector2(Input.GetAxisRaw((string)inputList["Mouse X"]), Input.GetAxisRaw((string)inputList["Mouse Y"]));
        		rollDelta = Input.GetAxis((string)inputList["Roll thrust"]);
        	}

        	// Get raw mouse input for a cleaner reading on more sensitive mice.

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
