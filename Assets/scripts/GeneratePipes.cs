using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeneratePipes : MonoBehaviour
{
    Camera mainCamera;
    public GameObject pipes;
    float heightPerNote = 0.0f;
    float yPos = 0;

    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;
        string songName = Scenes.getParameter("songName");
        print(songName);
        List<Note> notes = new MusicParser().parseMusic(songName);
        List<Note> sortedNotes = notes.OrderBy(o => o.getPitch().getPitchVal()).ToList();
        Pitch lastPitch = sortedNotes.Last().getPitch();
        Pitch firstPitch = sortedNotes.First().getPitch();
        int noteRange = lastPitch.getPitchOffset(firstPitch);
        foreach (Note item in sortedNotes)
        {
            print(item.ToString());
        }

        heightPerNote = (2 * Camera.main.orthographicSize) / noteRange;
        print(heightPerNote);
        yPos = Camera.main.orthographicSize * -1;

        InvokeRepeating("CreateObstacle", 1f, 1.5f);
    }

    void CreateObstacle(){
        yPos += heightPerNote;
        Vector3 pos = new Vector3(8, yPos, 0);

        print(pos);

        Instantiate(pipes, pos, Quaternion.identity);
    }
}
