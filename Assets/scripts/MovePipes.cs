using UnityEngine;

public class MovePipes : MonoBehaviour {
	void FixedUpdate () {
		this.transform.Translate (new Vector3 (-0.05f, 0, 0));
    }
}
