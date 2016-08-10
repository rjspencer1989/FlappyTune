using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEditor;
using MusicParser;
using System.Linq;

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
        foreach (string item in importedAssets){
            if(HasExtension(item)){
                SongData songData = new SongData();
                float tempo = 0;
                string bt = "";
                StringBuilder builder = new StringBuilder();
                float fc = 0;
                StreamReader reader = new StreamReader(item);
                EditorUtility.DisplayProgressBar("working", "loading stream", 0.0f);
                while(!reader.EndOfStream){
                    builder.Append(reader.ReadLine());
                    fc++;
                    if(fc % 100 == 0){
                        EditorUtility.DisplayProgressBar("working", "loading stream", reader.BaseStream.Position / (float)reader.BaseStream.Length);
                    }
                }
                EditorUtility.ClearProgressBar();
                Song score = MusicXmlParser.ParseScore(builder.ToString());
                List<Note> notes = new List<Note>();
                foreach (Measure measure in score.Measures){
                    foreach(MeasureElement element in measure.MeasureElements){
                        if(element.Type == MeasureElementType.Note){
                            notes.Add(element.Element as Note);
                            if(measure.Direction.Type.MetronomeMark.PerMinute > 0){
                                    bt = measure.Direction.Type.MetronomeMark.BeatUnit;
                                    tempo = measure.Direction.Type.MetronomeMark.PerMinute;
                                }
                                songData.BeatType = bt;
                                songData.Tempo = tempo;
                        }
                    }
                }

                foreach (var note in notes){
                    if(!note.IsRest){
                        Pipe p = new Pipe();
                        p.Step = note.Pitch.Step;
                        p.Alter = note.Pitch.Alter;
                        p.Octave = note.Pitch.Octave;
                        p.Type = note.Type;
                        songData.Pipes.Add(p);
                    } else{

                    }
                }
                
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(GetAssetPath(item));
                bf.Serialize(file, songData);
                file.Close();
                AssetDatabase.SaveAssets();
            }
        }
    }
}
