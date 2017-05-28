using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Winner_UI : MonoBehaviour
{


    [SerializeField]
    float time_end = 120;

    [SerializeField]
    private TextMesh winner;



    // Use this for initialization
    void Start ()
    {
        winner.text = "Gagnant/Winner: Joueur" + Global.winner.ToString();
	}
	
	// Update is called once per frame
	void Update ()
    {

        time_end -= Time.deltaTime;

        if(time_end <= 0)
            SceneManager.LoadScene("main_menu", LoadSceneMode.Single);




    }
}
