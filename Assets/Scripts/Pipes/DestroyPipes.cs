using UnityEngine;

public class DestroyPipes: MonoBehaviour{
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("pipe")){
            print(other.gameObject.transform.parent);
            Destroy(other.gameObject.transform.parent.gameObject);
        }
    }
}