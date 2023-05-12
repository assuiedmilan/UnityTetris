using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public enum TetrominoShapes
{
    Debug,
    I,
    O,
    T,
    J,
    L,
    S,
    Z,
}

[Serializable]
public struct TetrominoData
{
    [FormerlySerializedAs("Tetromino")] public TetrominoShapes tetromino;
    [FormerlySerializedAs("Tile")] public Tile tile;
    [FormerlySerializedAs("Cells")] public Vector2Int[] cells;

    public void Initialize()
    {
        cells = TetrominosCoordinates.Cells[tetromino];
    }
}
