using System.Collections.Generic;
using UnityEngine;

public class MatchChecker : MonoBehaviour
{
    private GameBoard gameBoard;

    private void Start()
    {
        gameBoard = FindFirstObjectByType<GameBoard>();
    }

    //Find all matches on the board
    public List<Tile> FindAllMatches()
    {
        List<Tile> matchedTiles = new List<Tile>();

        //Check horizonal matches
        matchedTiles.AddRange(FindHorizontalMatches());

        //Check vertical matches
        matchedTiles.AddRange(FindVerticalMatches());

        //Remove duplicates (Duplicate tiles can be in both horizontal and vertical matches)
        matchedTiles = RemoveDuplicates(matchedTiles);

        return matchedTiles;
    }

    //Find horizontal matches (rows)
    List<Tile> FindHorizontalMatches()
    {
        List<Tile> matches = new List<Tile>();

        for (int row = 0; row < gameBoard.height; row++)
        {
            for (int col = 0; col < gameBoard.width - 2; col++)
            {
                Tile tile1 = gameBoard.GetTileAt(col, row);
                Tile tile2 = gameBoard.GetTileAt(col + 1, row);
                Tile tile3 = gameBoard.GetTileAt(col + 2, row);

                if (tile1 != null && tile2 != null && tile3 != null)
                {
                    if (tile1.tileType == tile2.tileType && tile2.tileType == tile3.tileType)
                    {
                        //Found a match of 3!!!
                        if (!matches.Contains(tile1)) matches.Add(tile1);
                        if (!matches.Contains(tile2)) matches.Add(tile2);
                        if (!matches.Contains(tile3)) matches.Add(tile3);
                    }
                }
            }
        }

        return matches;
    }

    //Find Vertical Matches (columns)

    List<Tile> FindVerticalMatches()
    {
        List<Tile> matches = new List<Tile>();

        for (int col = 0; col < gameBoard.width; col++)
        {
            for (int row = 0; row < gameBoard.height - 2; row++)
            {
                Tile tile1 = gameBoard.GetTileAt(col, row);
                Tile tile2 = gameBoard.GetTileAt(col, row + 1);
                Tile tile3 = gameBoard.GetTileAt(col, row + 2);

                if (tile1!= null && tile2 != null && tile3 != null)
                {
                    if (tile1.tileType == tile2.tileType && tile2.tileType == tile3.tileType)
                    {
                        // Found a match of 3

                        if (!matches.Contains(tile1)) matches.Add(tile1);
                        if (!matches.Contains(tile2)) matches.Add(tile2);
                        if (!matches.Contains(tile3)) matches.Add(tile3);
                    }
                }
            }
        }
        return matches;
    }
    //Remove duplicate tiles from the list
    List<Tile> RemoveDuplicates(List<Tile> tiles)
    {
        List<Tile> unique = new List<Tile>();

        foreach (Tile tile in tiles)
        {
            if (!unique.Contains(tile))
                {
            unique.Add(tile);
            }
        }
        return unique;
    }
}
