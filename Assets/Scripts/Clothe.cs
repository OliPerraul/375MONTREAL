using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clothe : MonoBehaviour
{
    [SerializeField]
    public bool is_held;
    public bool is_worn { get; set; }

    #region Movement
    public Vector3 curr_speed;
    public Vector3 target_speed;
    [SerializeField]
    private float deccel_rate = 0.3f;
    #endregion

    private BoxCollider2D boxcol;

    public Vector3 start_hitbox_offset = new Vector3(0,-0.1f);
    public Vector3 start_hitbox_size = new Vector3(5.5f, 8.5f);

    public Vector3 alternate_hitbox_offset;
    public Vector3 alternate_hitbox_size;

    public enum CLOTHE_TYPE
    {
        HAT, SHIRT, PANTS

    }

    //clothes determinant

    public CLOTHE_TYPE clothe_type { get; set; }

    /// <summary>
    /// Job or style
    /// </summary>
    public string theme { get; set; }


    private SpriteRenderer sprite_rend;
    private Sprite sprite;

    

    // Use this for initialization
    void Start ()
    {
         boxcol = GetComponent<BoxCollider2D>(); //box collider

        is_worn = false;
        is_held = false;
        
        sprite_rend = GetComponent<SpriteRenderer>();//access sprite renderer

        DetermineHitbox();
        DetermineSprite();
        
	}

    // Use this for initialization
    void DetermineHitbox()
    {
                //save start hitbox preference
        

        switch (clothe_type)
        {
            case CLOTHE_TYPE.HAT:
                alternate_hitbox_offset = new Vector2(-.5f,1.4f);
                alternate_hitbox_size = new Vector2(1.5f, 1.5f);

                break;

            case CLOTHE_TYPE.SHIRT:
                alternate_hitbox_offset = new Vector2(-.3f, -1.8f);
                alternate_hitbox_size = new Vector2(1.5f, 1.5f);

                break;

            case CLOTHE_TYPE.PANTS:
                alternate_hitbox_offset = new Vector2(-.5f, -3f);
                alternate_hitbox_size = new Vector2(1.5f, 1.5f);

                break;


        }

        boxcol.offset = alternate_hitbox_offset;
        boxcol.size = alternate_hitbox_size;

    }


    void DetermineSprite()  ///TODO REPLACE YEAR BY THEME
    {
        

        switch (clothe_type) //depending on type choose from number of options
        {
            case CLOTHE_TYPE.HAT:
                sprite = Resources.Load<Sprite>("Sprites/Clothes/Hats/spr_hat" +"("+ theme+")");

                break;

            case CLOTHE_TYPE.SHIRT:
                sprite = Resources.Load<Sprite>("Sprites/Clothes/Shirts/spr_shirt" + "(" + theme + ")");

                break;

            case CLOTHE_TYPE.PANTS:
                sprite = Resources.Load<Sprite>("Sprites/Clothes/Pants/spr_pants" + "(" + theme + ")");
                break;

          
        }

       // Debug.Log(sprite);

        sprite_rend.sprite = sprite;

    }


    // Update is called once per frame
    void Update ()
    {
       // transform.localScale.Set(1.5f, 1.5f, 1.5f);

        ///////IF WORN
        if (is_worn)
        {
            

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
        
    }


    void OnTriggerEnter2D(Collider2D other)
    {

      //  Debug.Log("colliding");

        //cal bounce on edge method
        BounceOnScreenEdge(other);

    }


    /// <summary>
    /// Helper method (Bounce on the edge of the screen)
    /// </summary>
    void BounceOnScreenEdge(Collider2D other)
    {
        string tag = other.gameObject.tag;


        if (tag == "leftboundary" || tag == "rightboundary")
        {
            curr_speed.x = -curr_speed.x;
            target_speed.x = -target_speed.x;

            is_held = false;
        }


        if (tag == "topboundary" || tag == "bottomboundary")
        {
            curr_speed.y = -curr_speed.y;
            target_speed.y = -target_speed.y;

            is_held = false;
        }

    }




    /// <summary>
    /// Creates an object based on type and time period
    /// </summary>
    /// <param name="type"></param>

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
