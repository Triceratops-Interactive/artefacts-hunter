using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [FormerlySerializedAs("_dialogueElements")] [SerializeField]
    public DialogueElement[] introDialogue;

    private void Start()
    {
        DialogueManager.instance.DisplayDialogue(introDialogue);
    }
}