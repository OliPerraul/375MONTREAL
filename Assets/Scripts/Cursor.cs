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

      private Sprite sprite;

    // Use this for initialization
    void Start ()
    {
        //Choose sprite
        ChooseSprite();

        if (SceneManager.GetActiveScene().name == "gameplay")
        {
            //sprite_rend = GetComponent<SpriteRenderer>();
           // sprite_rend.color = PlayersController.player_colors[number - 1];

           // get character reference
             character = PlayersController.players[number - 1].character;
        }
        
       
     }

    /// <summary>
    /// Choose the correct player sprite
    /// </summary>
    void ChooseSprite()
    {
        sprite_rend = GetComponent<SpriteRenderer>();

        sprite = Resources.Load<Sprite>("Sprites/Cursors/spr_cursor" + number);
        sprite_rend.sprite = sprite;

    }


    // Update is called once per frame
    void Update ()
    {
        PlayerInputs();
        
        //update cursor pos
        //transform.position += speed;


        //make sure always a sprite assigned

        if (sprite_rend.sprite == null)
        {
            sprite = Resources.Load<Sprite>("Sprites/Cursors/spr_cursor1");
            sprite_rend.sprite = sprite;
        }


        ///RESET IF NEEDED
        Camera cam = Camera.main;

        int right_screen = cam.pixelWidth;
        int up_screen = cam.pixelHeight;


        Vector3 top_right = cam.ScreenToWorldPoint(new Vector3(right_screen, up_screen, 0));
        Vector3 bottom_left = cam.ScreenToWorldPoint(new Vector3(0,0, 0));

        if (transform.position.x < bottom_left.x || transform.position.x > top_right.x)
            ResetCursor();
        
    }

    void LateUpdate()
    {
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


        //speed check
        if (speed.magnitude > max_speed)
        {
            speed.Normalize();
            speed *= max_speed;
        }
        
                        
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
        //Debug.Log("Stay on Trigger: " + other.name);

        if (other.gameObject.tag == "Clothe")
        {
            //Check for alpha testing 
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

        //cal bounce on edge method
        BounceOnScreenEdge(other);

    }
    

    void OnTriggerEnter2D(Collider2D other)
    {

        BounceOnScreenEdge(other);

        //Debug.Log("Entering Trigger: " + other.name);

        //cal bounce on edge method
        //BounceOnScreenEdge(other);

    }

    void OnTriggerExit2D(Collider2D trigger)
    {
   
        if (trigger.gameObject.tag == "Clothe")
        {

            //Debug.Log ("Exit is Holding"+isHolding);

            Clothe clothe = trigger.GetComponent<Clothe>();
            clothe.is_held = false;
            canHold = true;


        }
        else
        {
            BounceOnScreenEdge(trigger);
        }
    }


    /// <summary>
    /// Helper method (Bounce on the edge of the screen)
    /// </summary>
    void BounceOnScreenEdge(Collider2D other)
    {
        string tag = other.gameObject.tag;

        if (tag == "leftboundary" || tag == "rightboundary")
        {
            speed.x = -speed.x*2;
            //update cursor pos
            transform.position += speed;
        }


        if (tag == "topboundary" || tag == "bottomboundary")
        {
            speed.y = -speed.y*2;
            //update cursor pos
            transform.position += speed;
        }

    }


    void ResetCursor()
    {

        Camera cam = Camera.main;

        int x_pos = cam.pixelWidth/2;
        int y_pos = cam.pixelHeight/2;

        Vector3 pos = cam.ScreenToWorldPoint(new Vector3(x_pos, y_pos, 0));
        pos.z = 0;//kill z value
      

        transform.position = pos;

    }





    // Update is called once per frame
    /* void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.tag == "Clothe")
        {

            //Debug.Log ("Exit is Holding"+isHolding);

            Clothe clothe = other.GetComponent<Clothe>();
            clothe.is_held = false;
            canHold = true;


        }

    } */


}
