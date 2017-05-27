using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //associated player number
    public int number { get; set; }

    public Cursor cursor { get; set; }
    public Character character { get; set; }


    // Use this for initialization
    void Start ()
    {
        InitCharacters(); //init character then cursor

        InitCursors();

    }


    void InitCharacters()
    {
        GameObject prefab = Resources.Load("Character") as GameObject;

        GameObject charObj = Instantiate(prefab, Vector3.zero, Quaternion.Euler(0, 0, 0));

       charObj.name = "Character" + number.ToString();
       
     
        Character _character = charObj.GetComponent<Character>(); //access main component

        this.character = _character; //set given player

        character.number = number;

        character.transform.position = FindPosition();
       
      }



    void InitCursors()
    {
		//Switch qui instantiate un curseur pour tout le monde
		switch(number){
		case 1:
			GameObject prefab = Resources.Load ("Cursor") as GameObject;

			GameObject cursObj = Instantiate (prefab, Vector3.zero, Quaternion.Euler (0, 0, 0));

			cursObj.name = "Cursor" + number.ToString ();

			Cursor _cursor = cursObj.GetComponent<Cursor> (); //access main component

			_cursor.rightStick = "Horizontal_P1";
			_cursor.leftStick = "Vertical_P1";
			_cursor.jumpButton = "Jump_P1";

			this.cursor = _cursor; //set given player

			cursor.number = number;
			break;
		case 2:
			GameObject prefab1 = Resources.Load("Cursor") as GameObject;

			GameObject cursObj1 = Instantiate(prefab1, Vector3.zero, Quaternion.Euler(0, 0, 0));

			cursObj1.name = "Cursor" + number.ToString();

			Cursor _cursor1 = cursObj1.GetComponent<Cursor>(); //access main component

			_cursor1.rightStick = "Horizontal_P2";
			_cursor1.leftStick = "Vertical_P2";
			_cursor1.jumpButton = "Jump_P2";

			this.cursor = _cursor1; //set given player

			cursor.number = number;
			break;
		case 3: 
			GameObject prefab2 = Resources.Load("Cursor") as GameObject;

			GameObject cursObj2 = Instantiate(prefab2, Vector3.zero, Quaternion.Euler(0, 0, 0));

			cursObj2.name = "Cursor" + number.ToString();

			Cursor _cursor2 = cursObj2.GetComponent<Cursor>(); //access main component

			_cursor2.rightStick = "Horizontal_P3";
			_cursor2.leftStick = "Vertical_P3";
			_cursor2.jumpButton = "Jump_P3";

			this.cursor = _cursor2; //set given player

			cursor.number = number;
			break;
		case 4:
			GameObject prefab3 = Resources.Load("Cursor") as GameObject;

			GameObject cursObj3 = Instantiate(prefab3, Vector3.zero, Quaternion.Euler(0, 0, 0));

			cursObj3.name = "Cursor" + number.ToString();

			Cursor _cursor3 = cursObj3.GetComponent<Cursor>(); //access main component

			_cursor3.rightStick = "Horizontal_P4";
			_cursor3.leftStick = "Vertical_P4";
			_cursor3.jumpButton = "Jump_P4";

			this.cursor = _cursor3; //set given player

			cursor.number = number;
			break;
		}
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

        pos.z = 0;//kill z to appear on plane

        return pos;

       

    }


	
	// Update is called once per frame
	void Update () {
		
	}
}
