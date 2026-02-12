using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UIElements;

// GameBoard.cs
// This manages the entire game board and all tiles

public class GameBoard : MonoBehaviour 
{
    //SERIALIZED FIELDS - these appear in Unity Inspector so we can adjust them
    [Header("Board Settings")]
    [SerializeField] public int width = 8;  // Board is 8 tiles wide
    [SerializeField] public int height = 8; // Board is 8 tiles tall

    [Header("Tile Prefabs")]
    [SerializeField] private GameObject redTilePrefab;
    [SerializeField] private GameObject blueTilePrefab;
    [SerializeField] private GameObject greenTilePrefab;
    [SerializeField] private GameObject yellowTilePrefab;
    [SerializeField] private GameObject purpleTilePrefab;
    //[SerializeField] private GameObject tilePrefab;  //The tile we'll copy to make 

    //PRIVATE FIELDS - only this class needs to know about these 
    private Tile[,] tiles; //2D array to store all tiles

    //Start is called when the game begins
    void Start()
    {
        CreateBoard(); 
    }

    //Creates the entire board
    void CreateBoard()
    {
        //Create the 2D array to hold our tiles
        tiles = new Tile[width, height];

        //Loop through each position on the board
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                //Create a tile at this postion
                CreateTile(x, y);
            }
        }
    }

    //Creates a single tile at a specific postion
    void CreateTile(int x, int y)
    {
        //Choose a random color for this tile
        TileType randomType = GetRandomTileType();
        GameObject prefabToUse = GetPrefabForType(randomType);

        //Creates a GameObject from our tilePrefab
        GameObject tileObject = Instantiate(prefabToUse, transform);

        //Get the Tile Component from it 
        Tile tile = tileObject.GetComponent<Tile>();

        //Set up the tile with its position and color 
        tile.Initialize(y, x, randomType);

        //Store it in out 2D array
        tiles[x, y] = tile;

        //Name it in the hierarchy for debugging
        tileObject.name = "Tile( " + x + ", " + y + ")";
    }
    //Picks a random tile color
    TileType GetRandomTileType()
    {
        //Random.Range (0,5) gives us 0, 1, 2, 3, or 4
        int randomIndex = Random.Range(0, 5);

        //Convert the number to a TileType
        return (TileType)randomIndex;
    }
    GameObject GetPrefabForType(TileType type)
    {
        switch (type)
        {
            case TileType.Red:
                return redTilePrefab;
            case TileType.Green:
                return greenTilePrefab;
            case TileType.Blue:
                return blueTilePrefab;
            case TileType.Purple:
                return purpleTilePrefab;
            case TileType.Yellow:
                return yellowTilePrefab;
            default:
                return redTilePrefab;
        }
    }
    public void SwapTiles(Tile tile1, Tile tile2)
    {
        //Get their positions in the array
        int tile1X = tile1.col;
        int tile1Y = tile1.row;
        int tile2X = tile2.col;
        int tile2Y = tile2.row;

        //Swap theire positions in the array
        tiles[tile1X, tile1Y] = tile2;
        tiles[tile2X, tile2Y] = tile1;

        //Swap their row and column values
        tile1.row = tile2Y;
        tile1.col = tile2X;
        tile2.row = tile1Y;
        tile2.col = tile1X;

        //Move them visually on screen
        tile1.transform.position = new Vector3(tile1.col, tile1.row, 0);
        tile2.transform.position = new Vector3(tile2.col, tile2.row, 0);

        Debug.Log("Swapped Tiles!!!");
    }
    //This makes width and height accesible
    

    // Get a tile at a specific position

    public Tile GetTileAt(int col, int row)
    {
        if (col >= 0 && col < width && row >= 0 && row < height)
        {
            return tiles[col, row];
        }
        return null;
    }

    // Remove a tile from the board 
    public void RemoveTile(Tile tile)
    {
        if (tiles != null)
        {
            tiles[tile.col, tile.row] = null;
            Destroy(tile.gameObject);
        }
    }

    // Move a tile to a new postion 
    public void MoveTile(Tile tile, int newCol, int newRow)
    {
        // Remove from old position
        tiles[tile.col, tile.row] = null;

        // Update tiles data
        tile.col = newCol;
        tile.row = newRow;

        // Place in new position
        tiles[newCol, newRow] = tile;

        // Move visually
        tile.transform.position = new Vector3(newCol, newRow, 0);
    }

    // Spawn a new tile at a specific position
    public void SpawnTileAt(int col, int row)
    {
        TileType randomType = GetRandomTileType();
        GameObject prefabToUse = GetPrefabForType(randomType);

        GameObject tileObject = Instantiate (prefabToUse, transform);
        Tile tile = tileObject.GetComponent<Tile>();

        tile.Initialize(row, col, randomType);
        tiles[col, row] = tile;
        tileObject.name = "Tile (" + col + ", " + row + ")";
    }
}
