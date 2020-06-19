using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AbilityHandler : MonoBehaviour
{
    [Header("Tile Settings")]
    public Tilemap selectableTileMap;
    public Tilemap riverTileMap;
    public Tilemap noCollRiverTileMap;
    public Tilemap noCollGroundTileMap;

    [Header("Burn Tile Settings")]
    public TileBase treeTile;
    public TileBase treeStumpTile;
    public TileBase fallenLogTile;
    public TileBase[] fallenLogTiles;

    [Header("Regen Tile Settings")]
    public TileBase saplingTile;

    [Header("Highlighted Tile Settings")]
    public Color highlightColor;
    public GameObject firePS;
    public GameObject growPS;
    public float treeBurnTime = 2f;

    [Header("Extra Settings")]
    public Transform playerPos;
    public Animator playerAnimator;
    public bool debug = false;

    private Vector3Int currentTilePos;
    private bool tileHighlighted = false;

    private void Update()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0;
        Vector3Int tilePos = selectableTileMap.layoutGrid.WorldToCell(worldPos);
        
        if (selectableTileMap.HasTile(tilePos))
        {
            if (debug) Debug.Log(selectableTileMap.GetTile(tilePos).name + ", Tile: " + tilePos + ", World: " + worldPos);

            if (tileHighlighted)
            {
                selectableTileMap.SetColor(currentTilePos, Color.white);
                tileHighlighted = false;
            }

            currentTilePos = tilePos;
            HighlightTile();

            if (tileHighlighted)
            {
                if (Input.GetMouseButtonDown(0) && selectableTileMap.GetTile(currentTilePos) == treeTile)
                {
                    Vector3Int playerCellPos = selectableTileMap.layoutGrid.WorldToCell(playerPos.position);
                    if (currentTilePos.x == playerCellPos.x || currentTilePos.y == playerCellPos.y)
                    {
                        playerAnimator.SetTrigger("isBurningTree");
                        BurnTree(worldPos);
                    }
                    else
                    {
                        Debug.Log("Player is not adjacent or on same axis as tile");
                        Debug.Log("Player: " + playerCellPos + ", Cell: " + currentTilePos);
                    }
                }
                else if (Input.GetMouseButtonDown(1) && selectableTileMap.GetTile(currentTilePos) == saplingTile)
                {
                    playerAnimator.SetTrigger("isGrowingTree");
                    GrowSapling(worldPos);
                }
            }
            
        }
        else
        {
            if (tileHighlighted)
            {
                selectableTileMap.SetColor(currentTilePos, Color.white);
                tileHighlighted = false;
            }
        }

    }

    private void HighlightTile()
    {
        if (!tileHighlighted)
        {
            selectableTileMap.SetTileFlags(currentTilePos, TileFlags.None);
            selectableTileMap.SetColor(currentTilePos, highlightColor);
            tileHighlighted = true;
        }
    }

    #region burn-ability

    private async void BurnTree(Vector3 pos)
    {
        GameObject particles = Instantiate(firePS, pos, Quaternion.Euler(Vector3.left * 90f));
        Destroy(particles, treeBurnTime);
        await Task.Delay(TimeSpan.FromSeconds(treeBurnTime / 2f));
        ChangeTileSprite();
    }

    private void ChangeTileSprite()
    {
        noCollGroundTileMap.SetTile(currentTilePos, treeStumpTile);
        selectableTileMap.SetTile(currentTilePos, null);

        Vector3Int playerCellPos = selectableTileMap.layoutGrid.WorldToCell(playerPos.position);
        Vector3Int dirToFall = DetermineDirectionToFall(playerCellPos);

        Debug.Log("TILE: " + currentTilePos + ", PLAYER: " + selectableTileMap.layoutGrid.WorldToCell(playerPos.position) + ", FALLEN: " + dirToFall);
        noCollGroundTileMap.SetTile(dirToFall, fallenLogTile);

        MakeGroundTileWalkable(dirToFall);
    }

    private Vector3Int DetermineDirectionToFall(Vector3Int player)
    {
        if (currentTilePos.x == player.x)
        {
            fallenLogTile = fallenLogTiles[0];
            if (currentTilePos.y > player.y)
            {
                return currentTilePos + Vector3Int.up; 
            }
            else
            {
                return currentTilePos + Vector3Int.down; 
            }
        }
        else if (currentTilePos.y == player.y)
        {
            fallenLogTile = fallenLogTiles[1];
            if (currentTilePos.x > player.x)
            {
                return currentTilePos + Vector3Int.right; 
            }
            else
            {
                return currentTilePos + Vector3Int.left; 
            }
        }

        throw new NullReferenceException();
    }

    private void MakeGroundTileWalkable(Vector3Int logTilePos)
    {
        Vector3 worldCellPos = selectableTileMap.layoutGrid.CellToWorld(logTilePos);
        Vector3Int riverTilePos = noCollRiverTileMap.layoutGrid.WorldToCell(worldCellPos);
        Vector3Int baseTilePos = noCollRiverTileMap.layoutGrid.WorldToCell(worldCellPos);

        if (riverTileMap.HasTile(baseTilePos))
        {
            TileBase riverTile = riverTileMap.GetTile(riverTilePos);
            if (debug) Debug.Log("There is a tile (" + riverTile.name + ") at: " + riverTilePos);

            noCollRiverTileMap.SetTile(baseTilePos, riverTile);
            riverTileMap.SetTile(riverTilePos, null);
        }
        else
        {
            Debug.Log("Error changing tile");
        }
    }

    #endregion

    #region regen-ability

    private async void GrowSapling(Vector3 psPos)
    {
        GameObject particles = Instantiate(growPS, psPos, Quaternion.Euler(Vector3.left * 90f));
        Destroy(particles, treeBurnTime);
        await Task.Delay(TimeSpan.FromSeconds(treeBurnTime / 2f));
        selectableTileMap.SetTile(currentTilePos, treeTile);
    }

    #endregion
}
