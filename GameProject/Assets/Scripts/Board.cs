using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
    [SerializeField] public TetrominoData[] tetrominos;
    [SerializeField] public Vector3Int spawnPosition;
    public Tilemap Tilemap { get; private set; }
    public Piece CurrentPiece { get; private set; }

    private void Awake()
    {
        Tilemap = GetComponentInChildren<Tilemap>();
        CurrentPiece = GetComponentInChildren<Piece>();
        
        for(var i = 0; i < tetrominos.Length; i++)
        {
            tetrominos[i].Initialize();
        }
    }

    private void Start()
    {
        SpawnPiece();
    }

    private void SpawnPiece()
    {
        var shapeIndex = Random.Range(0, tetrominos.Length);
        CurrentPiece.Initialize(this, spawnPosition, tetrominos[shapeIndex]);
        UpdateCellsOccupiedByCurrentTetromino(CurrentPiece);
    }
    
    private void UpdateCellsOccupiedByCurrentTetromino(Piece piece)
    {
        Vector3Int currentOccupiedCell = new();
        
        foreach (var cell in piece.Cells)
        {
            currentOccupiedCell = cell + piece.Position;
            Tilemap.SetTile(currentOccupiedCell, piece.CurrentTetrominoData.tile);
        }
    }
    

}
