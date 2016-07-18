using System.Xml;
using UnityEngine;
public static class MusicParser {
    static XmlDocument score;
    public static void LoadFile(string name){
        TextAsset asset = Resources.Load(name) as TextAsset;
        score = new XmlDocument();
        score.LoadXml(asset.text);
    }

    public static void ParseScore(string name){
        LoadFile(name);
        
    }
}