using UnityEngine;

public class Piece : MonoBehaviour
{
    public Board Board { get; private set; }
    public TetrominoData CurrentTetrominoData { get; private set; }
    public Vector3Int[] Cells { get; private set; }
    public Vector3Int CenterCoordinates { get; private set; }

    public float automaticMovementDownStepDelay = 1f;
    private float _timeAtWhichAutomaticMovementDownOccurs;
    
    public void Initialize(Board board, Vector3Int position, TetrominoData data)
    {
        Board = board;
        CenterCoordinates = position;
        CurrentTetrominoData = data;
        Cells ??= new Vector3Int[CurrentTetrominoData.cells.Length];

        UpdateTimeAtWhichAutomaticMovementDownOccurs();
        
        SetCells();
    }

    private void Update()
    {
        /*if (Time.time > _timeAtWhichAutomaticMovementDownOccurs)
        {
            MoveDownOneRow();
        }*/

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector2Int.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Vector2Int.right);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(Vector2Int.down);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(Vector2Int.up);
        }
    }

    private void MoveDownOneRow()
    {
        UpdateTimeAtWhichAutomaticMovementDownOccurs();
        Move(Vector2Int.down);
    }

    private void Move(Vector2Int movement)
    {
        var newPosition = new Vector3Int(CenterCoordinates.x + movement.x, CenterCoordinates.y + movement.y, 0);

        if (Board.IsPositionInValid(this, newPosition))
        {
            return;
        }

        Board.ClearPiece(this);
        CenterCoordinates = newPosition;
        Board.DrawPiece(this);
        
    }

    private void UpdateTimeAtWhichAutomaticMovementDownOccurs()
    {
        _timeAtWhichAutomaticMovementDownOccurs = Time.time + automaticMovementDownStepDelay;
    }
    
    private void SetCells()
    {
        for(var i = 0; i < Cells.Length; i++)
        {
            Cells[i] = (Vector3Int) CurrentTetrominoData.cells[i];
        }
    }

}
