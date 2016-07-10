using UnityEngine;
using MusicXml.Domain;

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
        Score score = MusicXml.MusicXmlParser.GetScore(songName);
        Part p = score.Parts[0];
        foreach (Measure measure in p.Measures)
        {
            foreach (MeasureElement me in measure.MeasureElements)
            {
                Note n = me.Element as Note;
                print(n.Pitch.Step+ " " + n.Pitch.Octave);
            }
        }
    }

    void CreateObstacle(){
        yPos += heightPerNote;
        Vector3 pos = new Vector3(8, yPos, 0);

        print(pos);

        Instantiate(pipes, pos, Quaternion.identity);
    }
}
