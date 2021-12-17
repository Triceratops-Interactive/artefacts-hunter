using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class GameState : MonoBehaviour
{
    public static GameState instance;

    public const int NumGames = 2;
    public const int SphinxNoseIdx = 0;
    public const int DinoBoneIdx = 1;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            readDescriptions = new bool[NumGames];
            playedGames = new bool[NumGames];
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int NumPlayedGames()
    {
        return playedGames.Count(playedGame => playedGame);
    }
    
    public bool[] readDescriptions;
    public bool[] playedGames;
}