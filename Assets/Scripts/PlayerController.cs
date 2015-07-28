using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	private Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody>();
	}

	// Before performing phsyics calculations
	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis("Horizontal"); // Get data from keyboard
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce(movement * speed);

	}
	// Before rendering a frame
	void Update ()
	{
		
	}
}
