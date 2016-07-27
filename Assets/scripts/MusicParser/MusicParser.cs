using System;
using System.Xml;
using UnityEngine;
using System.IO;
using System.Globalization;

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
                        XmlNode measureNode = innerDoc.SelectSingleNode("measure");
                        Measure measure = new Measure();
                        if(measureNode.Attributes != null){
                            var measureWidthAttribute = measureNode.Attributes["width"];
                            decimal w;
                            if (measureWidthAttribute != null && decimal.TryParse(measureWidthAttribute.InnerText, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture,out w)){
                                measure.Width = w;
                            }
                            var measureNumberAttribute = measureNode.Attributes["number"];
                            int n;
                            if (measureNumberAttribute != null && int.TryParse(measureNumberAttribute.InnerText, NumberStyles.Integer, CultureInfo.InvariantCulture,out n)){
                                measure.Number = n;
                            }
                        }

                        var attributesNode = measureNode.SelectSingleNode("attributes");
                        if(attributesNode != null){
                            measure.MeasureAttributes = new MeasureAttribute();
                            var divisionNode = attributesNode.SelectSingleNode("divisions");
                            if(divisionNode != null){
                                measure.MeasureAttributes.Divisions = Convert.ToInt32(divisionNode.InnerText);
                            }

                            var keyNode = attributesNode.SelectSingleNode("key");
                            if(keyNode != null){
                                measure.MeasureAttributes.Key = new Key();
                                var fifthsNode = keyNode.SelectSingleNode("fifths");
                                if(fifthsNode != null){
                                    measure.MeasureAttributes.Key.Fifths = Convert.ToInt32(fifthsNode.InnerText);
                                }
                                var modeNode = keyNode.SelectSingleNode("mode");
                                if(modeNode != null){
                                    measure.MeasureAttributes.Key.Mode = modeNode.InnerText;
                                }
                            }

                            measure.MeasureAttributes.Time = GetTime(attributesNode);
                            measure.MeasureAttributes.Clef = GetClef(attributesNode);
                        }

                        var directionNode = measureNode.SelectSingleNode("direction");
                        if(directionNode != null){
                            measure.Direction = new DirectionElement();
                            var soundNode = directionNode.SelectSingleNode("sound");
                            if(soundNode != null){
                                measure.Direction.Sound = new SoundElement();
                                var tempoAttribute = soundNode.Attributes["tempo"];
                                int t;
                                if (tempoAttribute != null && int.TryParse(tempoAttribute.InnerText, NumberStyles.Integer, CultureInfo.InvariantCulture,out t)){
                                    measure.Direction.Sound.Tempo = t;
                                }
                            }
                            var typeNode = directionNode.SelectSingleNode("direction-type");
                            if(typeNode != null){
                                measure.Direction.Type = new DirectionType();
                                var MetronomeNode = typeNode.SelectSingleNode("metronome");
                                if(MetronomeNode != null){
                                    measure.Direction.Type.MetronomeMark = new Metronome();
                                    var beatNode = MetronomeNode.SelectSingleNode("beat-unit");
                                    if(beatNode != null){
                                        measure.Direction.Type.MetronomeMark.BeatUnit = beatNode.InnerText;
                                    }

                                    var minuteNode = MetronomeNode.SelectSingleNode("per-minute");
                                    if(minuteNode != null){
                                        measure.Direction.Type.MetronomeMark.PerMinute = float.Parse(minuteNode.InnerText);
                                    }
                                }
                            }
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
                        song.Measures.Add(measure);
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

    private static Clef GetClef(XmlNode attributesNode)
		{
			var clef = new Clef();

			var clefNode = attributesNode.SelectSingleNode("clef");

			if (clefNode != null)
			{
				var lineNode = clefNode.SelectSingleNode("line");
				if (lineNode != null)
					clef.Line = Convert.ToInt32(lineNode.InnerText);

				var signNode = clefNode.SelectSingleNode("sign");
				if (signNode != null)
					clef.Sign = signNode.InnerText;
			}
			return clef;
		}

		private static Time GetTime(XmlNode attributesNode)
		{
			var time = new Time();

			var timeNode = attributesNode.SelectSingleNode("time");
			if (timeNode != null)
			{
				var beatsNode = timeNode.SelectSingleNode("beats");

				if (beatsNode != null)
					time.Beats = Convert.ToInt32(beatsNode.InnerText);

				var beatTypeNode = timeNode.SelectSingleNode("beat-type");

				if (beatTypeNode != null)
					time.Mode = beatTypeNode.InnerText;

				var symbol = TimeSymbol.Normal;

				if (timeNode.Attributes != null)
				{
					var symbolAttribute = timeNode.Attributes["symbol"];

					if (symbolAttribute != null)
					{
						switch (symbolAttribute.InnerText)
						{
							case "common":
								symbol = TimeSymbol.Common;
								break;
							case "cut":
								symbol = TimeSymbol.Cut;
								break;
							case "single-number":
								symbol = TimeSymbol.SingleNumber;
								break;
						}
					}
				}

				time.Symbol = symbol;
			}
			return time;
		}
}
