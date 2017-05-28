using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score_UI : MonoBehaviour
{
    [SerializeField]
    TextMesh textmesh;

    [SerializeField]
    private int number;
    

	// Use this for initialization
	void Start ()
    {
      
        
    }

    // Update is called once per frame
    void Update ()
    {
        textmesh = GetComponent<TextMesh>();

        textmesh.color = PlayersController.player_colors[number - 1];
    }
}
