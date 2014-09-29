using UnityEngine;
 
[AddComponentMenu("Camera/PlayerControl")]
public class PlayerControl : MonoBehaviour
{
    Vector2 _smoothMouse;

    float _rollAbsolute;
    float _smoothRoll;
 
    public bool lockCursor = true;
    public Vector2 sensitivity = new Vector2(0.4f, 0.4f);
    public Vector2 smoothing = new Vector2(8, 8);
    public float rollSensitivity = 10f;
    public float rollSmoothing = 1f;

    bool firstPerson = true;

    GameObject leftWeapon;
    GameObject rightWeapon;
 
    void Start(){
    	Transform[] allChildren = gameObject.GetComponentsInChildren<Transform>();
    	foreach(Transform child in allChildren){
    		if(child.gameObject.name == "Left laser"){
    			leftWeapon = child.gameObject;
    		}else if(child.gameObject.name == "Right laser"){
    			rightWeapon = child.gameObject;
    		}
    	}
    }
 
    void FixedUpdate(){
		if(Input.GetButton("Fire left")){
			leftWeapon.BroadcastMessage("Fire");
		}
		if(Input.GetButton("Fire right")){
			rightWeapon.BroadcastMessage("Fire");
		}
		if(Input.GetButtonDown("Camera select")){
			if(firstPerson){
				gameObject.BroadcastMessage("ThirdPerson");
				firstPerson = false;
			}else{
				gameObject.BroadcastMessage("FirstPerson");
				firstPerson = true;
			}
		}
    	// Broadcast thrust messages
		transform.BroadcastMessage("XThrust", Input.GetAxis("X thrust"));
		transform.BroadcastMessage("YThrust", Input.GetAxis("Y thrust"));
		transform.BroadcastMessage("ZThrust", Input.GetAxis("Z thrust"));

        // Ensure the cursor is always locked when set
        Screen.lockCursor = lockCursor;
 
        // Get raw mouse input for a cleaner reading on more sensitive mice.
        var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        var rollDelta = Input.GetAxis("Roll thrust");
 
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
 
