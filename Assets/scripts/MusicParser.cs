using UnityEngine;

using System.Xml;
using System.Collections.Generic;
using System;

class MusicParser{
    private List<Note> notes;
    XmlDocument loadMusic(string name){
        TextAsset asset = Resources.Load(name) as TextAsset;
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(asset.text);
        return doc;
    }

    public List<Note> parseMusic(string songName){
        String step = ""; 
        String type = "";
        int octave = -1;
        int duration = -1;
        notes = new List<Note>();
        XmlDocument music = loadMusic(songName);
        XmlNodeList bars = music.DocumentElement.SelectNodes("/score-partwise/part/measure/note");
        for(int i = 0; i < bars.Count; i++){
            XmlNode pitch = bars[i].SelectSingleNode("pitch");
            if(pitch != null){ // rests don't have a pitch
                step = pitch.SelectSingleNode("step").InnerText;
                octave = Int32.Parse(pitch.SelectSingleNode("octave").InnerText);
            } else if(bars[i].SelectSingleNode("rest") != null){
                octave = 0;
                step = "rest";
            }
            type = bars[i].SelectSingleNode("type").InnerText;
            duration = Int32.Parse(bars[i].SelectSingleNode("duration").InnerText);
            Note note = new Note(step, octave, type, duration);
            notes.Add(note);
        }
        return notes;
    }
}