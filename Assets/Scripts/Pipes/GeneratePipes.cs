using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
//using MusicParser;

public class GeneratePipes : MonoBehaviour{
    public GameObject pipeBox;
    public string instrument = "church_organ";
    double heightPerNote = 0.5;

    const int SECONDS_IN_MINUTE = 60;
    float yPos = 1;

    float beatDuration = 0.0f;

    float lowPosition = 0.0f;
    float highPosition = 0.0f;

    double offset = 0;
    float updateInterval = 0.0f;
    List<Pipe> pipes;

    // Use this for initialization
    void Start(){
        highPosition = pipeBox.transform.Find("ScoreBox").transform.localScale.y;
        lowPosition = -highPosition;
        string songName = Scenes.getParameter("songName");
        TextAsset asset = Resources.Load("Songs/" + songName) as TextAsset;
        Stream stream = new MemoryStream(asset.bytes);
        BinaryFormatter bf = new BinaryFormatter();
        List<Pipe> pipes = (List<Pipe>)bf.Deserialize(stream);
        // notesUsed = notes.OrderBy(o=>o.Pitch, new PitchComparer()).GroupBy(o=>o.Pitch).Select(o=>o.First()).ToList();
        // Note lowest = notesUsed.First();
        // Note highest = notesUsed.Last();

        // offset = highest.Pitch.getPitchOffset(lowest.Pitch);
        // heightPerNote = (highPosition * 2) / offset;
        new WaitForSeconds(2.0f);
        StartCoroutine(CreateObstacle(pipes));
    }

    void FixedUpdate(){
        updateInterval = Time.deltaTime;
    }

    public IEnumerator CreateObstacle(List<Pipe> pipes){
        foreach (var item in pipes){
            if(item != null){
                GameObject score = Instantiate(pipeBox, Vector3.zero, Quaternion.identity) as GameObject;
                AudioSource audioSource = score.transform.Find("ScoreBox").GetComponent<AudioSource>();
                string audioPath = string.Format("Instruments/{0}/{1}{2}", instrument, item.Step, item.Octave);
                audioSource.clip = Resources.Load(audioPath) as AudioClip;
                if (item.Alter != 0){
                    print(item.Alter);
                    float adjust = Mathf.Pow(2, (item.Alter / 12.0f));
                    print(adjust);
                    audioSource.pitch *= adjust;
                }
            }
            yield return new WaitForSecondsRealtime(SECONDS_IN_MINUTE / item.Tempo);
        }
        //         //use time sig beat type as beat def.
        //         //get bpm from metronome, to calculate time duration of 1 beat
        //         //eg if beat is quarter note, and metronome is 60, each quarter note lasts 1 second.
        //         //this is base setting
        //         //each pipe is 0.5 units at x scale 1.
        //         //each pipe moved 0.05 units per fixedupdate
        //         //num movements to enter and leave pipe is width/movement eg 0.5/0.05 = 10 movements
        //         //each movement is updateInterval often eg 0.02
        //         //(width/movement) * updateInterval = time from enter to leave eg 10 * 0.02 = 0.2 seconds
        //         //need width/movement * updateInterval == beat length.
        //         //need xscale such that width/movement * updateInterval == beat length
        //         //if beat time == 0.2 at xscale 1 beat xscale must be 5 so that beat time == 1 second.
        //         //multiply or divide by two as many times as difference between beat type and type of note.
        //         //eg if beat type is quarter and notes are halves the xscale of a half note would be twice that of a quarter.
        //         //eg if beat type is quarter and notes are eigths, the xscale will be half of a quarter
        //         //set the waitforseconds value to create the next pipe in time
        //         //if beat is quarter and 80bpm, each quarter is 0.75 seconds, so waitforseconds = 0.75
        //         //if note is half, waitforseconds = 1.5 seconds
        //         //if note is eighth, waitforseconds = 0.375 seconds etc


        //         if(measure.Direction.Type.MetronomeMark.PerMinute != 0){
        //             beatDuration = SECONDS_IN_MINUTE / measure.Direction.Type.MetronomeMark.PerMinute;
        //         }
        //         foreach (MeasureElement item in measure.MeasureElements){
        //             switch (item.Type){
        //                 case MeasureElementType.Note:
        //                     Note note = item.Element as Note;
        //                     if(!note.IsRest){
        //                         GameObject score =  Instantiate(pipeBox, Vector3.zero, Quaternion.identity) as GameObject;
        //                         AudioSource audioSource = score.transform.Find("ScoreBox").GetComponent<AudioSource>();
        //                         string audioPath = string.Format("Instruments/{0}/{1}{2}", instrument, note.Pitch.Step, note.Pitch.Octave);
        //                         audioSource.clip = Resources.Load(audioPath) as AudioClip;
        //                         if(note.Pitch.Alter != 0){
        //                             audioSource.pitch += Mathf.Pow(2, note.Pitch.Alter / 12);
        //                         }
        //                         yPos = lowPosition + (float)(heightPerNote * (notesUsed.IndexOf(note)));
        //                         print(yPos);
        //                         Vector3 pos = new Vector3(8, yPos, 1);
        //                         score.transform.Translate(pos);
        //                     }
        //                     break;
        //             }
        //             yield return new WaitForSeconds(beatDuration);
        //         }
        //     }
        
    }
}
