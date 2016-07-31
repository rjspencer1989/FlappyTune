using System.Collections.Generic;
using UnityEngine;
public class SongPipes : ScriptableObject{
    List<Pipe> pipes;

    void OnEnable(){
        if(pipes == null){
            pipes = new List<Pipe>();
        }
    }
}
