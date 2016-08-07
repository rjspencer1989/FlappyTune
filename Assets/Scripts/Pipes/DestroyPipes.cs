using UnityEngine;

public class DestroyPipes: MonoBehaviour{
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("pipe")){
            Destroy(other.gameObject.transform.parent.gameObject);
        }
    }
}