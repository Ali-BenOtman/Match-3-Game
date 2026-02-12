
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.Rendering;
using UnityEngine;

public class TileSwapper : MonoBehaviour
{
    private GameBoard gameBoard;
    private MatchChecker matchChecker;
    private BoardRefiller boardRefiller;

    void Start()
    {
        gameBoard = FindFirstObjectByType<GameBoard>();
        matchChecker = FindFirstObjectByType<MatchChecker>();
        boardRefiller = FindFirstObjectByType<BoardRefiller>();
    }
    // Swaps two tiles if they're adjacent
    public bool TrySwapTiles(Tile tile1, Tile tile2)
    {

        //First we check if there are moves left

        if (MoveManager.Instance.GetMoves()  <= 0)
        {
            Debug.Log("No moves left!! Cannot Swap!");
            return false;
        }
        //Check if tiles are next to each other
        if(!AreAdjacent(tile1, tile2))
        {
            Debug.Log("Tiles are not adjacent - cannot swap");
            return false;
        }

        //Perfrom the swap
        SwapTiles(tile1, tile2);
        return true;
    }

    //Check if tow tiles are next to each other (not diagonal)
    bool AreAdjacent(Tile tile1, Tile tile2)
    {
        int rowDiff = Mathf.Abs(tile1.row - tile2.row);
        int colDiff = Mathf.Abs(tile1.col - tile2.col);

        //Adjacent = same row and 1 column apart, OR same column and 1 row apart
        return (rowDiff == 1 && colDiff == 0) || (rowDiff == 0 &&  colDiff == 1);
    }

    //Actually swaps the tiles
    void SwapTiles(Tile tile1, Tile tile2)
    {

        MoveManager.Instance.UseMove();

        gameBoard.SwapTiles(tile1, tile2);

        // Check for matches after swapping 
        CheckAndRemoveMatches();

        
    }
    void CheckAndRemoveMatches()
    {
        List<Tile> matches = matchChecker.FindAllMatches();

        if (matches.Count > 0)
        {
            Debug.Log("Found " + matches.Count + " matching tiles!");

            //Add Score 
            ScoreManager.Instance.AddScore(matches.Count);

            // Remove all matched tiles
            foreach (Tile tile in matches)
            {
                gameBoard.RemoveTile(tile);
            }

            StartCoroutine(boardRefiller.RefillBoard());
        }
        else
        {
            Debug.Log("No matches found");
        }
    }

}
