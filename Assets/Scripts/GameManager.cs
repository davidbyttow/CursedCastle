using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public static GameManager global { get; private set; }

	void Awake() {
		if (global!= null) {
			throw new System.Exception("More than one GameManager present in scene");
		}
		global= this;
	}

	void Start() {

	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Tab)) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
	
	public void ExitLevel() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
