using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HighlightTile : MonoBehaviour
{
    [Header("Tile Settings")]
    public Tilemap selectabletileMap;
    public TileBase treeTile;
    public TileBase treeStumpTile;
    public TileBase fallenLogTile;
    
    [Header("Highlighted Tile Settings")]
    public Color highlightColor;
    public GameObject firePS;
    public float treeBurnTime = 2f;

    [Header("Extra Settings")]
    public Transform playerPos;
    public bool debug = false;

    private Vector3Int currentTilePos;
    private bool tileHighlighted = false;

    /*
    private void Start()
    {
        for (int x = -3; x < Screen.width; x++)
        {
            for (int y = -3; y < Screen.height; y++)
            {
                Vector3Int pos = new Vector3Int(x,y,0);
                Vector3Int tilePos = tileMap.layoutGrid.WorldToCell(pos);
                if (tileMap.HasTile(tilePos)) Debug.Log(tileMap.GetTile(tilePos).name + ", Tile: " + tilePos + ", World: " + pos);
            }
        }
    }
    */

    private void Update()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0;
        Vector3Int tilePos = selectabletileMap.layoutGrid.WorldToCell(worldPos);
        
        if (selectabletileMap.HasTile(tilePos))
        {
            if (debug) Debug.Log(selectabletileMap.GetTile(tilePos).name + ", Tile: " + tilePos + ", World: " + worldPos);

            currentTilePos = tilePos;
            ChangeTileColour();

            if (Input.GetMouseButtonDown(0) && tileHighlighted && selectabletileMap.GetTile(currentTilePos) == treeTile)
            {
                Vector3Int playerCellPos = selectabletileMap.layoutGrid.WorldToCell(playerPos.position);
                if (currentTilePos.x == playerCellPos.x || currentTilePos.y == playerCellPos.y)
                {
                    BurnTree(worldPos);
                }
                else
                {
                    Debug.Log("Player is not adjacent or on same axis as tile");
                }
            }
        }
        else
        {
            if (tileHighlighted)
            {
                selectabletileMap.SetColor(currentTilePos, Color.white);
                tileHighlighted = false;
            }
        }

    }

    private void ChangeTileColour()
    {
        selectabletileMap.SetTileFlags(currentTilePos, TileFlags.None);
        selectabletileMap.SetColor(currentTilePos, highlightColor);
        tileHighlighted = true;
    }

    private async void BurnTree(Vector3 pos)
    {
        GameObject particles = Instantiate(firePS, pos, Quaternion.Euler(Vector3.left * 90f));
        Destroy(particles, treeBurnTime);
        await Task.Delay(TimeSpan.FromSeconds(treeBurnTime / 2f));
        ChangeTileSprite();
    }

    private void ChangeTileSprite()
    {
        selectabletileMap.SetTile(currentTilePos, treeStumpTile);

        Vector3Int dirToFall = Vector3Int.zero;
        Vector3Int playerCellPos = selectabletileMap.layoutGrid.WorldToCell(playerPos.position);

        if (currentTilePos.x == playerCellPos.x)
        {
            if (currentTilePos.y > playerCellPos.y)
            {
                dirToFall = currentTilePos + Vector3Int.up; 
            }
            else
            {
                dirToFall = currentTilePos + Vector3Int.down; 
            }
        }
        else if (currentTilePos.y == playerCellPos.y)
        {
            if (currentTilePos.x > playerCellPos.x)
            {
                dirToFall = currentTilePos + Vector3Int.right; 
            }
            else
            {
                dirToFall = currentTilePos + Vector3Int.left; 
            }
        }

        Debug.Log("TILE: " + currentTilePos + ", PLAYER: " + selectabletileMap.layoutGrid.WorldToCell(playerPos.position) + ", FALLEN: " + dirToFall);
        selectabletileMap.SetTile(dirToFall, fallenLogTile);
    }
}
