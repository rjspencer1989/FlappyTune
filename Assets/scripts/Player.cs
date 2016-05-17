using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	private Rigidbody2D rb;
	public float speed;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
		float input = Input.GetAxis ("Jump");
		rb.AddForce (new Vector2(0.0f, input) * speed);
	}
		
}
