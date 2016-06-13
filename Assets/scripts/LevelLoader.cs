using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

	public void ClickButton(int level) {
		SceneManager.LoadScene (level);
	}
}
