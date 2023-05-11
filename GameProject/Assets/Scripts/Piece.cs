using UnityEngine;

public class Piece : MonoBehaviour
{
    public Board Board { get; private set; }
    public TetrominoData CurrentTetrominoData { get; private set; }
    public Vector3Int[] Cells { get; private set; }
    public Vector3Int Position { get; private set; }

    public void Initialize(Board board, Vector3Int position, TetrominoData data)
    {
        Board = board;
        Position = position;
        CurrentTetrominoData = data;
        Cells ??= new Vector3Int[CurrentTetrominoData.cells.Length];

        SetCells();
    }

    private void SetCells()
    {
        for(var i = 0; i < Cells.Length; i++)
        {
            Cells[i] = (Vector3Int) CurrentTetrominoData.cells[i];
        }
    }

}
