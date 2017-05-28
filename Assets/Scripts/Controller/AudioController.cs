using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
	//a drag dans lispector
/*	public Character character;
	public Cursor cursor;
	public Clothe clothe;
	public MenuButton button;
	public Player player;*/

	//musiques
	public AudioSource BGM;//La chose qui emet de la musique





	//public AudioClip currentBGM; // la musique
	public AudioClip [] bgm; // le array de musique

	//FX
/*	public AudioClip [] fx; // la chose qui emet de la musique
	public AudioClip currentFX; // le FX
	public AudioSource FX;*/

	//variable intuiles
	//private int cpt=0;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		/*	if (Input.GetKeyDown("joystick button 0")){
			PlayClip (1);		}*/
	}

	public void PlayClip(int clip){
		BGM.clip = bgm[clip];
		BGM.Play ();

	}
}
