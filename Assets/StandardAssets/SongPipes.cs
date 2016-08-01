using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class SongPipes : ScriptableObject{
    public List<Pipe> pipes { get; set; }

    void OnEnable(){
        if(pipes == null){
            pipes = new List<Pipe>();
        }
    }
}
