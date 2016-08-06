using System;
using UnityEditor;
using MusicParser;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public class MusicXmlImporter : AssetPostprocessor{
    static string extension = ".xml";

    public static bool HasExtension(string asset){
        return asset.EndsWith(extension, StringComparison.OrdinalIgnoreCase);
    }

    private static string GetAssetPath(string asset){
        StringBuilder builder = new StringBuilder();
        builder.Append("Assets/Resources/Songs/");
        int start = asset.LastIndexOf("/") + 1;
        builder.Append(asset.Substring(start, (asset.Length - extension.Length - start)));
        builder.Append(".bytes");
        return builder.ToString();
    }

    public static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths){
        List<Pipe> pipes = new List<Pipe>();
        foreach (string item in importedAssets){
            if(HasExtension(item)){
                float tempo = 0;
                string bt = "";
                StreamReader reader = new StreamReader(item);
                string data = reader.ReadToEnd();
                Song score = MusicXmlParser.ParseScore(data);
                foreach (Measure measure in score.Measures){
                    foreach(MeasureElement element in measure.MeasureElements){
                        if(element.Type == MeasureElementType.Note){
                            Note n = element.Element as Note;
                            if(!n.IsRest){
                                Pipe p = new Pipe();
                                p.Step = n.Pitch.Step;
                                p.Alter = n.Pitch.Alter;
                                p.Octave = n.Pitch.Octave;
                                p.Type = n.Type;
                                if(measure.Direction.Type.MetronomeMark.PerMinute > 0){
                                    bt = measure.Direction.Type.MetronomeMark.BeatUnit;
                                    tempo = measure.Direction.Type.MetronomeMark.PerMinute;
                                }
                                p.BeatType = bt;
                                p.Tempo = tempo;
                                pipes.Add(p);
                            }
                        }
                    }
                }
                
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(GetAssetPath(item));
                bf.Serialize(file, pipes);
                file.Close();
            }
        }
    }
}
