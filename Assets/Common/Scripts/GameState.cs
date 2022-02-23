using System;
using System.Linq;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState instance;

    public const int NumGames = 2;
    public const int SphinxNoseIdx = 0;
    public const int DinoBoneIdx = 1;

    public const int NumCharacters = 4;
    public const int DanielWhiteIdx = 0;
    public const int DanielBlackIdx = 1;
    public const int JillWhiteIdx = 2;
    public const int JillBlackIdx = 3;
    
    // Set by Unity editor
    [Header("Character properties")]
    public RuntimeAnimatorController[] menuAnimators;
    public RuntimeAnimatorController[] ingameAnimators;
    public Sprite[] characterImage;
    
    // Changed during the game
    [Header("Ingame properties - DO NOT SET")]
    public int selectedCharacterIdx;
    public bool[] readDescriptions;
    public bool[] playedGames;

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
}