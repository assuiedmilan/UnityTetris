using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
    [SerializeField] public TetrominoData[] tetrominos;
    [SerializeField] public Vector3Int spawnPosition;
    [SerializeField] public GameObject visualGrid;
    
    public Tilemap Tilemap { get; private set; }
    public Piece CurrentPiece { get; private set; }
    public Vector2Int BoardSize { get; private set; }
    private Vector2Int BoardLowerOrigin { get; set; }
    
    public RectInt Boundaries => new(BoardLowerOrigin, BoardSize);

    private void Awake()
    {
        Tilemap = GetComponentInChildren<Tilemap>();
        CurrentPiece = GetComponentInChildren<Piece>();

        for (var i = 0; i < tetrominos.Length; i++)
        {
            tetrominos[i].Initialize();
        }

        var gridSize = visualGrid.GetComponent<SpriteRenderer>().size;
        BoardSize = new Vector2Int((int) gridSize.x, (int) gridSize.y);
        BoardLowerOrigin = new Vector2Int(
            -BoardSize.x / 2,
            -BoardSize.y / 2
            );
    }

    private void Start()
    {
        SpawnPiece();
    }

    public bool IsPositionInValid(Piece piece, Vector3Int newPiecePosition)
    {
        // ReSharper disable once LoopCanBeConvertedToQuery: Performance
        foreach (var cell in piece.Cells)
        {
            var newOccupiedCell = cell + piece.CenterCoordinates;

            if (IsCellPositionInValid(newOccupiedCell))
            {
                return true;
            }
        }

        return false;
    }

    private bool IsCellPositionInValid(Vector3Int cellToCheck)
    {
        return !Boundaries.Contains((Vector2Int)cellToCheck) || Tilemap.HasTile(cellToCheck);
    }
    
    private void SpawnPiece()
    {
        var shapeIndex = Random.Range(1, tetrominos.Length);
        shapeIndex = 0;
        CurrentPiece.Initialize(this, spawnPosition, tetrominos[shapeIndex]);
        DrawPiece(CurrentPiece);
    }
    
    public void ClearPiece(Piece piece)
    {
        UpdateTilemap(piece, null);

    }
    
    public void DrawPiece(Piece piece)
    {
        UpdateTilemap(piece, piece.CurrentTetrominoData.tile);
    }

    private void UpdateTilemap(Piece piece, TileBase tile)
    {
        foreach (var cell in piece.Cells)
        {
            var currentOccupiedCell = cell + piece.CenterCoordinates;
            Tilemap.SetTile(currentOccupiedCell, tile);
        }
    }
}
