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
    private float move_rate = .5f;

    [SerializeField]
    private float accel_rate = .5f;

    //inputs

    private float curr_x_speed = 0f;
    private float curr_y_speed = 0f;
    
    private float target_x_speed = 0f;
    private float target_y_speed = 0f;

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
		target_x_speed = Input.GetAxis(rightStick);
		target_y_speed = Input.GetAxis(leftStick);

		A_pressed = Input.GetButtonDown(jumpButton);
		A_held = Input.GetButton(jumpButton);
		A_released = Input.GetButtonUp(jumpButton);



        //approximate to target
        curr_x_speed = Mathf.Lerp(curr_x_speed, target_x_speed, accel_rate);
        curr_y_speed = Mathf.Lerp(curr_y_speed, target_y_speed, accel_rate);

        speed = new Vector3(curr_x_speed, curr_y_speed, 0f) * move_rate;

        
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
                clothe.is_held = true;
                clothe_held = clothe;
				isHolding = true;

                clothe.curr_speed = speed;
			}
           
        }
                
    }


    // Update is called once per frame
    void OnTriggerExit2D(Collider2D other)
    {
		
        if (other.gameObject.tag == "Clothe")
        {
			
			//Debug.Log ("Exit is Holding"+isHolding);

            Clothe clothe = other.GetComponent<Clothe>();
                  clothe.is_held = false;
			canHold = true;

           
        }

    }


}
