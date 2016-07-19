using System.Xml;
using UnityEngine;
public static class MusicParser {
    public static XmlDocument LoadFile(string name){
        TextAsset asset = Resources.Load(name) as TextAsset;
        XmlDocument scoreDocument = new XmlDocument();
        scoreDocument.LoadXml(asset.text);
        return scoreDocument;
    }

    public static void ParseScore(string name){
        XmlDocument doc = LoadFile(name);
        Score score = new Score();
        XmlNodeList partNodes = doc.SelectNodes("score-partwise/part-list/part");
        if(partNodes != null){
            foreach (XmlNode partNode in partNodes){
                Part part = new Part();
                score.Parts.Add(part);
                if(partNode.Attributes != null){
                    part.Id = partNode.Attributes["id"].InnerText;
                }

                XmlNode partName = partNode.SelectSingleNode("part-name");
                if(partName != null){
                    part.Name = partName.InnerText;
                }

                string measurePath = string.Format("//part[@id='{0}']/measure", part.Id);
                XmlNodeList measureNodes = partNode.SelectNodes(measurePath);

                if(measureNodes != null){
                    foreach (XmlNode measureNode in measureNodes){
                        Measure measure = new Measure();
                        
                    }
                }
            }
        }
    }
}
