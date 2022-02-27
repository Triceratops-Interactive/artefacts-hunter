using System;
using UnityEngine;

public class FinalLevelBehaviour : MonoBehaviour
{
    [SerializeField] private Transform sandPrefab;
    [SerializeField] private int floorSizeX = 12;
    [SerializeField] private int floorSizeY = 6;

    private Transform _floor;

    private void Awake()
    {
        _floor = GameObject.Find("Floor").GetComponent<Transform>();
    }

    private void Start()
    {
        GenerateFloor();
    }

    private void GenerateFloor()
    {
        
        for (int x = 0; x <= floorSizeX; x++)
        {
            for (int y = 0; y <= floorSizeY; y++)
            {
                var sand = Instantiate(sandPrefab, this._floor);
                sand.position = new Vector3(x - (floorSizeX / 2), y - (floorSizeY / 2));
            }
        }
    }
}