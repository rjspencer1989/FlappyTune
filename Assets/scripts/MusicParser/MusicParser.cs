using System;
using System.Globalization;
using System.Xml;
using UnityEngine;

public static class MusicParser {
    public static XmlDocument LoadFile(string name){
        TextAsset asset = Resources.Load(name) as TextAsset;
        XmlDocument scoreDocument = new XmlDocument();
        scoreDocument.LoadXml(asset.text);
        return scoreDocument;
    }

    public static Score ParseScore(string name){
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
                        decimal w;
                        if(measureNode.Attributes != null){
                            if(decimal.TryParse(measureNode.Attributes["width"].InnerText, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out w)){
                                measure.Width = w;
                            }
                        }
                        XmlNode attributesNode = measureNode.SelectSingleNode("Attributes");
                        if(attributesNode != null){
                            measure.Attributes = new MeasureAttributes();

                            XmlNode divisionsNode = attributesNode.SelectSingleNode("divisions");
                            if(divisionsNode != null){
                                measure.Attributes.Divisions = Convert.ToInt32(divisionsNode.InnerText);
                            }

                            XmlNode keyNode = attributesNode.SelectSingleNode("key");
                            if(keyNode != null){
                                measure.Attributes.Key = new Key();
                                XmlNode fifthNode = keyNode.SelectSingleNode("fifths");
                                if(fifthNode != null){
                                    measure.Attributes.Key.Fifths = Convert.ToInt32(fifthNode.InnerText);
                                }

                                XmlNode modeNode = keyNode.SelectSingleNode("mode");
                                if(modeNode != null){
                                    measure.Attributes.Key.Mode = modeNode.InnerText;
                                }
                            }

                            measure.Attributes.Time = GetTime(attributesNode);
                            measure.Attributes.Clef = GetClef(attributesNode);
                        }

                        XmlNodeList childNodes = measureNode.ChildNodes;

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
                                measure.MeasureElements.Add(measureElement);
                            }
                        }
                        part.Measures.Add(measure);
                    }
                }
            }
        }
        return score;
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

        subNode.SelectSingleNode("rest");
        if(subNode != null){
            note.IsRest = true;
        }
        return note;
    }

    private static Clef GetClef(XmlNode node){
        Clef clef = new Clef();
        return clef;
    }

    private static Time GetTime(XmlNode node){
        Time time = new Time();
        return time;
    }

    private static Pitch GetPitch(XmlNode node){
        Pitch pitch = new Pitch();
        return pitch;
    }
}
