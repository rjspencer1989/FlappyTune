using System;
using UnityEditor;
using MusicParser;
using UnityEngine;
using System.Text;
using System.IO;

public class MusicXmlImporter : AssetPostprocessor{
    static string extension = ".xml";

    public static bool HasExtension(string asset){
        return asset.EndsWith(extension, StringComparison.OrdinalIgnoreCase);
    }

    private static string GetAssetPath(string asset){
        StringBuilder builder = new StringBuilder();
        builder.Append("Assets/Songs/");
        builder.Append(asset.Substring(0, asset.Length - extension.Length));
        builder.Append(".asset");
        return builder.ToString();
    }

    public static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths){
        foreach (string item in importedAssets){
            if(HasExtension(item)){
                StreamReader reader = new StreamReader(item);
                Song score = MusicXmlParser.ParseScore(reader.ReadToEnd());
                SongPipes songPipes = ScriptableObject.CreateInstance("SongPipes") as SongPipes;
                Pipe p = ScriptableObject.CreateInstance("Pipe") as Pipe;
                p.Step = 'C';
                p.Alter = 0;
                p.Octave = 4;
                p.BeatType = "quarter";
                p.Type = "quarter";
                p.Tempo = 80;
                songPipes.pipes.Add(p);
                AssetDatabase.CreateAsset(songPipes, GetAssetPath(item));
            }
        }
    }
}
