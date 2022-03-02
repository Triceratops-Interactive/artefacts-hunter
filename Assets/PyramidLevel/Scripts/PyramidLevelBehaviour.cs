using UnityEngine;

public class PyramidLevelBehaviour : MonoBehaviour
{
    [SerializeField] private DialogueElement[] introDialogue;

    private void Start()
    {
        DialogueManager.instance.DisplayDialogue(introDialogue);
    }
}