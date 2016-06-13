using UnityEngine;
using System.Collections;

public class DestroyPipes: MonoBehaviour{
    void OnTriggerEnter2D(Collider2D other){
        print(other);
        if(other.CompareTag("pipebox")){
            Destroy(other.gameObject);
        }
    }
}