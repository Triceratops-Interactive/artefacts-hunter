using UnityEngine;

public class JanitorBehaviour : DialogueBehaviour
{
    [SerializeField] private DialogueElement[] beforeFirstMinigameTalk;

    public override DialogueElement[] GiveDialogue()
    {
        return beforeFirstMinigameTalk;
    }
}