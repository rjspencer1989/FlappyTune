using UnityEngine;

public class DestroyPipes: MonoBehaviour{
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("pipe") || other.CompareTag("score")){
            Destroy(other.gameObject);
        }
    }
}