﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	private Rigidbody2D rb;
	public float speed;
	public Text scoreText;
	int score = 0;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		setScoreText();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
		float input = Input.GetAxis ("Jump");
		rb.AddForce (new Vector2(0.0f, input) * speed);
	}

	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.CompareTag("edge") || other.gameObject.CompareTag("pipe")){
			this.gameObject.SetActive(false);
			SceneManager.LoadScene ("GameOver");
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("score")){
			score += 1;
			setScoreText();
		}
	}

	void setScoreText(){
		scoreText.text = "Score: " + score.ToString();
	}
}
