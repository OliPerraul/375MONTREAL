using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundsController : MonoBehaviour
{
    string announced_style = "";

    string announced_job = "";
    

    #region Timer
    // [SerializeField]//TODO REMOVE
    float round_time = 120;
    ///

    [SerializeField]
    float timer_begin_game = 3f;

    [SerializeField]
    float max_round_time = 120;

    [SerializeField]
    float min_round_time = 10;


    [SerializeField]
    float time_between_rounds = 20;


    //current time of the round
    float timer_rounds = 60f;
    
    float between_rounds_timer = 20f;
    #endregion
    
    #region Gamestates
    //bools
    bool in_game = false;

    bool in_round = false;

    bool init_round = false;
    #endregion

    #region UI

    [SerializeField]
    private Text timer_UI;

    [SerializeField]
    private Text years_UI;

    [SerializeField]
    private Text hint_UI;


    [SerializeField]
    private ClothesPoolController clothePoolController;
    #endregion
    

    // Use this for initialization
    void Start ()
    {
        between_rounds_timer = 2f; //set interval between first round to be short
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        #region before_game

        if (!in_game)
        {
             timer_begin_game -= Time.deltaTime;



            //if count down is over
            if (timer_begin_game < 0)
            {
                if (!init_round) //initialize the round
                {
                    in_game = true;
                    init_round = true;
                    in_round = false;

                    //reset timer to random
                    timer_rounds = Random.Range(min_round_time, max_round_time);

                }
                init_round = false; //set ready for next round
            }

            


        }

        #endregion

        
        #region in_game
        if (in_game)
        {
            ////between rounds
            #region between_rounds
            //if in round decrease timer
            if (!in_round)
            {
                between_rounds_timer -= Time.deltaTime;


                //if count down is over
                if (between_rounds_timer < 0)
                {
                    if (!init_round) //initialize the round
                    {
                        init_round = true;
                        in_round = true;

                        //reset timer to random
                        timer_rounds = Random.Range(min_round_time, max_round_time);

                        clothePoolController.CreatePool();
                    }
                 }
            }
            #endregion
             

            #region in_round
            //if in round decrease timer
            if (in_round)
            {
                timer_rounds -= Time.deltaTime;
                
                //if count down is over
                if (timer_rounds < 0)
                {
                    if (in_round)
                    {
                        init_round = false;
                        in_round = false;

                        clothePoolController.RemoveNotWorn(); //remove clothes not worn

                        //reset timer to random
                        between_rounds_timer = time_between_rounds;
                    }
                }
            }
            #endregion



        }
        #endregion



        //UPDATE UI GRAPHIC
        UpdateUI();

    }



    void UpdateUI()
    {
        if (in_game)
        {
            if (in_round)
            {
                int sixty = 60;
                int minute_count = (int)round_time / sixty;

                int seconds = 0;

                if (in_round)
                {
                    sixty = 60;
                    minute_count = (int)timer_rounds / sixty;

                    seconds = Mathf.RoundToInt(timer_rounds % 60) - 1;

                }

                timer_UI.text = minute_count.ToString() + ":" + seconds.ToString();
            }
            else
            {
                timer_UI.text = ("INTERMISSION");
            }



        }
        else
        {
            timer_UI.text=("0:00" );

        }
    }
}
