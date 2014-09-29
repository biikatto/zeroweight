using UnityEngine;
 
[AddComponentMenu("Camera/Steer")]
public class Steer : MonoBehaviour
{
    Vector2 _smoothMouse;

    float _rollAbsolute;
    float _smoothRoll;
 
    public bool lockCursor;
    public Vector2 sensitivity = new Vector2(2, 2);
    public Vector2 smoothing = new Vector2(3, 3);
    public float rollSensitivity = 10f;
    public float rollSmoothing = 1f;
 
    void Start(){}
 
    void FixedUpdate(){
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
 
