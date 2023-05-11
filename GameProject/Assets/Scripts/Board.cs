using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
    [SerializeField] public TetrominoData[] tetrominos;
    public Tilemap Tilemap { get; private set; }
        
    private void Awake()
    {
        Tilemap = GetComponentInChildren<Tilemap>();
        foreach (var tetromino in tetrominos)
        {
            tetromino.Initialize();
        }
    }

    private void Start()
    {
        SpawnPiece();
    }

    private void SpawnPiece()
    {
        var shapeIndex = Random.Range(0, tetrominos.Length);
        var spawnedTetrominoData = tetrominos[shapeIndex];
    }

}
