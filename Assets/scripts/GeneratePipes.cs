using System.Collections.Generic;
using UnityEngine;

public class GeneratePipes : MonoBehaviour
{

    public GameObject pipes;

    // Use this for initialization
    void Start()
    {
        string songName = Scenes.getParameter("songName");
        print(songName);
        List<Note> notes = new MusicParser().parseMusic(songName);
        
        InvokeRepeating("CreateObstacle", 1f, 1.5f);
    }

    void CreateObstacle(){
        Vector3 pos = new Vector3(8, 0, 0);

        Instantiate(pipes, pos, Quaternion.identity);
    }
}
