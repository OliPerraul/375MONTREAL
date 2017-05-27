using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clothe : MonoBehaviour
{
    //reference of all the themes
    public static List<string> themes_lookup;


    [SerializeField]
    public bool is_held;
    public bool is_worn { get; set; }

    #region Movement
    public Vector3 curr_speed;
    public Vector3 target_speed;
    [SerializeField]
    private float deccel_rate = 0.3f;
    #endregion


    public enum CLOTHE_TYPE
    {
        HAT, SHIRT, PANTS

    }

    //clothes determinant

    public CLOTHE_TYPE clothe_type { get; set; }

    /// <summary>
    /// Job or style
    /// </summary>
    private string theme { get; set; }

    
    //TODO REPLACE BY JOB OR STYLE
    public string years { get; set; } //year of the shirt
    

    private SpriteRenderer sprite_rend;
    private Sprite sprite;

    

    // Use this for initialization
    void Start ()
    {
        //initialize reference list
       themes_lookup = new List<string> { "Industrialisation", "Prohibition", "Pin-up", "Hippie", "Disco", "Glam", "Hip-Hop", "Coureur_des_bois", "Gendarme", "Bucheron", "Clerge", "Cirque", "Construction", "Hipster", "Hipster", "Superhero", "Ballerine", "Dracula", "Mozart", "Legionnaire_Romain", "Pharaon"};
        
        is_worn = false;
        is_held = false;
        
        sprite_rend = GetComponent<SpriteRenderer>();//access sprite renderer

        DetermineSprite();
        
	}


    void DetermineSprite()  ///TODO REPLACE YEAR BY THEME
    {
        switch (clothe_type) //depending on type choose from number of options
        {
            case CLOTHE_TYPE.HAT:
                sprite = Resources.Load<Sprite>("Sprites/Clothes/Hats/spr_hat" + years);

                break;

            case CLOTHE_TYPE.SHIRT:
                sprite = Resources.Load<Sprite>("Sprites/Clothes/Shirts/spr_shirt" + years);

                break;

            case CLOTHE_TYPE.PANTS:
                sprite = Resources.Load<Sprite>("Sprites/Clothes/Pants/spr_pants" + years);
                break;

          
        }

       // Debug.Log(sprite);

        sprite_rend.sprite = sprite;

    }


    // Update is called once per frame
    void Update ()
    {
        
        ///////IF WORN
        if (is_worn)
        {
            //pulse only if correct player hovers over it
        }
        else//if not worn
        {
             Movement();//apply movement
        }

        

    }

    void Movement()
    {
        if (is_held)
            target_speed = curr_speed;
        else
            target_speed = Vector3.zero;

        curr_speed = Vector3.Lerp(curr_speed, target_speed, deccel_rate);
        transform.position += curr_speed;


        BounceOnScreenEdge();


    }


    void BounceOnScreenEdge()
    {

        Camera cam = Camera.main;
        Vector3 pos = Vector3.zero;

        
        Vector3 pos_bottom_left = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 pos_top_right = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, 0));

        if ((transform.position.x > pos_top_right.x) || (transform.position.x < pos_bottom_left.x))
        {
            is_held = false; //prevent cursor dragging item outside

            curr_speed.x = -curr_speed.x;
            target_speed.x = -target_speed.x;

        }
        
        if ((transform.position.y > pos_top_right.y) || (transform.position.y < pos_bottom_left.y))
        {

            is_held = false; //prevent cursor dragging item outside

            curr_speed.y = -curr_speed.y;
            target_speed.y = -target_speed.y;

        }

        

    }

    // Update is called once per frame
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Cursor")
        {
            is_held = false; //not helf if cursor not over

        }


    }




    /// <summary>
    /// Creates an object based on type and time period
    /// </summary>
    /// <param name="type"></param>
    /// <param string="year"></param>
    /// <returns></returns>
    public static GameObject Create(CLOTHE_TYPE type, string theme)//custom init
    {
        //find container
        GameObject scriptedEntityController = GameObject.Find("ScriptedEntityController");

        //begin creation
        GameObject prefab = Resources.Load("Clothe") as GameObject;
        GameObject newObject = Instantiate(prefab, Vector3.zero, Quaternion.Euler(0, 0, 0));

        newObject.name = type.ToString(); //assign correct name
        
        Clothe clothe = newObject.GetComponent<Clothe>(); //access main component


        clothe.clothe_type = type; //set type
        clothe.theme = theme; //set theme
                

        return newObject;

    }

}
