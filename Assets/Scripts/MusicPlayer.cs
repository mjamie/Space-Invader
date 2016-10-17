using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

	static MusicPlayer insatance = null;
	// Use this for initialization
	void Awake () {
		if(insatance != null){
			Destroy (gameObject);
		} else {
		insatance = this;
		GameObject.DontDestroyOnLoad (gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
