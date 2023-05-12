using UnityEngine;

public class Piece : MonoBehaviour
{
    public Board Board { get; private set; }
    public TetrominoData TetrominoData { get; private set; }
    public Vector3Int[] Cells { get; private set; }
    public Vector3Int CenterCoordinates { get; private set; }
    public int RotationIndex { get; private set; }
    
    public float automaticMovementDownStepDelay = 1f;
    private float _timeAtWhichAutomaticMovementDownOccurs;
    
    public void Initialize(Board board, Vector3Int position, TetrominoData data)
    {
        Board = board;
        CenterCoordinates = position;
        TetrominoData = data;
        RotationIndex = 0;
        
        Cells ??= new Vector3Int[TetrominoData.cells.Length];

        UpdateTimeAtWhichAutomaticMovementDownOccurs();
        
        SetCells();
    }

    private void Update()
    {
        Board.ClearPiece(this);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector2Int.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Vector2Int.right);
        }
        
        if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time > _timeAtWhichAutomaticMovementDownOccurs)
        {
            MoveDownOneRow();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Rotate(-1);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Rotate(1);
        }
        Board.DrawPiece(this);
    }

    private void MoveDownOneRow()
    {
        UpdateTimeAtWhichAutomaticMovementDownOccurs();
        Move(Vector2Int.down);
    }

    private bool Move(Vector2Int movement)
    {
        var newPosition = new Vector3Int(CenterCoordinates.x + movement.x, CenterCoordinates.y + movement.y, 0);
        
        if (Board.IsPositionInValid(this, newPosition))
        {
            return false;
        }

        CenterCoordinates = newPosition;
        return true;
    }

    private void Rotate(int direction)
    {
        var originalRotation = RotationIndex;
        RotationIndex = Wrap(RotationIndex + direction, 0, 4);
        ApplyRotationMatrix(direction);

        if (TestWallKicks(RotationIndex, direction)) return;
        
        RotationIndex = originalRotation;
        ApplyRotationMatrix(-direction);
    }
    
    private bool TestWallKicks(int rotationIndex, int rotationDirection)
    {
        var wallKickIndex = GetWallKickIndex(rotationIndex, rotationDirection);

        for (var i = 0; i < TetrominoData.WallKicks.GetLength(1); i++)
        {
            var translation = TetrominoData.WallKicks[wallKickIndex, i];

            if (Move(translation)) {
                return true;
            }
        }

        return false;
    }

    private int GetWallKickIndex(int rotationIndex, int rotationDirection)
    {
        var wallKickIndex = rotationIndex * 2;

        if (rotationDirection < 0) {
            wallKickIndex--;
        }

        return Wrap(wallKickIndex, 0, TetrominoData.WallKicks.GetLength(0));
    }
    
    private void ApplyRotationMatrix(int direction)
    {
        var matrix = TetrominosCoordinates.RotationMatrix;

        for (var i = 0; i < Cells.Length; i++)
        {
            Vector3 cell = Cells[i];

            int x, y;

            switch (TetrominoData.tetromino)
            {
                case TetrominoShapes.I:
                case TetrominoShapes.O:
                    // "I" and "O" are rotated from an offset center point
                    cell.x -= 0.5f;
                    cell.y -= 0.5f;
                    x = Mathf.CeilToInt((cell.x * matrix[0] * direction) + (cell.y * matrix[1] * direction));
                    y = Mathf.CeilToInt((cell.x * matrix[2] * direction) + (cell.y * matrix[3] * direction));
                    break;

                case TetrominoShapes.T:
                case TetrominoShapes.J:
                case TetrominoShapes.L:
                case TetrominoShapes.S:
                case TetrominoShapes.Z:
                default:
                    x = Mathf.RoundToInt((cell.x * matrix[0] * direction) + (cell.y * matrix[1] * direction));
                    y = Mathf.RoundToInt((cell.x * matrix[2] * direction) + (cell.y * matrix[3] * direction));
                    break;
            }

            Cells[i] = new Vector3Int(x, y, 0);
        }
    }
    
    private int Wrap(int input, int min, int max)
    {
        if (input < min) {
            return max - (min - input) % (max - min);
        } else {
            return min + (input - min) % (max - min);
        }
    }
    
    private void UpdateTimeAtWhichAutomaticMovementDownOccurs()
    {
        _timeAtWhichAutomaticMovementDownOccurs = Time.time + automaticMovementDownStepDelay;
    }
    
    private void SetCells()
    {
        for(var i = 0; i < Cells.Length; i++)
        {
            Cells[i] = (Vector3Int) TetrominoData.cells[i];
        }
    }

}
