using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

	//musiques
	public AudioSource BGM;//La chose qui emet de la musique

	//public AudioClip currentBGM; // la musique
	public AudioClip [] bgm; // le array de musique

	void Start(){
		if(!BGM.playOnAwake) 
		{
			BGM.clip = bgm [Random.Range (0, bgm.Length)];
			BGM.Play();
			//BGM.loop = true;
		}
	}
	void Update ()
	{
		if (!BGM.isPlaying)
		{
			BGM.clip = bgm[Random.Range(0, bgm.Length)];
			BGM.Play();
			PlayNextSong ();
		}
	}

	public void PlayClip(int clip){
		BGM.Stop ();
		BGM.clip = bgm[clip];
		BGM.Play ();

	}
	void PlayNextSong(){
		BGM.clip = bgm[Random.Range(0,bgm.Length)];
		BGM.Play();
		Invoke("PlayNextSong", BGM.clip.length);
	}
}
