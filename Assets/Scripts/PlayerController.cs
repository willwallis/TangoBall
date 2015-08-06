using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Tango;

public class PlayerController : MonoBehaviour {

	public Text winText; // display Win message
	public float multiplier; // multiplies pose position for small spaces
	public Text multiplierDisplay; // displays multiplier
	private int count; // score, number of objects collected
	public Text countText; // displays score
	public Text poseText; // displays current pose position
	public Text playerText; // displays current player position (based on multiplier)
	public bool debug; // Determines whether pose and player position display in UI
	public GameObject m_targetFollowingObject; // Pose object to read from

	// Runs on start of game
	void Start() {
		count = 0; // set score to zero
		SetCountText(); // display zero score
		winText.text = ""; // make sure no text in win text
		Screen.sleepTimeout = SleepTimeout.NeverSleep; // stop android app going to sleep
		Application.targetFrameRate = 60; // helps with Tango pose data
		SetMultiplierText (); // displays current multiplier (set through variable)			
	}

	// Updates to player position made at end of process
	void LateUpdate ()
	{
		// Get the pose position (x, y, and z)
		float poseX = m_targetFollowingObject.transform.position.x;
		float poseY = m_targetFollowingObject.transform.position.y;
		float poseZ = m_targetFollowingObject.transform.position.z;

		// Create a vector based on pose X and Z (we don't want player going up and down) 
		// Multiply by multiplier, this is because in small room not enough space to move.
		Vector3 posePosition = new Vector3 (poseX * multiplier, 0.5f, poseZ * multiplier);

		// set player position to calculated pose position
		transform.position = posePosition;

		// if in debug mode show pose and player position
		if (debug == true)
			displayDebug (poseX, poseY, poseZ);
		}

	// Code to detect pick ups object collisions
	void OnTriggerEnter (Collider other) 
	{
		if(other.gameObject.CompareTag("Pick Up"))
		   {
			other.gameObject.SetActive(false);
			count = count + 1;
			SetCountText();
			if(count == 12)
				{
				winText.text = "You Win";
				}
		}
	}

	// Faster and Slower methods increase and decrease the multiplier
	public void Faster() {
		multiplier = multiplier + 1;
		SetMultiplierText ();
	}
	public void Slower() {
		multiplier = multiplier - 1;
		SetMultiplierText ();
	}

	// Single methods to set text for score and multiplier
	void SetCountText () {
		countText.text = "Score: " + count.ToString ();
	}
	void SetMultiplierText () {
		multiplierDisplay.text = multiplier.ToString();
	}

	// Display debug data if required.
	void displayDebug (float poseX, float poseY, float poseZ) {
		poseText.text = "Pose X: " + poseX.ToString ("0.00")
			+ " Y: " + poseY.ToString ("0.00") 
				+ " Z: " + poseZ.ToString ("0.00");
		
		float playerX = transform.position.x;
		float playerY = transform.position.y;
		float playerZ = transform.position.z;
		playerText.text = "Player X: " + playerX.ToString ("0.00")
			+ " Y: " + playerY.ToString ("0.00") 
				+ " Z: " + playerZ.ToString ("0.00");
	}

}
