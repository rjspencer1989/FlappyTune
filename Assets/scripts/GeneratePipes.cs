using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GeneratePipes : MonoBehaviour
{
    Camera mainCamera;
    public GameObject pipeBox;
    double heightPerNote = 0.5;
    float yPos = 1;

    float lowPosition = 0.0f;
    float highPosition = 0.0f;

    double offset = 0;

    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;
        highPosition = pipeBox.transform.Find("ScoreBox").transform.localScale.y;
        lowPosition = -highPosition;
        string songName = Scenes.getParameter("songName");
        Song song = MusicParser.ParseScore(songName);

        List<Note> notes = new List<Note>();
        foreach (var item in song.MeasureElements){
            if(item.Type == MeasureElementType.Note){
                notes.Add(item.Element as Note);
            }
        }
        
        List<Note> sorted = notes.OrderBy(o=>o.Pitch, new PitchComparer()).GroupBy(o=>o.Pitch).Select(o=>o.First()).ToList();
        Note lowest = sorted.First();
        Note highest = sorted.Last();
        
        offset = highest.Pitch.getPitchOffset(lowest.Pitch);
        heightPerNote = (highPosition * 2) / offset;
        new WaitForSeconds(2.0f);
        StartCoroutine(CreateObstacle(notes, sorted));
    }

    public IEnumerator CreateObstacle(List<Note> notes, List<Note> sorted){
        foreach (Note item in notes){
            if(!item.IsRest){
                GameObject score =  Instantiate(pipeBox, Vector3.zero, Quaternion.identity) as GameObject;
                yPos = lowPosition + (float)(heightPerNote * (sorted.IndexOf(item)));
                Vector3 pos = new Vector3(8, yPos, 0);
                score.transform.Translate(pos);
            }
            yield return new WaitForSeconds(2.0f);
        }
    }
}
