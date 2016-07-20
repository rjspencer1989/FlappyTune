using System;
using System.Xml;
using UnityEngine;
using System.IO;

public static class MusicParser{
    public static string LoadFile(string name){
        TextAsset asset = Resources.Load(name) as TextAsset;
        return asset.text;
    }

    public static Song ParseScore(string name){
        Song song = new Song();
        string doc = LoadFile(name);
        XmlTextReader reader = new XmlTextReader(new StringReader(doc));
        while(reader.Read()){
            switch (reader.NodeType){
                case XmlNodeType.Element:
                    if(reader.Name == "measure"){
                        var inner = reader.ReadOuterXml();
                        XmlDocument innerDoc = new XmlDocument();
                        innerDoc.LoadXml(inner);
                        XmlNode measureNodes = innerDoc.SelectSingleNode("measure");
                        XmlNodeList childNodes = measureNodes.ChildNodes;
                        foreach (XmlNode node in childNodes){
                            MeasureElement measureElement = null;
                            if(node.Name == "note"){
                                measureElement = new MeasureElement {Type = MeasureElementType.Note, Element = GetNote(node)};
                            } else if(node.Name == "backup"){
                                measureElement = new MeasureElement {Type = MeasureElementType.Backup, Element = GetBackupElement(node)};
                            } else if(node.Name == "forward"){
                                measureElement = new MeasureElement {Type = MeasureElementType.Forward, Element = GetForwardElement(node)};
                            }

                            if(measureElement != null){
                                song.MeasureElements.Add(measureElement);
                            }
                        }
                    }
                break;
            }
        }
        return song;
    }

    private static Forward GetForwardElement(XmlNode node){
        Forward forward = new Forward();
        XmlNode forwardNode = node.SelectSingleNode("duration");
        if(forwardNode != null){
            forward.Duration = Convert.ToInt32(forwardNode.InnerText);
        }
        return forward;
    }

    private static Backup GetBackupElement(XmlNode node){
        Backup backup = new Backup();
        XmlNode backupNode = node.SelectSingleNode("duration");
        if(backupNode != null){
            backup.Duration = Convert.ToInt32(backupNode.InnerText);
        }
        return backup;
    }

    private static Note GetNote(XmlNode node){
        Note note = new Note();
        XmlNode subNode = node.SelectSingleNode("type");
        if(subNode != null){
            note.Type = subNode.InnerText;
        }

        subNode = node.SelectSingleNode("voice");
        if(subNode != null){
            note.Voice = Convert.ToInt32(subNode.InnerText);
        }

        subNode = node.SelectSingleNode("duration");
        if(subNode != null){
            note.Duration = Convert.ToInt32(subNode.InnerText);
        }

        note.Pitch = GetPitch(node);

        subNode = node.SelectSingleNode("staff");
        if(subNode != null){
            note.Staff = Convert.ToInt32(subNode.InnerText);
        }

        subNode = node.SelectSingleNode("chord");
        if(subNode != null){
            note.IsChordTone = true;
        }

        subNode= node.SelectSingleNode("rest");
        if(subNode != null){
            note.IsRest = true;
        }
        return note;
    }

    private static Pitch GetPitch(XmlNode node){
        Pitch pitch = new Pitch();
        XmlNode subNode = node.SelectSingleNode("pitch");
        if(subNode != null){
            XmlNode pitchPartNode = subNode.SelectSingleNode("step");
            if(pitchPartNode != null){
                pitch.Step = pitchPartNode.InnerText[0];
            }

            pitchPartNode = subNode.SelectSingleNode("alter");
            if(pitchPartNode != null){
                pitch.Alter = Convert.ToInt32(pitchPartNode.InnerText);
            }

            pitchPartNode = subNode.SelectSingleNode("octave");
            if(pitchPartNode != null){
                pitch.Octave = Convert.ToInt32(pitchPartNode.InnerText);
            }
        } else {
            return null;
        }
        return pitch;
    }
}
