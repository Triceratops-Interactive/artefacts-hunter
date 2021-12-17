using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    private Text _textComponent;
    private Image _characterImage;
    private DialogueElement[] _currentElements;
    private int _elementPos = 0;
    private int _dialoguePos = -1;
    private Action _callback;

    public void DisplayDialogue(DialogueElement[] dialogueElements, Action callback = null)
    {
        _currentElements = dialogueElements;
        _callback = callback;
        NextDialogue();
        EnablePanel(true);
    }

    public bool IsDisplayingDialogue()
    {
        return _dialoguePos != -1;
    }

    private void NextDialogue()
    {
        _dialoguePos++;
        if (_dialoguePos >= _currentElements[_elementPos].dialogue.Length)
        {
            _elementPos++;
            _dialoguePos = 0;
        }

        if (_elementPos >= _currentElements.Length)
        {
            // Finished displaying dialogue
            _elementPos = 0;
            _dialoguePos = -1;
            EnablePanel(false);
            _callback?.Invoke();
            
            return;
        }

        _characterImage.enabled = _currentElements[_elementPos].characterSprite != null;
        if (_currentElements[_elementPos].characterSprite.Equals(GameState.instance.characterImage[0]))
        {
            // Set image of chosen character
            _characterImage.sprite = GameState.instance.characterImage[GameState.instance.selectedCharacterIdx];
        }
        else
        {
            _characterImage.sprite = _currentElements[_elementPos].characterSprite;
        }
        _textComponent.text = _currentElements[_elementPos].dialogue[_dialoguePos];
    }

    private void Awake()
    {
        if (instance != null)
        {
            throw new Exception("Multiple Instances of DialogueManager");
        }

        instance = this;
        
        _textComponent = GameObject.Find("DialogueText").GetComponent<Text>();
        _characterImage = GameObject.Find("CharacterImage").GetComponent<Image>();

        EnablePanel(false);
    }

    private void Update()
    {
        var confirmed = Input.GetButtonDown("Fire1");
        if (confirmed)
        {
            NextDialogue();
        }
    }

    private void EnablePanel(bool enable)
    {
        gameObject.SetActive(enable);
    }
}
