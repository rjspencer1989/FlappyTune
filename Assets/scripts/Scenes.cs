using System.Collections.Generic;
using UnityEngine.SceneManagement;

public static class Scenes{
    private static Dictionary<string, string> parameters;

    public static void Load(string name, string key, string val){

        Scenes.parameters = new Dictionary<string, string>();
        Scenes.parameters.Add(key, val);
        SceneManager.LoadScene(name);
    }

    static string DEFAULT_SONG = "star";

    public static Dictionary<string, string> getSceneParameters(){
        return Scenes.parameters;
    }

    public static string getParameter(string key){
        if(parameters == null){
            return DEFAULT_SONG;
        } else if(!parameters.ContainsKey(key)){
            return DEFAULT_SONG;
        }
        return parameters[key];
    }

    public static void setParam(string paramKey, string paramValue) {
        if (parameters == null){
            Scenes.parameters = new Dictionary<string, string>();
        }
        Scenes.parameters.Add(paramKey, paramValue);
    }
}