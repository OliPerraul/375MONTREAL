using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {


    public int number { get; set; }
    
    public bool receiving { get; set; }
    
    private Vector3 curr_scale = new Vector3(1,1,1);
    private Vector3 target_scale = new Vector3(1,1,1);

    [SerializeField]
    private float scale_rate;

    [SerializeField]
    private float max_scale;

    private SpriteRenderer sprite_rend;
    


    //contains the clothes
    private Dictionary<Clothe.CLOTHE_TYPE, Clothe> clothes;
    

    // Use this for initialization
    void Start ()
    {
        sprite_rend = GetComponent<SpriteRenderer>();
        sprite_rend.color = PlayersController.player_colors[number - 1];


        clothes = new Dictionary<Clothe.CLOTHE_TYPE, Clothe>();

       
    }

   


	
	// Update is called once per frame
	void Update ()
    {
       
        DetermineScale();

    }

    void DetermineScale()
    {
        curr_scale = Vector3.Lerp(curr_scale, target_scale, scale_rate);
        transform.localScale = curr_scale;
    }
    
    // Update is called once per frame
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Clothe")
        {
            Clothe clothe = other.GetComponent<Clothe>();

            if (clothe.is_held) //if the clothe is is_held
            {
                receiving = true;
                target_scale = new Vector3(max_scale, max_scale, max_scale);
            }
                      
        }
        
    }

    // Update is called once per frame
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Clothe")
        {
            Clothe clothe = other.GetComponent<Clothe>();

            receiving = false;
            target_scale = new Vector3(1,1,1);

        }

    }


    public void AddClothe(Clothe clothe) 
    {
        clothe.gameObject.transform.SetParent(transform); //set as child

        clothe.is_worn = true;

        Clothe.CLOTHE_TYPE clothe_type = clothe.clothe_type; //add to dictionary
        clothes.Add(clothe_type, clothe);

        switch (clothe_type)
        {
            case Clothe.CLOTHE_TYPE.HAT:
                clothe.transform.position = transform.position + new Vector3(0, 2, 0);
                break;
            case Clothe.CLOTHE_TYPE.SHIRT:
                clothe.transform.position = transform.position;
                break;
            case Clothe.CLOTHE_TYPE.PANTS:
                clothe.transform.position = transform.position + new Vector3(0, -2, 0);
                break;
            case Clothe.CLOTHE_TYPE.SHOES:
                clothe.transform.position = transform.position + new Vector3(0, -4, 0);
                break;

        }

        

    }


    void WearClothe(Clothe clothe)
    {





    }




}
