using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    [SerializeField]
    private int num_players = 1;

    private Vector3 start_scale;

    private Vector3 curr_scale;
    private Vector3 target_scale;

    [SerializeField]
    private float scale_rate;

    [SerializeField]
    private float max_scale;

   

    // Use this for initialization
    void Start ()
    {
        start_scale = transform.lossyScale;
        curr_scale = start_scale;
        target_scale = start_scale;
        
        	
	}

    // Update is called once per frame
    void Update()
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
        if (other.gameObject.tag == "Cursor")
        {
            target_scale = new Vector3(start_scale.x * max_scale, start_scale.y * max_scale, start_scale.z * max_scale);

           bool A_pressed = Input.GetKeyDown("joystick button 0");

            if (A_pressed)
            {
                MasterControlProgram.num_players = num_players;
                //goto new scene
                SceneManager.LoadScene("gameplay", LoadSceneMode.Single);


            }

        }

    }

    // Update is called once per frame
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Cursor")
        {
            target_scale = start_scale;

            bool A_pressed = Input.GetKeyDown("joystick button 0");
          

            

        }

    }


}
