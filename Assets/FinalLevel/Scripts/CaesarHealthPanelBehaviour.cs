using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaesarHealthPanelBehaviour : MonoBehaviour
{
    private GameObject _heart1;
    private GameObject _heart2;
    private GameObject _heart3;
    private GameObject _caesarImage;
    
    public static CaesarHealthPanelBehaviour Instance;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of CaesarHealthPanelBehaviour!");
        }

        Instance = this;
    }
    
    private void Start()
    {
        _heart1 = GameObject.Find("CHeart1");
        _heart2 = GameObject.Find("CHeart2");
        _heart3 = GameObject.Find("CHeart3");
        _caesarImage = GameObject.Find("CaesarImage");
        gameObject.SetActive(false);
    }

    public void SetHealth(int hp)
    {
        switch (hp)
        {
            case 0:
                _heart1.SetActive(false);
                _heart2.SetActive(false);
                _heart3.SetActive(false);
                _caesarImage.SetActive(false);
                break;
            case 1:
                _heart1.SetActive(true);
                _heart2.SetActive(false);
                _heart3.SetActive(false);
                _caesarImage.SetActive(true);
                break;
            case 2:
                _heart1.SetActive(true);
                _heart2.SetActive(true);
                _heart3.SetActive(false);
                _caesarImage.SetActive(true);
                break;
            case 3:
                _heart1.SetActive(true);
                _heart2.SetActive(true);
                _heart3.SetActive(true);
                _caesarImage.SetActive(true);
                break;
        }
    }
}
