using System;
using System.Linq;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState instance;

    public const int NumGames = 3;
    public const int SphinxNoseIdx = 0;
    public const int DinoBoneIdx = 1;
    public const int LaurelIdx = 2;

    public const int NumCharacters = 4;
    public const int DanielWhiteIdx = 0;
    public const int DanielBlackIdx = 1;
    public const int JillWhiteIdx = 2;
    public const int JillBlackIdx = 3;

    // Set by Unity editor
    [Header("Character properties")] public RuntimeAnimatorController[] menuAnimators;
    public RuntimeAnimatorController[] ingameAnimators;
    public RuntimeAnimatorController[] fightAnimators;
    public Sprite[] characterImage;
    public Sprite[] characterScarImage;

    // Changed during the game
    [Header("Ingame properties - DO NOT SET")]
    public bool mute;
    public int selectedCharacterIdx;

    public bool[] readDescriptions;
    public bool[] playedGames;
    public bool shownIntroDialogue;
    public bool shownFirstReturn;
    public bool shownLastFightTalk;
    public bool shownLastTalk;
    public bool playerScarred;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            if (readDescriptions.Length != NumGames)
            {
                readDescriptions = new bool[NumGames];
            }

            if (playedGames.Length != NumGames)
            {
                playedGames = new bool[NumGames];
            }

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