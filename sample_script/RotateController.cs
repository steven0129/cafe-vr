using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateController : MonoBehaviour {
	[SerializeField]
	private Transform rig;

	float rotSpeed = 0.5f;

	private Valve.VR.EVRButtonId touchpad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;

	private SteamVR_TrackedObject trackedObject;
	private SteamVR_Controller.Device device {get {return SteamVR_Controller.Input((int)trackedObject.index);} }

	void Start () 
	{
		trackedObject = GetComponent<SteamVR_TrackedObject> ();
	}

	void FixedUpdate () 
	{
		var device = SteamVR_Controller.Input ((int)trackedObject.index);
		if (device.GetAxis ().x != 0 || device.GetAxis ().y != 0) 
		{
			Debug.Log (device.GetAxis ().x + "" + device.GetAxis ().y);
		}

		float rotx = device.GetAxis (Valve.VR.EVRButtonId.k_EButton_Axis0).x * rotSpeed * Mathf.Deg2Rad;

		if (device.GetTouch (touchpad)) {
			
			rig.transform.RotateAround( Vector3.up, rotx);


			}
		}

	}
