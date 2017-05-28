using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cursor : MonoBehaviour
{
    //associated player number
    public int number { get; set; }
        
    
    //character associated with given cursor
    private Character character;

    [SerializeField]
    private float max_speed = 1.5f;


    //inputs

    private Vector3 speed;

	public string rightStick = "Horizontal_P1";
	public string leftStick = "Vertical_P1";
	public string jumpButton = "Jump_P1";
  
    private bool A_pressed;
    private bool A_held;
    private bool A_released;

	public bool isHolding = false;
	public bool canHold = true;

    private Clothe clothe_held;

    private SpriteRenderer sprite_rend;

    // Use this for initialization
    void Start ()
    {
                

        if (SceneManager.GetActiveScene().name == "gameplay")
        {
            sprite_rend = GetComponent<SpriteRenderer>();
            sprite_rend.color = PlayersController.player_colors[number - 1];

           // get character reference
             character = PlayersController.players[number - 1].character;
        }

       
        
     }
	
	// Update is called once per frame
	void Update ()
    {
        PlayerInputs();


        //update cursor pos
        transform.position += speed;
		   
    }
    
     

    void PlayerInputs()
    {
        float x_axis = Input.GetAxis(rightStick);
		float y_axis = Input.GetAxis(leftStick);

       
        //if held left: neg max spd
        float speed_x = 0;
        if (x_axis < 0)
             speed_x = -Mathf.Lerp(max_speed, 0, x_axis);
         else
             speed_x = Mathf.Lerp(0, max_speed, x_axis);
        
        
        //if held down: neg max spd
        float speed_y = 0;
        if (y_axis < 0)
             speed_y = -Mathf.Lerp(max_speed, 0, y_axis);
         else
             speed_y = Mathf.Lerp(0, max_speed, y_axis);

        
        //set correct speed
        speed.x = speed_x;
        speed.y = speed_y;

                

        A_pressed = Input.GetButtonDown(jumpButton);
		A_held = Input.GetButton(jumpButton);
		A_released = Input.GetButtonUp(jumpButton);
        


        //adding clothes on character or let go clothe
        if (A_released)
        {
            if (clothe_held != null)//if holding a clothe
            {
                if (character.receiving == true)
                    character.AddClothe(clothe_held.GetComponent<Clothe>());

                clothe_held.is_held = false;
                clothe_held = null;
            }
        }


    }


    // Update is called once per frame
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Clothe")
        {
			
			///Debug.Log ("Enter is Holding"+isHolding);

            Clothe clothe = other.GetComponent<Clothe>();
			if ((A_held) && (!clothe.is_worn))
            {
                clothe_held = clothe;

                if((clothe_held == clothe)||clothe_held == null)
                clothe.is_held = true;
                
				isHolding = true;

                clothe.curr_speed = speed;
			}
           
        }
                
    }


   // // Update is called once per frame
   // void OnTriggerExit2D(Collider2D other)
   // {
		
   //     if (other.gameObject.tag == "Clothe")
   //     {
			
			////Debug.Log ("Exit is Holding"+isHolding);

   //         Clothe clothe = other.GetComponent<Clothe>();
   //               clothe.is_held = false;
			//canHold = true;

           
   //     }

   // }


}
