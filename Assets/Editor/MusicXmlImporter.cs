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
                StreamReader reader = new StreamReader(item);
                string data = reader.ReadToEnd();
                Song score = MusicXmlParser.ParseScore(data);
                Pipe p = new Pipe();
                p.Step = 'C';
                p.Alter = 0;
                p.Octave = 4;
                p.BeatType = "quarter";
                p.Type = "quarter";
                p.Tempo = 80;
                pipes.Add(p);
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(GetAssetPath(item));
                bf.Serialize(file, pipes);
                file.Close();
            }
        }
    }
}
