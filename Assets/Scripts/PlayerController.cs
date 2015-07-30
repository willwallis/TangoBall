using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Text winText;
	public float speed;
	public Text countText;

	private Rigidbody rb;
	private int count;

	void Start() {
		rb = GetComponent<Rigidbody>();
		count = 0;
		SetCountText();
		winText.text = "";
	}

	// Before performing phsyics calculations
	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis("Horizontal"); // Get data from keyboard
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce(movement * speed);

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

	void SetCountText () {
		countText.text = "Score: " + count.ToString ();
	}

	// Before rendering a frame
	void Update ()
	{
		
	}
}
