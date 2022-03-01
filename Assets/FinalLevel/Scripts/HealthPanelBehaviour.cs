using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPanelBehaviour : MonoBehaviour
{
    private GameObject _heart1;

    private GameObject _heart2;

    private GameObject _heart3;

    public static HealthPanelBehaviour Instance;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of HealthPanelBeahviour!");
        }

        Instance = this;
    }
    
    private void Start()
    {
        _heart1 = GameObject.Find("Heart1");
        _heart2 = GameObject.Find("Heart2");
        _heart3 = GameObject.Find("Heart3");
    }

    public void SetHealth(int hp)
    {
        switch (hp)
        {
            case 0:
                _heart1.SetActive(false);
                _heart2.SetActive(false);
                _heart3.SetActive(false);
                break;
            case 1:
                _heart1.SetActive(true);
                _heart2.SetActive(false);
                _heart3.SetActive(false);
                break;
            case 2:
                _heart1.SetActive(true);
                _heart2.SetActive(true);
                _heart3.SetActive(false);
                break;
            case 3:
                _heart1.SetActive(true);
                _heart2.SetActive(true);
                _heart3.SetActive(true);
                break;
        }
    }
}
