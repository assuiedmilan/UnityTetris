using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TetrominoShapes
{
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
    public TetrominoShapes tetromino;
    public Tile tile;
    public Vector2Int[] cells;
    public Vector2Int[,] WallKicks { get; private set; }

    public void Initialize()
    {
        cells = TetrominosCoordinates.Cells[tetromino];
        WallKicks = TetrominosCoordinates.WallKicks[tetromino];
    }
}
