using UnityEngine;

public class GeneratePipes : MonoBehaviour
{

    public GameObject pipes;
    public float moveFactor = 1.0f;
    float pipeMin;

    // Use this for initialization
    void Start()
    {
        pipeMin = moveFactor * -1;
        InvokeRepeating("CreateObstacle", 1f, 1.5f);
    }

    void CreateObstacle(){
        print(moveFactor);
        float rv = Random.Range(pipeMin, moveFactor);
        print(rv);
        Vector3 pos = new Vector3(8, rv, 0);

        Instantiate(pipes, pos, Quaternion.identity);
    }
}
