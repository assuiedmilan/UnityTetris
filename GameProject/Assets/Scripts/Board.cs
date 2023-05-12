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

    private RectInt _boundaries; 

    private void Awake()
    {
        Tilemap = GetComponentInChildren<Tilemap>();
        CurrentPiece = GetComponentInChildren<Piece>();

        for (var i = 0; i < tetrominos.Length; i++)
        {
            tetrominos[i].Initialize();
        }

        var gridSize = visualGrid.GetComponent<SpriteRenderer>().size;
        var boardSize = new Vector2Int((int) gridSize.x, (int) gridSize.y);
        var boardLowerOrigin = new Vector2Int(
            -boardSize.x / 2,
            -boardSize.y / 2
            );
        _boundaries = new RectInt(boardLowerOrigin, boardSize);
    }

    private void Start()
    {
        SpawnPiece();
        /*SpawnDebugPiece();*/
    }

    public bool IsPositionInValid(Piece piece, Vector3Int newPieceCenterCoordinate)
    {
        // ReSharper disable once LoopCanBeConvertedToQuery: Performance
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var cell in piece.Cells)
        {
            var newOccupiedCell = cell + newPieceCenterCoordinate;

            if (IsCellPositionInValid(newOccupiedCell))
            {
                return true;
            }
        }

        return false;
    }

    private bool IsCellPositionInValid(Vector3Int cellToCheck)
    {
        return !_boundaries.Contains((Vector2Int)cellToCheck) ||;
    }
    
    private void SpawnPiece()
    {
        var shapeIndex = Random.Range(1, tetrominos.Length);
        CurrentPiece.Initialize(this, spawnPosition, tetrominos[shapeIndex]);
        DrawPiece(CurrentPiece);
    }
    
    /*private void SpawnDebugPiece()
    {
        var shapeIndex = tetrominos[0];
        var debugPiece = gameObject.AddComponent<Piece>();
        debugPiece.Initialize(this, new Vector3Int(0,0,0), tetrominos[0]);
        DrawPiece(debugPiece);
    }*/
    
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
