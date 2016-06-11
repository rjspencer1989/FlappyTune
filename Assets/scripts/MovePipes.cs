using UnityEngine;
using System.Collections;

public class MovePipes : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate (new Vector3 (-0.05f, 0, 0));
	}

	void OnTriggerEnter2D(Collider2D other){
		print (other.tag);
		if(other.gameObject.CompareTag("Respawn")){
			print ("respawn");
			this.gameObject.SetActive(false);
		}
	}
}
