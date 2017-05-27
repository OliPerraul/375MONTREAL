using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //associated player number
    public int number { get; set; }

    

    // Use this for initialization
    void Start ()
    {
        InitCursors();

        InitCharacters();


	}


    void InitCharacters()
    {
        GameObject prefab = Resources.Load("Character") as GameObject;

        GameObject charObj = Instantiate(prefab, Vector3.zero, Quaternion.Euler(0, 0, 0));

       charObj.name = "Character" + number.ToString();
       
     
        Character character = charObj.GetComponent<Character>(); //access main component

        character.number = number;

        character.transform.position = FindPosition();
       
      }



    void InitCursors()
    {
        GameObject prefab = Resources.Load("Cursor") as GameObject;

        GameObject cursObj = Instantiate(prefab, Vector3.zero, Quaternion.Euler(0, 0, 0));

        cursObj.name = "Cursor" + number.ToString();


        Cursor cursor = cursObj.GetComponent<Cursor>(); //access main component

        cursor.number = number;

    }



    Vector3 FindPosition()
    {
        int x_pos;
        int y_pos;
        Camera cam = Camera.main;
        Vector3 pos = Vector3.zero;
       

        switch (number)
        {
            case 1://top left

                x_pos = (cam.pixelWidth/5);
                y_pos = (cam.pixelHeight /4);

                pos = cam.ScreenToWorldPoint(new Vector3(x_pos, y_pos, 0));


           break;

            case 2: //top right

                x_pos = 4*(cam.pixelWidth / 5);
                y_pos = (cam.pixelHeight / 4);

                pos = cam.ScreenToWorldPoint(new Vector3(x_pos, y_pos, 0));
                pos.z = 0;//kill z to appear on plane

                break;



            case 3://bottom left 

                x_pos = (cam.pixelWidth / 5);
                y_pos = 3*(cam.pixelHeight / 4);

                pos = cam.ScreenToWorldPoint(new Vector3(x_pos, y_pos, 0));


                break;

            case 4:

                x_pos = 4 * (cam.pixelWidth / 5);
                y_pos = 3 * (cam.pixelHeight / 4);

                pos = cam.ScreenToWorldPoint(new Vector3(x_pos, y_pos, 0));


            break;

                
        }

        return pos;

    }


	
	// Update is called once per frame
	void Update () {
		
	}
}
