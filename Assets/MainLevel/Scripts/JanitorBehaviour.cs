using System;
using UnityEngine;

public class JanitorBehaviour : DialogueBehaviour
{
    [SerializeField] private DialogueElement[] beforeFirstMinigameTalk;
    [SerializeField] private DialogueElement[] afterFirstMinigameTalk;
    [SerializeField] private DialogueElement[] beforeLastMinigameTalk;
    [SerializeField] private float fadeOutDelay = 2;

    private bool _fadingOut;
    private float _fadeOutTime;
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public override (DialogueElement[] dialogue, Action callback) GiveDialogue()
    {
        if (GameState.instance.NumPlayedGames() == 0)
        {
            return (beforeFirstMinigameTalk, null);
        }
        else if (GameState.instance.NumPlayedGames() == GameState.NumGames - 1)
        {
            return (beforeLastMinigameTalk, null);
        }
        else
        {
            return (afterFirstMinigameTalk, null);
        }
    }
    
    private void FixedUpdate()
    {
        if (!_fadingOut || _fadeOutTime <= 0) return;

        _fadeOutTime -= Time.fixedDeltaTime;

        if (_fadeOutTime < 0)
        {
            _fadingOut = false;
            gameObject.SetActive(false);
        }
        else
        {
            var alpha = _fadeOutTime / fadeOutDelay;
            _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, alpha);
        }
    }

    public void FadeOut()
    {
        _fadeOutTime = fadeOutDelay;
        _fadingOut = true;
    }

    public bool IsFadingOut()
    {
        return _fadingOut;
    }
}