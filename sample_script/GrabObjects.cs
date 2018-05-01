using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObjects : MonoBehaviour {

	public GameObject wine;
    public GameObject grass;

	private SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device device { get { return SteamVR_Controller.Input ((int)trackedObj.index); } }

	private GameObject collidingObject;
	private GameObject objectInHand;

	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}

	private void SetCollidingObject(Collider col)
	{
		if (collidingObject || !col.GetComponent<Rigidbody> ())
		{
			return;
		}

		collidingObject = col.gameObject;
	}

	public void OnTriggerEnter (Collider other)
	{
		SetCollidingObject (other);
	}

	public void OnTriggerStay (Collider other)
	{
		SetCollidingObject (other);
	}

	public void OnTriggerExit (Collider other)
	{
		if (!collidingObject) 
		{
			return;
		}

		collidingObject = null;
	}

	private void GrabObject()
	{
		objectInHand = collidingObject;
		collidingObject = null;

		var joint = AddFixedJoint();
		joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
	}

	private FixedJoint AddFixedJoint()
	{
		FixedJoint fx = gameObject.AddComponent<FixedJoint> ();
		fx.breakForce = 20000;
		fx.breakTorque = 20000;
		return fx;
	}

	private void ReleaseObject()
	{
		if (GetComponent<FixedJoint> ()) 
		{
			GetComponent<FixedJoint> ().connectedBody = null;
			Destroy (GetComponent<FixedJoint> ());

			objectInHand.GetComponent<Rigidbody> ().velocity = device.velocity;
			objectInHand.GetComponent<Rigidbody> ().angularVelocity = device.angularVelocity;
		}

		objectInHand = null;
	}

	void Update()
	{
		if (device.GetHairTriggerDown ()) 
		{
			if (collidingObject) 
			{
                if (collidingObject == wine)
                {
				if (wine.GetComponent<value> ().decide == 0) {
					wine.GetComponent<value> ().decide = 1;
				}
                }
                if (collidingObject == grass)
                {
                    if (grass.GetComponent<value>().decide == 0)
                    {
                        grass.GetComponent<value>().decide = 1;
                    }
                }
				GrabObject ();
			}
		}

		if (device.GetHairTriggerUp ()) 
		{
			if (objectInHand) 
			{
				ReleaseObject ();
			}
		}
	}
}
