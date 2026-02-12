using UnityEngine;

//Tile.cs
//This represents a single tile on the game board
public class Tile : MonoBehaviour 
{
    //FIELDS - what does tile know about itself
    public int row;     // Which row am in? (0-7)
    public int col;     // Which column am in? (0-7)
    public TileType tileType;   //What color am I?

    //METHODS - what can a tile do?

    //Initialize sets up the tile when it's created
    public void Initialize(int newRow, int newColumn, TileType newType)
    {
        row = newRow;
        col = newColumn;
        tileType = newType;

        // Set the tile's position in the game world
        transform.position = new Vector3(newColumn, row, 0);
    }

    public void PrintTileInfo()
    {
        Debug.Log("Tile at (" + row + "," +  col + ") is " + tileType);
    }
}
