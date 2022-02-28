using System;
using System.Linq;
using UnityEngine;

public class FinalLevelBehaviour : MonoBehaviour
{
    [Header("Level Generation")] [SerializeField]
    private int floorSizeX = 12;

    [SerializeField] private int floorSizeY = 6;
    [SerializeField] private Transform sandPrefab;
    [SerializeField] private Transform upHighMid;
    [SerializeField] private Transform upLowMid;
    [SerializeField] private Transform upHighLeft;
    [SerializeField] private Transform upLowLeft;
    [SerializeField] private Transform upHighRight;
    [SerializeField] private Transform upLowRight;
    [SerializeField] private Transform leftUp;
    [SerializeField] private Transform leftMid;
    [SerializeField] private Transform leftHighDown;
    [SerializeField] private Transform leftLowDown;
    [SerializeField] private Transform rightUp;
    [SerializeField] private Transform rightMid;
    [SerializeField] private Transform rightHighDown;
    [SerializeField] private Transform rightLowDown;
    [SerializeField] private Transform downMid;
    [SerializeField] private Transform downLeft;
    [SerializeField] private Transform downRight;

    [Header("Dialogues")] [SerializeField] private DialogueElement[] introDialogue;


    private Transform _floor;
    private Transform _walls;

    private void Awake()
    {
        _floor = GameObject.Find("Floor").GetComponent<Transform>();
        _walls = GameObject.Find("Walls").GetComponent<Transform>();
    }

    private void Start()
    {
        GenerateFloor();
        GenerateWalls();
        PositionCharacters();
        DialogueManager.instance.DisplayDialogue(introDialogue);
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

    private void GenerateWalls()
    {
        int maxY = floorSizeY / 2;
        int minY = -maxY;
        int maxX = floorSizeX / 2;
        int minX = -maxX;

        // Upper wall
        Instantiate(upLowLeft, _walls).position = new Vector3(minX, maxY);
        Instantiate(upHighLeft, _walls).position = new Vector3(minX, maxY + 1);
        for (int x = minX + 1; x < maxX; x++)
        {
            Instantiate(upLowMid, _walls).position = new Vector3(x, maxY);
            Instantiate(upHighMid, _walls).position = new Vector3(x, maxY + 1);
        }

        Instantiate(upLowRight, _walls).position = new Vector3(maxX, maxY);
        Instantiate(upHighRight, _walls).position = new Vector3(maxX, maxY + 1);

        // Left wall
        Instantiate(leftUp, _walls).position = new Vector3(minX, maxY);
        for (int y = maxY - 1; y > minY + 1; y--)
        {
            Instantiate(leftMid, _walls).position = new Vector3(minX, y);
        }

        Instantiate(leftHighDown, _walls).position = new Vector3(minX, minY + 1);
        Instantiate(leftLowDown, _walls).position = new Vector3(minX, minY);

        // Right wall
        Instantiate(rightUp, _walls).position = new Vector3(maxX, maxY);
        for (int y = maxY - 1; y > minY + 1; y--)
        {
            Instantiate(rightMid, _walls).position = new Vector3(maxX, y);
        }

        Instantiate(rightHighDown, _walls).position = new Vector3(maxX, minY + 1);
        Instantiate(rightLowDown, _walls).position = new Vector3(maxX, minY);

        // Lower wall
        Instantiate(downLeft, _walls).position = new Vector3(minX, minY);
        for (int x = minX + 1; x < maxX; x++)
        {
            Instantiate(downMid, _walls).position = new Vector3(x, minY);
        }

        Instantiate(downRight, _walls).position = new Vector3(maxX, minY);
    }

    private void PositionCharacters()
    {
        GameObject.Find("Caesar").GetComponent<Animator>().Play("PlayerMoveDown");
    }
}