using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Tile selectedTile;
    private TileSwapper tileSwapper;
    private Camera mainCamera;

    void Start()
    {
        tileSwapper = GetComponent<TileSwapper>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TrySelectTile();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (selectedTile != null)
            {
                TrySwapWithSelected();
            }
        }
    }

    void TrySelectTile()
    {
        Tile clickedTile = GetTileAtMousePosition();

        if (clickedTile != null)
        {
            selectedTile = clickedTile;
            Debug.Log("Selected tile at (" + clickedTile.row + ", " + clickedTile.col + ")");
        }
    }

    void TrySwapWithSelected()
    {
        Tile releasedTile = GetTileAtMousePosition();

        Debug.Log("Mouse released - checking for swap");

        if (releasedTile != null && releasedTile != selectedTile)
        {
            bool swapped = tileSwapper.TrySwapTiles(selectedTile, releasedTile);

            if (swapped)
            {
                Debug.Log("Successfully swapped tiles!");
            }
        }
        else
        {
            Debug.Log("Released on same tile or no tile found");
        }

            selectedTile = null;
    }

    Tile GetTileAtMousePosition()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null)
        {
            return hit.collider.GetComponent<Tile>();
        }

        return null;
    }
}