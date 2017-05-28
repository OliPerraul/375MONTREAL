using System.Collections.Generic;

public static class Global
{
    public static int num_players = 2;

    //[player1, player2..]
    public static int[] player_scores;

    //themes lookup
    public static List<string> themes_lookup = new List<string> { "cirque", "construction", "hipster", "bucheron", "disco", "gendarme", "hippie", "hipster", "pinup", "superhero", "ballerine", "prohibition", "dracula", "glam", "industrialisation", "legionaire", "mozart"};


    //initialize reference list
    public static List<string> job_lookup = new List<string> { "gendarme", "bucheron", "cirque", "construction", "hipster"};

    public static List<string> style_lookup = new List<string> { "industrialisation", "prohibition", "pinUp", "hippie", "disco", "glam", "hipHop", "coureurdesbois", };

   
        //contains the scores
    public static int[] scores = new int[4];

    //contains the scores
    public static int[] old_scores = new int[4];


    public static int winner = 0;


}