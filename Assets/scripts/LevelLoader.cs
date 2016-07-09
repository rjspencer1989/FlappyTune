using UnityEngine;

public class LevelLoader : MonoBehaviour {

	public void ClickButton(string name) {
        Scenes.Load("MainScene", "songName", name);
    }
}
