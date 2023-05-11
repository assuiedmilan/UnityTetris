using System.Collections.Generic;
using UnityEngine;

public struct TetrominosCoordinates
{
    private static readonly float Cos = Mathf.Cos(Mathf.PI / 2f);
    private static readonly float Sin = Mathf.Sin(Mathf.PI / 2f);
    private static readonly float[] RotationMatrix = new[] { Cos, Sin, -Sin, Cos };

    public static readonly Dictionary<TetrominoShapes, Vector2Int[]> Cells = new()
    {
        { TetrominoShapes.I, new Vector2Int[] { new (-1, 1), new ( 0, 1), new ( 1, 1), new ( 2, 1) } },
        { TetrominoShapes.J, new Vector2Int[] { new (-1, 1), new (-1, 0), new ( 0, 0), new ( 1, 0) } },
        { TetrominoShapes.L, new Vector2Int[] { new ( 1, 1), new (-1, 0), new ( 0, 0), new ( 1, 0) } },
        { TetrominoShapes.O, new Vector2Int[] { new ( 0, 1), new ( 1, 1), new ( 0, 0), new ( 1, 0) } },
        { TetrominoShapes.S, new Vector2Int[] { new ( 0, 1), new ( 1, 1), new (-1, 0), new ( 0, 0) } },
        { TetrominoShapes.T, new Vector2Int[] { new ( 0, 1), new (-1, 0), new ( 0, 0), new ( 1, 0) } },
        { TetrominoShapes.Z, new Vector2Int[] { new (-1, 1), new ( 0, 1), new ( 0, 0), new ( 1, 0) } },
    };

    private static readonly Vector2Int[,] WallKicksI = {
        { new (0, 0), new (-2, 0), new ( 1, 0), new (-2,-1), new ( 1, 2) },
        { new (0, 0), new ( 2, 0), new (-1, 0), new ( 2, 1), new (-1,-2) },
        { new (0, 0), new (-1, 0), new ( 2, 0), new (-1, 2), new ( 2,-1) },
        { new (0, 0), new ( 1, 0), new (-2, 0), new ( 1,-2), new (-2, 1) },
        { new (0, 0), new ( 2, 0), new (-1, 0), new ( 2, 1), new (-1,-2) },
        { new (0, 0), new (-2, 0), new ( 1, 0), new (-2,-1), new ( 1, 2) },
        { new (0, 0), new ( 1, 0), new (-2, 0), new ( 1,-2), new (-2, 1) },
        { new (0, 0), new (-1, 0), new ( 2, 0), new (-1, 2), new ( 2,-1) },
    };

    private static readonly Vector2Int[,] WallKicksJ_L_O_S_T_Z = {
        { new (0, 0), new (-1, 0), new (-1, 1), new (0,-2), new (-1,-2) },
        { new (0, 0), new ( 1, 0), new ( 1,-1), new (0, 2), new ( 1, 2) },
        { new (0, 0), new ( 1, 0), new ( 1,-1), new (0, 2), new ( 1, 2) },
        { new (0, 0), new (-1, 0), new (-1, 1), new (0,-2), new (-1,-2) },
        { new (0, 0), new ( 1, 0), new ( 1, 1), new (0,-2), new ( 1,-2) },
        { new (0, 0), new (-1, 0), new (-1,-1), new (0, 2), new (-1, 2) },
        { new (0, 0), new (-1, 0), new (-1,-1), new (0, 2), new (-1, 2) },
        { new (0, 0), new ( 1, 0), new ( 1, 1), new (0,-2), new ( 1,-2) },
    };

    public static readonly Dictionary<TetrominoShapes, Vector2Int[,]> WallKicks = new()
    {
        { TetrominoShapes.I, WallKicksI },
        { TetrominoShapes.J, WallKicksJ_L_O_S_T_Z },
        { TetrominoShapes.L, WallKicksJ_L_O_S_T_Z },
        { TetrominoShapes.O, WallKicksJ_L_O_S_T_Z },
        { TetrominoShapes.S, WallKicksJ_L_O_S_T_Z },
        { TetrominoShapes.T, WallKicksJ_L_O_S_T_Z },
        { TetrominoShapes.Z, WallKicksJ_L_O_S_T_Z },
    };
}
