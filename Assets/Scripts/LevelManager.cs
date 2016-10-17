using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public void LoadLevel (string name) {
		Debug.Log("Level load requested " + name);
		Application.LoadLevel(name);
	}

	public void QuitRequest () {
	 	Debug.Log ("Program is exiting");
	 	Application.Quit ();
	}

	public void backToStart (string name)
	{
	Application.LoadLevel (name);
	}

	public void LoadNextLevel(){
		Application.LoadLevel(Application.loadedLevel + 1);
	}

	public void brickDestroyed () {
			LoadNextLevel ();
		
	}
}
