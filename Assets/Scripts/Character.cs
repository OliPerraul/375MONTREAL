using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    [SerializeField]
    public int number;
    
    public bool receiving { get; set; }
    
    private Vector3 curr_scale = new Vector3(1,1,1);
    private Vector3 target_scale = new Vector3(1,1,1);

    [SerializeField]
    private float scale_rate;

    [SerializeField]
    private float max_scale;

    private SpriteRenderer sprite_rend;

    private Sprite sprite;
    
    
    //contains the clothes
    public Dictionary<Clothe.CLOTHE_TYPE, Clothe> clothes { get; set; }
    
	private AudioController audioControl;
    // Use this for initialization
    void Start ()
    {
		AudioController audio = (AudioController)FindObjectOfType (typeof(AudioController));
		audioControl = audio;
              
        clothes = new Dictionary<Clothe.CLOTHE_TYPE, Clothe>();

        ChooseSprite();

      
    }

    /// <summary>
    /// Choose the correct player sprite
    /// </summary>
    void ChooseSprite()
    {
        sprite_rend = GetComponent<SpriteRenderer>();

        sprite = Resources.Load<Sprite>("Sprites/Characters/spr_char_" + number);
        sprite_rend.sprite = sprite;

    }



    // Update is called once per frame
    void Update ()
    {
       
        DetermineScale();

    }

    void DetermineScale()
    {
        curr_scale = Vector3.Lerp(curr_scale, target_scale, scale_rate);
        transform.lossyScale.Set(curr_scale.x,curr_scale.y,curr_scale.z);
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

    /// <summary>
    /// Add clothe onto the player character
    /// </summary>
    /// <param name="clothe"></param>
    public void AddClothe(Clothe clothe) 
    {
       
        clothe.gameObject.transform.SetParent(transform); //set as child
        clothe.is_worn = true;

        Clothe.CLOTHE_TYPE clothe_type = clothe.clothe_type; //add to dictionary

        Clothe clothe_out; //ref to clothe popped out
        if (clothes.TryGetValue(clothe_type, out clothe_out)) // if clothe type already exist swap and pop  out old one
        {
            clothes.Remove(clothe_type); //remove from dictionary
            clothes.Add(clothe_type, clothe);//add new clothe

            clothe_out.is_worn = false;
            clothe_out.transform.SetParent(null); //unparent

            Vector3 reject_speed = new Vector3(Random.Range(1, 2), Random.Range(1, 2), 0); //determine reject speed
            clothe_out.curr_speed = reject_speed;
			audioControl.PlayClip(5);

        }
        else //else add clothe normally
        {
            clothes.Add(clothe_type, clothe);//add new clothe
			Debug.Log("The clothes are applied");
			audioControl.PlayClip(5);
           
        }

        //determine position on the character
        clothe.transform.position = transform.position;
        

    }




}
