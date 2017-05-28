using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

	//musiques
	public AudioSource BGM;//La chose qui emet de la musique

	//public AudioClip currentBGM; // la musique
	public AudioClip [] bgm; // le array de musique


	public void PlayClip(int clip){
		BGM.Stop ();
		BGM.clip = bgm[clip];
		BGM.Play ();

	}
}
