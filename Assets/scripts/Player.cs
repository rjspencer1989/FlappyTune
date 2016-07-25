using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	private Rigidbody2D rb;
	public Text scoreText;
	int score = 0;
	public float speed;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		setScoreText();

	}

	void FixedUpdate () {
		bool inputVal = false;
        if(Input.touchSupported){
            if(Input.touchCount > 0){
                inputVal = true;
            }
        } else{
            inputVal = Input.GetAxis("Jump") == 1 ? true: false;
        }
		
		if(inputVal) {
			rb.AddForce (Vector2.up * speed);
			inputVal = false;
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.CompareTag("edge") || other.gameObject.CompareTag("pipe")){
			this.gameObject.SetActive(false);
			SceneManager.LoadScene ("GameOver");
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag("score")){
			score += 1;
			setScoreText();
            other.GetComponent<AudioSource>().Play();
        }
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.CompareTag("score")){
			other.GetComponent<AudioSource>().Stop();
        }
	}

	void setScoreText(){
		scoreText.text = "Score: " + score.ToString();
	}
}
