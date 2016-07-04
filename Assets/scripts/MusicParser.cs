using UnityEngine;

using System.Xml;
using System.Collections.Generic;
using System;

class MusicParser : MonoBehaviour{
    XmlDocument loadMusic(string name){
        TextAsset asset = Resources.Load(name) as TextAsset;
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(asset.text);
        return doc;
    }

    void Start(){
        List<Note> notes = new List<Note>();
        XmlDocument music = loadMusic("star");
        XmlNodeList bars = music.DocumentElement.SelectNodes("/score-partwise/part/measure/note");
        foreach(XmlNode item in bars){
            XmlNode pitch = item.SelectSingleNode("pitch");
            Note note = new Note(pitch.SelectSingleNode("step").InnerText, Int32.Parse(pitch.SelectSingleNode("octave").InnerText), item.SelectSingleNode("type").InnerText, Int32.Parse(item.SelectSingleNode("duration").InnerText));
            print(note.ToString());
            notes.Add(note);
        }
    }
}