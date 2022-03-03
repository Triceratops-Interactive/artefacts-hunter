using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit_Dino : MonoBehaviour
{
    [SerializeField] private DialogueElement[] exitDialogue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DialogueManager.instance.DisplayDialogue(exitDialogue, BackToMuseum);
        }
    }

    private void BackToMuseum()
    {
        GameState.instance.playedGames[GameState.DinoBoneIdx] = true;
        SceneManager.LoadScene("MainScene");
    }
}
