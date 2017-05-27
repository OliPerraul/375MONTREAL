using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clothe : MonoBehaviour
{

    public bool is_held { get; set; }
    public bool is_worn { get; set; }

    #region Movement
    public Vector3 curr_speed { get; set; }
    public Vector3 target_speed { get; set; }
    [SerializeField]
    private float deccel_rate = 0.3f;
    #endregion

    
    public enum CLOTHE_TYPE
    {
        HAT, SHIRT, PANTS, SHOES

    }
    public CLOTHE_TYPE clothe_type { get; set; }

    public string years { get; set; } //year of the shirt


    private SpriteRenderer sprite_rend;
    private Sprite sprite;


    // Use this for initialization
    void Start ()
    {
        is_worn = false;
        is_held = false;


        sprite_rend = GetComponent<SpriteRenderer>();//access sprite renderer

        DetermineSprite();
        

	}

    void DetermineSprite()
    {
        Debug.Log(clothe_type);

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

            case CLOTHE_TYPE.SHOES:
                sprite = Resources.Load<Sprite>("Sprites/Clothes/Shoes/spr_shoes" + years);

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

            Debug.Log("happy");
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


    /// <summary>
    /// Creates an object based on type and time period
    /// </summary>
    /// <param name="type"></param>
    /// <param string="year"></param>
    /// <returns></returns>
    public static GameObject Create(CLOTHE_TYPE type, string years)//custom init
    {
        //find container
        GameObject scriptedEntityController = GameObject.Find("ScriptedEntityController");

        //begin creation
        GameObject prefab = Resources.Load("Clothe") as GameObject;
        GameObject newObject = Instantiate(prefab, Vector3.zero, Quaternion.Euler(0, 0, 0));

        newObject.name = type.ToString(); //assign correct name
        
        Clothe clothe = newObject.GetComponent<Clothe>(); //access main component


        clothe.clothe_type = type; //set type
        clothe.years = years;  //set years
        

        return newObject;

    }

}
