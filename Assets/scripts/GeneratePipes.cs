using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GeneratePipes : MonoBehaviour
{
    Camera mainCamera;
    public GameObject scoreBox;
    public GameObject pipe;
    double heightPerNote = 0.5;
    float yPos = 1;

    float lowPosition = 0.0f;
    float highPosition = 0.0f;

    double offset = 0;

    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;
        highPosition = (mainCamera.orthographicSize - scoreBox.transform.localScale.y);
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
                GameObject score =  Instantiate(scoreBox, Vector3.zero, Quaternion.identity) as GameObject;
                GameObject lowerPipe = Instantiate(pipe, Vector3.zero, Quaternion.identity) as GameObject;
                GameObject upperPipe = Instantiate(pipe, Vector3.zero, Quaternion.identity) as GameObject;
                yPos = lowPosition + (float)(heightPerNote * (sorted.IndexOf(item)));
                Vector3 pos = new Vector3(8, yPos, 0);
                score.transform.Translate(pos);
                Vector3 bottomPos = new Vector3(pos.x, pos.y, pos.z);
                Vector3 topPos = new Vector3(pos.x, pos.y, pos.z);
                float topOffset = mainCamera.orthographicSize - (topPos.y + (score.transform.localScale.y / 2));
                upperPipe.transform.localScale = new Vector3(upperPipe.transform.localScale.x, topOffset, upperPipe.transform.localScale.z);
                topPos.y = mainCamera.orthographicSize - (topOffset / 2);
                topPos.z = -1;
                upperPipe.transform.Translate(topPos);
                float bottomOffset = (mainCamera.orthographicSize * -1) - (bottomPos.y - (score.transform.localScale.y / 2));
                lowerPipe.transform.localScale = new Vector3(lowerPipe.transform.localScale.x, (bottomOffset * -1), lowerPipe.transform.localScale.z);
                bottomPos.y = -mainCamera.orthographicSize - (bottomOffset / 2);
                bottomPos.z = -1;
                lowerPipe.transform.Translate(bottomPos);
            }
            yield return new WaitForSeconds(2.0f);
        }
    }
}
