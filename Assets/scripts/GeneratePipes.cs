using UnityEngine;

public class GeneratePipes : MonoBehaviour
{
    Camera mainCamera;
    public GameObject pipes;
    float heightPerNote = 0.0f;
    float yPos = 1;

    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;
        string songName = Scenes.getParameter("songName");
        print(songName);
        TextAsset asset = Resources.Load(songName) as TextAsset;
        string json = asset.text;
        Song song = JsonUtility.FromJson<Song>(json);
    }

    void CreateObstacle(){
        yPos += heightPerNote;
        Vector3 pos = new Vector3(8, yPos, 0);

        print(pos);

        Instantiate(pipes, pos, Quaternion.identity);
    }
}
