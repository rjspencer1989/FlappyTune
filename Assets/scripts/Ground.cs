using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Ground : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.CompareTag ("Player")) {
			other.gameObject.SetActive (false);
			SceneManager.LoadScene ("GameOver");
		}
	}
}
