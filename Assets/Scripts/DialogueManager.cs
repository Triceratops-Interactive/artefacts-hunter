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

    public void DisplayDialogue(DialogueElement[] dialogueElements)
    {
        _currentElements = dialogueElements;
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
            _elementPos = 0;
            _dialoguePos = -1;
            EnablePanel(false);
            return;
        }

        _characterImage.sprite = _currentElements[_elementPos].characterSprite;
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
