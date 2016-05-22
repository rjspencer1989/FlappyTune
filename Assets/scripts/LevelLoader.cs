using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

	public void ClickButton(int level) {
		SceneManager.LoadScene (level);
	}
}
