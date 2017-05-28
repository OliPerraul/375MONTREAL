using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundsController : MonoBehaviour
{


    //CURRENTYL DISPLAYED SCORES
    private int displayed_score1 = 0;

    private int displayed_score2 = 0;

    private int displayed_score3 = 0;

    private int displayed_score4 = 0;




    [SerializeField]
    private float score_trans_speed = .01f;


    [SerializeField]
    private int points_annouced_theme = 15;

    [SerializeField]
    private int points_fullset = 50;



    string announced_style = "";

    string announced_job = "";
    [SerializeField]
    private AudioController audioControl;

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
    private TextMesh timer_UI;

    [SerializeField]
    private TextMesh hint_UI;



    //SCORES UIs
    [SerializeField]
    private TextMesh score1_UI;

    [SerializeField]
    private TextMesh score2_UI;

    [SerializeField]

    private TextMesh score3_UI;

    [SerializeField]
    private TextMesh score4_UI;



    [SerializeField]
    private ClothesPoolController clothePoolController;
    #endregion


    // Use this for initialization
    void Start()
    {
        AudioController audio = (AudioController)FindObjectOfType(typeof(AudioController));
        audioControl = audio;
        between_rounds_timer = 2f; //set interval between first round to be short

    }

    // Update is called once per frame
    void Update()
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



                        //////annouce themes//////

                        //job
                        announced_job = Global.job_lookup[Random.Range(0, Global.job_lookup.Count - 1)];
                        //style
                        announced_style = Global.style_lookup[Random.Range(0, Global.style_lookup.Count - 1)];

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

                        DetermineScores();//determine the scores

                    }
                }
            }
            #endregion



        }
        #endregion


        //UPDATE UI GRAPHIC
        UpdateUI();

    }


    void DetermineScores()
    {
        for (int i = 0; i < Global.num_players; i++)
        {

            if (PlayersController.players[i] == null)
                return;

            //Save old score as ref
            Global.old_scores[i] = Global.scores[i];



            Character character = PlayersController.players[i].character;//character;

            Clothe hat;
            bool hat_found = character.clothes.TryGetValue(Clothe.CLOTHE_TYPE.HAT, out hat);

            Clothe shirt;
            bool shirt_found = character.clothes.TryGetValue(Clothe.CLOTHE_TYPE.SHIRT, out shirt);

            Clothe pants;
            bool pants_found = character.clothes.TryGetValue(Clothe.CLOTHE_TYPE.PANTS, out pants);

            if (hat_found)
            {
                if (hat.theme == announced_job || hat.theme == announced_style)
                {
                    Global.scores[i] += 15;
                    audioControl.PlayClip(0);

                }
            }

            if (shirt_found)
            {
                if (shirt.theme == announced_job || shirt.theme == announced_style)
                {
                    Global.scores[i] += 15;
                    audioControl.PlayClip(0);

                }
            }

            if (pants_found)
            {
                if (pants.theme == announced_job || pants.theme == announced_style)
                {
                    Global.scores[i] += 15;
                    audioControl.PlayClip(0);

                }
            }

            //Full set
            if ((hat_found) && (shirt_found) && (pants_found))
            {
                if (hat.theme == shirt.theme && shirt.theme == pants.theme)
                {
                    Global.scores[i] += 50;

                }
            }





        }

    }


    void UpdateUI()
    {

        DetermineActiveScores();




        if (in_game)
        {


            if (in_round)
            {
                int sixty = 60;
                int minute_count = (int)round_time / sixty;

                int seconds = 0;


                sixty = 60;
                minute_count = (int)timer_rounds / sixty;

                seconds = Mathf.RoundToInt(timer_rounds % 60) - 1;


                timer_UI.text = minute_count.ToString() + ":" + seconds.ToString();


                //update hint 
                if (announced_style == "coureurdesbois")
                { hint_UI.text = "La fête du 375 des " + announced_job + "s à l'époque des" + announced_style + "."; }
                else
                    hint_UI.text = "La fête du 375 des" + announced_job + "s à l'époque de la " + announced_style + ".";


            }
            else
            {
                timer_UI.text = ("INTERMISSION");

                hint_UI.text = ("");


            }



        }
        else
        {
            timer_UI.text = ("0:00");

        }



    }





    /////cancells the ones not played on
    void DetermineActiveScores()
    {
        
        if (Global.num_players == 2)
        {
            displayed_score1 = Global.scores[0];

            displayed_score2 = Global.scores[1];

            score1_UI.text = displayed_score1.ToString();
            score2_UI.text = displayed_score2.ToString();
            score3_UI.text = "";
            score4_UI.text = "";

        }

        if (Global.num_players == 3)
        {

            displayed_score1 = Global.scores[0];

            displayed_score2 = Global.scores[1];

            displayed_score3 = Global.scores[2];


            score1_UI.text = displayed_score1.ToString();
            score2_UI.text = displayed_score2.ToString();
            score3_UI.text = displayed_score3.ToString();
            score4_UI.text = "";

        }

        if ((Global.num_players == 3))
        {
            displayed_score1 = Global.scores[0];

            displayed_score2 = Global.scores[1];

            displayed_score3 = Global.scores[2];

            displayed_score4 = Global.scores[3];


            score1_UI.text = displayed_score1.ToString();
            score2_UI.text = displayed_score2.ToString();
            score3_UI.text = displayed_score3.ToString();
            score4_UI.text = displayed_score4.ToString();
        }

    }




}