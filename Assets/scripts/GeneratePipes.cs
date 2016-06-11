using UnityEngine;
using System.Collections;

public class GeneratePipes : MonoBehaviour {

	public GameObject pipes;

	// Use this for initialization
	void Start()
	{
		InvokeRepeating("CreateObstacle", 1f, 1.5f);
	}

	void CreateObstacle()
	{
		Instantiate(pipes);
	}
}
