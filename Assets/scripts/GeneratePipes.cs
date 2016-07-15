using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GeneratePipes : MonoBehaviour
{
    Camera mainCamera;
    public GameObject pipes;
    float heightPerNote = 0.5f;
    float yPos = 1;

    List<string> steps;

    // Use this for initialization
    void Start()
    {
        steps = new List<string>(new string[]{"C", "D", "E", "F", "G", "A", "B"});
        mainCamera = Camera.main;
        string songName = Scenes.getParameter("songName");
        TextAsset asset = Resources.Load(songName) as TextAsset;
        string json = asset.text;
        Song song = JsonUtility.FromJson<Song>(json);
        
        //Find lowest note
        //Find highest note
        //Calculate difference
        //divide visible height by number of notes
        new WaitForSeconds(2.0f);
        StartCoroutine(CreateObstacle(song));
    }

    public IEnumerator CreateObstacle(Song song){
        foreach (Note item in song.notes){

            yPos = heightPerNote * (steps.IndexOf(item.pitch.step) + 1);
            Vector3 pos = new Vector3(8, yPos, 0);

            print(pos);

            Instantiate(pipes, pos, Quaternion.identity);

            yield return new WaitForSeconds(1.5f);
        }
    }
}
