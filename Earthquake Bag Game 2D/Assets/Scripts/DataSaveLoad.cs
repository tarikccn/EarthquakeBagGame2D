using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataSaveLoad
{
    public void SavePlayerScore(string playerName, int playerScore)
    {
        List<Player> players = LoadPlayerData();
        // JSON verisini oluþtur
        Player player = new Player(playerName, playerScore);
        players.Add(player);

        // List<Player> nesnesini serialize et
        string serializedPlayers = JsonConvert.SerializeObject(players);

        // JSON verisini dosyada sakla
        string path = Application.dataPath + "/playerData.json";
        File.WriteAllText(path, serializedPlayers);
    }

    public List<Player> LoadPlayerData()
    {
        // JSON verisini dosyadan yükle
        string path = Application.dataPath + "/playerData.json";
        if (!File.Exists(path))
        {
            return new List<Player>();
        }
        string json = File.ReadAllText(path);

        // JSON verisini Player sýnýfýna dönüþtür
        List<Player> players = JsonConvert.DeserializeObject<List<Player>>(json);
        for (int i = 0; i < players.Count; i++)
        {
            Debug.Log(players[i].name);
        }

        return players;
    }
}

public class Player
{
    public string name;
    public int score;

    public Player(string playerName, int playerScore)
    {
        name = playerName;
        score = playerScore;
    }
}

