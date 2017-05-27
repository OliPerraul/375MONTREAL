using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    //character associated with given cursor
    [SerializeField]
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

  
    private bool A_pressed;
    private bool A_held;
    private bool A_released;

    private Clothe clothe_held;



    // Use this for initialization
    void Start ()
    {
        	
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
        target_x_speed = Input.GetAxis("Horizontal");
        target_y_speed = Input.GetAxis("Vertical");

        A_pressed = Input.GetKeyDown("joystick button 0");
        A_held = Input.GetKey("joystick button 0");
        A_released = Input.GetKeyUp("joystick button 0");



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
            Clothe clothe = other.GetComponent<Clothe>();
            if ((A_held) && !clothe.is_worn)
            {
                clothe.is_held = true;
                clothe_held = clothe;

                clothe.curr_speed = speed;
            }
           
        }
        
    }

  
}
