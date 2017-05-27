using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersController : MonoBehaviour
{
    public static Player[] players;

   
    public static Color[] player_colors;

    [SerializeField]
    public Color color_player1 = Color.blue;

    [SerializeField]
    public Color color_player2 = Color.red;

    [SerializeField]
    public Color color_player3 = Color.green;

    [SerializeField]
    public Color color_player4  = Color.yellow;



    // Use this for initialization
    void Start()
    {
        players = new Player[4]; //init static array
        player_colors = new Color[4]; //init static array

        //init player colors
        player_colors[0] = color_player1;

        //init player colors
        player_colors[1] = color_player2;

        //init player colors
        player_colors[2] = color_player3;

        //init player colors
        player_colors[3] = color_player4;



        InitPlayers();//initialize array of players
         

}


    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.zero; //force fixed pos


    }


    private void InitPlayers()
    {
        GameObject prefab;

        GameObject playerObj1;
        GameObject playerObj2;
        GameObject playerObj3;
        GameObject playerObj4;

        Player player1;
        Player player2;
        Player player3;
        Player player4;


        switch (MasterControlProgram.num_players)
        {

            case 2:
                prefab = Resources.Load("Player") as GameObject;

                playerObj1 = Instantiate(prefab, Vector3.zero, Quaternion.Euler(0, 0, 0));
                playerObj2 = Instantiate(prefab, Vector3.zero, Quaternion.Euler(0, 0, 0));


                playerObj1.name = "Player1";
                playerObj2.name = "Player2";


                player1 = playerObj1.GetComponent<Player>(); //access main component
                player2 = playerObj2.GetComponent<Player>(); //access main component


                player1.number = 1;
                player2.number = 2;


                //fill in static array of players
                PlayersController.players[0] = player1;
                PlayersController.players[1] = player2;

                break;

                case 3:

                prefab = Resources.Load("Player") as GameObject;

                playerObj1 = Instantiate(prefab, Vector3.zero, Quaternion.Euler(0, 0, 0));
                playerObj2 = Instantiate(prefab, Vector3.zero, Quaternion.Euler(0, 0, 0));
                playerObj3 = Instantiate(prefab, Vector3.zero, Quaternion.Euler(0, 0, 0));


                playerObj1.name = "Player1";
                playerObj2.name = "Player2";
                playerObj3.name = "Player3";


                player1 = playerObj1.GetComponent<Player>(); //access main component
                player2 = playerObj2.GetComponent<Player>(); //access main component
                player3 = playerObj3.GetComponent<Player>(); //access main component


                player1.number = 1;
                player2.number = 2;
                player3.number = 3;


                //fill in static array of players
                PlayersController.players[0] = player1;
                PlayersController.players[1] = player2;
                PlayersController.players[2] = player3;
                break;

            case 4:
                prefab = Resources.Load("Player") as GameObject;

                playerObj1 = Instantiate(prefab, Vector3.zero, Quaternion.Euler(0, 0, 0));
                playerObj2 = Instantiate(prefab, Vector3.zero, Quaternion.Euler(0, 0, 0));
                playerObj3 = Instantiate(prefab, Vector3.zero, Quaternion.Euler(0, 0, 0));
                playerObj4 = Instantiate(prefab, Vector3.zero, Quaternion.Euler(0, 0, 0));

                playerObj1.name = "Player1";
                playerObj2.name = "Player2";
                playerObj3.name = "Player3";
                playerObj4.name = "Player4";

                player1 = playerObj1.GetComponent<Player>(); //access main component
                player2 = playerObj2.GetComponent<Player>(); //access main component
                player3 = playerObj3.GetComponent<Player>(); //access main component
                player4 = playerObj4.GetComponent<Player>(); //access main component

                player1.number = 1;
                player2.number = 2;
                player3.number = 3;
                player4.number = 4;

                //fill in static array of players
                PlayersController.players[0] = player1;
                PlayersController.players[1] = player2;
                PlayersController.players[2] = player3;
                PlayersController.players[3] = player4;
                break;
        }


    }
   

}
