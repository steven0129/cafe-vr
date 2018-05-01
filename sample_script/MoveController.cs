using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour {
	[SerializeField]
	public Transform rig;

	private Valve.VR.EVRButtonId touchpad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;

	private SteamVR_TrackedObject trackedObject;
	private SteamVR_Controller.Device device {get {return SteamVR_Controller.Input((int)trackedObject.index);} }

	private Vector2 axis = Vector2.zero;

	// Use this for initialization
	void Start () 
	{
		trackedObject = GetComponent<SteamVR_TrackedObject> ();

	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		var device = SteamVR_Controller.Input ((int)trackedObject.index);
		if (device.GetAxis ().x != 0 || device.GetAxis ().y != 0) 
		{
			Debug.Log (device.GetAxis ().x + "" + device.GetAxis ().y);
		}
//		if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) )
//		{
//			Debug.Log ("Trigger press");
//			device.TriggerHapticPulse (700);
//		}
		if (device.GetTouch (touchpad)) {
			axis = device.GetAxis (Valve.VR.EVRButtonId.k_EButton_Axis0);

			if (rig != null) {
				
				rig.position += (transform.right * axis.x + transform.forward * axis.y) * Time.deltaTime * 3f;
				//多此一舉rig.position = new Vector3(rig.position.x, 0, rig.position.z); 
			}
		}

	}
}
