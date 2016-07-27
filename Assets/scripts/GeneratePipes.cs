using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GeneratePipes : MonoBehaviour{
    public GameObject pipeBox;
    public string instrument = "church_organ";
    double heightPerNote = 0.5;

    const int SECONDS_IN_MINUTE = 60;
    float yPos = 1;

    float beatDuration = 0.0f;

    float lowPosition = 0.0f;
    float highPosition = 0.0f;
    Song song = null;
    List<Note> notesUsed;

    double offset = 0;

    // Use this for initialization
    void Start()
    {
        highPosition = pipeBox.transform.Find("ScoreBox").transform.localScale.y;
        lowPosition = -highPosition;
        string songName = Scenes.getParameter("songName");
        song = MusicParser.ParseScore(songName);

        List<Note> notes = new List<Note>();
        foreach (var measure in song.Measures){
            if(measure.Direction.Type.MetronomeMark.PerMinute != 0){
                beatDuration = SECONDS_IN_MINUTE / measure.Direction.Type.MetronomeMark.PerMinute;
                print(beatDuration);
            }
            foreach (var item in measure.MeasureElements){
                if(item.Type == MeasureElementType.Note){
                    notes.Add(item.Element as Note);
                }    
            }
        }
        
        notesUsed = notes.OrderBy(o=>o.Pitch, new PitchComparer()).GroupBy(o=>o.Pitch).Select(o=>o.First()).ToList();
        Note lowest = notesUsed.First();
        Note highest = notesUsed.Last();
        
        offset = highest.Pitch.getPitchOffset(lowest.Pitch);
        heightPerNote = (highPosition * 2) / offset;
        new WaitForSeconds(2.0f);
        StartCoroutine("CreateObstacle");
    }

    public IEnumerator CreateObstacle(){
        foreach (Measure measure in song.Measures){
            if(measure.Direction.Type.MetronomeMark.PerMinute != 0){
                beatDuration = SECONDS_IN_MINUTE / measure.Direction.Type.MetronomeMark.PerMinute;
            }
            foreach (MeasureElement item in measure.MeasureElements){
                switch (item.Type){
                    case MeasureElementType.Note:
                        Note note = item.Element as Note;
                        if(!note.IsRest){
                            GameObject score =  Instantiate(pipeBox, Vector3.zero, Quaternion.identity) as GameObject;
                            AudioSource audioSource = score.transform.Find("ScoreBox").GetComponent<AudioSource>();
                            string audioPath = string.Format("Instruments/{0}/{1}{2}", instrument, note.Pitch.Step, note.Pitch.Octave);
                            audioSource.clip = Resources.Load(audioPath) as AudioClip;
                            if(note.Pitch.Alter != 0){
                                audioSource.pitch += Mathf.Pow(2, note.Pitch.Alter / 12);
                            }
                            yPos = lowPosition + (float)(heightPerNote * (notesUsed.IndexOf(note)));
                            Vector3 pos = new Vector3(8, yPos, 0);
                            score.transform.Translate(pos);
                        }
                        break;
                }
                yield return new WaitForSeconds(beatDuration);
            }
        }
    }
}
