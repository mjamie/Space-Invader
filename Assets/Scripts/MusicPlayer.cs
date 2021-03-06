﻿using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

	static MusicPlayer insatance = null;

	public AudioClip startClip;
	public AudioClip gameClip;
	public AudioClip endClip;

	private AudioSource music;
	void Awake () {
		if(insatance != null && insatance != this){
			Destroy (gameObject);
		} else {
		insatance = this;
		GameObject.DontDestroyOnLoad (gameObject);
		music = GetComponent <AudioSource>();
		music.clip = startClip;
		music.loop = true;
		music.Play ();
		}
	}

	void OnLevelWasLoaded (int level) {
		Debug.Log ("Musicplayer: loaded level: "+ level);
		music.Stop ();

		if(level == 0){
			music.clip = startClip;
		}
		if(level == 1){
			music.clip = gameClip;
		}
		if(level == 2){
			music.clip = endClip;
		}
		music.Play ();
	}
}
