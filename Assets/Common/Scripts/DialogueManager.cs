using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [SerializeField] private AudioClip textScrollClip;

    private Text _textComponent;
    private Image _characterImage;
    private DialogueElement[] _currentElements;
    private int _elementPos = 0;
    private int _dialoguePos = -1;
    private Action _callback;
    private string _currentDisplayedText;
    private int _currentDisplayedCharacterCount;
    private bool _soundInterrupted = false;
    private AudioClip _currentClip;
    private bool _currentTextFinished;

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
            _currentDisplayedCharacterCount = 0;
            EnablePanel(false);
            StopSound();
            _callback?.Invoke();

            return;
        }

        if (_currentElements[_elementPos].characterSprite == null)
        {
            _characterImage.enabled = false;
        }
        else
        {
            _characterImage.enabled = true;
            if (_currentElements[_elementPos].characterSprite.Equals(GameState.instance.characterImage[0]))
            {
                _characterImage.sprite = GameState.instance.playerScarred
                    ? GameState.instance.characterScarImage[GameState.instance.selectedCharacterIdx]
                    : GameState.instance.characterImage[GameState.instance.selectedCharacterIdx];
            }
            else
            {
                _characterImage.sprite = _currentElements[_elementPos].characterSprite;
            }
        }

        _currentDisplayedCharacterCount = 0;
        _currentTextFinished = false;
        _currentClip = _currentElements[_elementPos].textFinishClip;
        _currentDisplayedText = _currentElements[_elementPos].dialogue[_dialoguePos];
        if (GameState.instance.selectedCharacterIdx == GameState.JillWhiteIdx ||
            GameState.instance.selectedCharacterIdx == GameState.JillBlackIdx)
        {
            _currentDisplayedText = _currentDisplayedText.Replace("young man", "young lady");
        }

        if (textScrollClip != null)
        {
            PlaySound();
        }
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
        if (IngameMenuBehaviour.instance != null && IngameMenuBehaviour.instance.IsMenuActive())
        {
            return;
        }

        var confirmed = Input.GetButtonDown("Fire1");
        if (confirmed)
        {
            NextDialogue();
        }
    }

    private void FixedUpdate()
    {
        if (_currentDisplayedCharacterCount >= _currentDisplayedText.Length)
        {
            if (!_currentTextFinished)
            {
                _currentTextFinished = true;
                StopSound();
                if (_currentClip != null)
                {
                    SoundManager.instance.GetEffectSource().PlayOneShot(_currentClip);
                }
            }
            return;
        }

        if (IngameMenuBehaviour.instance != null && IngameMenuBehaviour.instance.IsMenuActive())
        {
            StopSound();
            _soundInterrupted = true;
            return;
        }

        if (_soundInterrupted)
        {
            PlaySound();
            _soundInterrupted = false;
        }

        _currentDisplayedCharacterCount++;
        _textComponent.text = _currentDisplayedText.Substring(0, _currentDisplayedCharacterCount);
    }

    private void PlaySound()
    {
        SoundManager.instance.GetEffectSource().Stop();
        SoundManager.instance.GetEffectSource().loop = true;
        SoundManager.instance.GetEffectSource().PlayOneShot(textScrollClip);
    }

    private void StopSound()
    {
        SoundManager.instance.GetEffectSource().Stop();
        SoundManager.instance.GetEffectSource().loop = false;
    }

    private void EnablePanel(bool enable)
    {
        gameObject.SetActive(enable);
    }
}