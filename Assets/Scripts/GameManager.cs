using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class GameManager: MonoBehaviour {
    static Dictionary<string, Player> players = new Dictionary<string, Player>();
    
    public static void RegisterPlayer(string id, Player player)
    {
        players.Add("Player " + id, player);
        player.transform.name = "Player " + id;
    }

    public static void UnRegisterPlayer(string id)
    {   
        players.Remove(id);
    }

    public static Player GetPlayer(string id)
    {
        return players[id];
    }
}
