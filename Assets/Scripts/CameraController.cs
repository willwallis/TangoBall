using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Tango;

public class CameraController : MonoBehaviour {

	public GameObject player; // player object (mapped)
	public GameObject m_targetFollowingObject; // Pose object to read from
	public Text cameraAngle; // debug field to display camera angle
	public Text cameraPosition; // debug field to display camera position
	public bool debug; // flag to indicate whether to show debug fields
	public float cameraDistance; // how far camera should be from player 

	// starting input offset and X, Y, and Z euler angles
	private Vector3 input; 
	private float inputX;
	private float inputZ;
	private float inputY;

	// Determine initial offset
	void Start () {
		input = transform.position - player.transform.position;
		inputY = input.y;
		inputX = transform.rotation.eulerAngles.x; // Use start value for X roatation
		inputZ = transform.rotation.eulerAngles.z; // Use start value for Z roatation
	}
	
	// Late update is called once per frame.
	// Code points camera at player from standard height and angle but point in
	// direction aligning with forward movement of tablet.
	void LateUpdate () {

		// Obtain pose rotation in y direction
		float poseY = m_targetFollowingObject.transform.rotation.eulerAngles.y;

		// Set main camera y rotation base on pose. No X or Y rotation.
		// Rotation uses quarternion which is a pain so using euler and converting
		Quaternion poseRotation = Quaternion.Euler (inputX, poseY, inputZ);
		transform.rotation = poseRotation;

		// Set Camera Position base on pose rotation
		// Convert to radians and find X and Z to keep camera constant distance from player
		poseY = poseY * (Mathf.PI / 180);
		float offsetX = -Mathf.Sin(poseY) * cameraDistance;
		float offsetZ = -Mathf.Cos(poseY) * cameraDistance;
		Vector3 rotateOffset = new Vector3 (offsetX, inputY, offsetZ);
		transform.position = player.transform.position + rotateOffset;

		// Display debug data if required
		if (debug == true)
			displayDebug (offsetX.ToString("0.0") , inputY.ToString("0.0") , offsetZ.ToString("0.0") );

	}

	// Displays camera position and rotation for debuging
	void displayDebug(string offsetX, string inputY, string offsetZ) {
		// Set Camera Position Text
		cameraPosition.text = "Offset X: " + offsetX 
			+ " Offset Y: " + inputY 
				+ " Offset Z: " + offsetZ;
		
		// Set Camera Angle Text
		cameraAngle.text = "Angle X: " + transform.rotation.eulerAngles.x.ToString ("0") 
			+ " Angle Y: " + transform.rotation.eulerAngles.y.ToString ("0")
				+ " Angle Z: " + transform.rotation.eulerAngles.z.ToString ("0");
	}
}
