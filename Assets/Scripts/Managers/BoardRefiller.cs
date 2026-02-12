using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardRefiller : MonoBehaviour
{
    private GameBoard gameBoard;
    private MatchChecker matchChecker;
    private bool isProcessing = false;

    void Start()
    {
        gameBoard = FindFirstObjectByType<GameBoard>();
        matchChecker = FindFirstObjectByType<MatchChecker>();  
    }
    // main refill process 
    public IEnumerator RefillBoard()
    {
        isProcessing = true;

        // Step 1 : Drop existing tiles down
        yield return StartCoroutine(DropTiles());

        // Step 2 : Spawn new tiles at the top
        yield return StartCoroutine(SpawnNewTiles());

        // Step 3: Check for new matches (cascade)
        yield return StartCoroutine(CheckForCascadeMatches());

        isProcessing = false;
    }

    // Makes tiles fall down to fill gaps
    IEnumerator DropTiles()
    {
        bool tilesMoved = true;

        // Keep dropping until no more tiles can move
        while (tilesMoved)
        {
            tilesMoved = false;

            // Check each column from bottom to top
            for (int col = 0; col < gameBoard.width ; col++)
            {
                for (int row = 0; row < gameBoard.height ; row++)
                {
                    // If this spot is empty
                    if (gameBoard.GetTileAt(col, row) == null)
                    {
                        // Look for a tile above it 
                        for (int rowAbove = row + 1; rowAbove < gameBoard.height; rowAbove++)
                        {
                            Tile tileAbove = gameBoard.GetTileAt(col, rowAbove);

                            if (tileAbove != null)
                            {
                                //Move the tile down
                                gameBoard.MoveTile(tileAbove, col, row);
                                tilesMoved = true;
                                break;
                            }
                        }
                    } 
                }
            }

            yield return new WaitForSeconds(0.1f); // Small delay for visual effect
        }
    }

    // Spawn new tiles at the top
    IEnumerator SpawnNewTiles()
    {
        for (int col = 0; col < gameBoard.width; col++)
        {
            for (int row = 0; row < gameBoard.height; row++)
            {
                if (gameBoard.GetTileAt(col, row) == null)
                {
                    // Spawn a new tile at this position
                    gameBoard.SpawnTileAt(col, row);
                }
            }
        }

        yield return new WaitForSeconds(0.2f);
    }
    // Check for matches after tiles fall; basically combos
    IEnumerator CheckForCascadeMatches()
    {
        List<Tile> matches = matchChecker.FindAllMatches();

        if (matches.Count > 0)
        {
            Debug.Log("Cascade!!! Found " +  matches.Count + " more matches !!!");

           // Add score with combo bonus!!!!
           ScoreManager.Instance.AddScore(matches.Count);
            
            //Remove Matches tiless

            foreach (Tile tile in matches)
            {
                gameBoard.RemoveTile(tile);
            }
            yield return new WaitForSeconds(0.3f);

            //Refill again (recursive can create chains!!!)
            yield return StartCoroutine(RefillBoard());
        }
    }

    public bool IsProcessing()
    {
        return isProcessing;
    }
}

    

