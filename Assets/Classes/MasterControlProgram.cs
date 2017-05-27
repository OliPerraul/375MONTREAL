using System.Collections.Generic;

public static class MasterControlProgram
{
    public static int num_players = 2;

    //[player1, player2..]
    public static int[] player_scores;

    //themes lookup
    public static List<string> themes_lookup = new List<string> { "cirque", "construction", "hipster"};


    //style lookup
    // public static List<string> themes_lookup = new List<string> { "cirque", "construction", "hipster" };

    //job lookup
    // public static List<string> themes_lookup = new List<string> { "cirque", "construction", "hipster" };


    //initialize reference list
    public static List<string> job_lookup = new List<string> { "gendarme", "bucheron", "clerge", "cirque", "construction", "hipster", "superhero", "ballerine", "dracula", "mozart", "legionnaireromain", "pharaon" };

    public static List<string> style_lookup = new List<string> { "industrialisation", "prohibition", "pinUp", "hippie", "disco", "glam", "hipHop", "coureurdesbois", };

    //public static List<string> themes_lookup = new List<string> { "industrialisation", "prohibition", "pinUp", "hippie", "disco", "glam", "hipHop", "coureur_des_bois", "gendarme", "bucheron", "clerge", "cirque", "construction", "hipster", "superhero", "ballerine", "dracula", "mozart", "legionnaire_romain", "pharaon" };



}