using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class GeneratePipes : MonoBehaviour
{
    Camera mainCamera;
    public GameObject pipes;
    float heightPerNote = 0.5f;
    float yPos = 1;

    float lowPosition = 0.0f;
    float highPosition = 0.0f;

    int offset = 0;

    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;
        highPosition = (mainCamera.orthographicSize - 0.5f);
        lowPosition = -highPosition;
        string songName = Scenes.getParameter("songName");
        TextAsset asset = Resources.Load(songName) as TextAsset;
        string json = asset.text;
        Song song = JsonUtility.FromJson<Song>(json);
        List<Note> sorted = song.notes.OrderBy(o=>o.pitch.getPitchValue()).ToList();
        Note lowest = sorted.First();
        Note highest = sorted.Last();
        print(lowest.pitch.ToString());
        print(highest.pitch.ToString());
        offset = highest.pitch.getPitchOffset(lowest.pitch);
        print(offset);
        heightPerNote = (highPosition * 2) / offset;
        print(heightPerNote);
        new WaitForSeconds(2.0f);
        StartCoroutine(CreateObstacle(song));
    }

    public IEnumerator CreateObstacle(Song song){
        foreach (Note item in song.notes){
            //GameObject pipeBox =  Instantiate(pipes, Vector3.zero, Quaternion.identity) as GameObject;
            //GameObject score = pipeBox.transform.Find("Score").gameObject;
            yPos = (heightPerNote * item.pitch.getStepNum()) + (score.transform.lossyScale.y / 2);
            Vector3 pos = new Vector3(8, yPos, 0);
            print(pos);
            //pipeBox.transform.Translate(pos);

            yield return new WaitForSeconds(1.5f);
        }

        // keep score box fixed height
        // keep score box bounds between min and max
        // calculate mid point of score box
        // pipes will be positioned appropriately
    }
}
