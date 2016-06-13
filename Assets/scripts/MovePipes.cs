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
}
