﻿using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HighlightTile : MonoBehaviour
{
    [Header("Tile Settings")]
    public Tilemap selectabletileMap;
    public Tilemap riverTileMap;
    public Tilemap groundTileMap;
    public TileBase treeTile;
    public TileBase treeStumpTile;
    public TileBase fallenLogTile;
    public TileBase[] fallenLogTiles;
    
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

        Vector3Int playerCellPos = selectabletileMap.layoutGrid.WorldToCell(playerPos.position);
        Vector3Int dirToFall = DetermineDirectionToFall(playerCellPos);

        Debug.Log("TILE: " + currentTilePos + ", PLAYER: " + selectabletileMap.layoutGrid.WorldToCell(playerPos.position) + ", FALLEN: " + dirToFall);
        selectabletileMap.SetTile(dirToFall, fallenLogTile);

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
        Vector3 worldCellPos = selectabletileMap.layoutGrid.CellToWorld(logTilePos);
        Vector3Int riverTilePos = groundTileMap.layoutGrid.WorldToCell(worldCellPos);
        Vector3Int baseTilePos = groundTileMap.layoutGrid.WorldToCell(worldCellPos);

        if (riverTileMap.HasTile(baseTilePos))
        {
            TileBase riverTile = riverTileMap.GetTile(riverTilePos);
            if (debug) Debug.Log("There is a tile (" + riverTile.name + ") at: " + riverTilePos);

            groundTileMap.SetTile(baseTilePos, riverTile);
            riverTileMap.SetTile(riverTilePos, null);
        }
        else
        {
            Debug.Log("Error changing tile");
        }
    }
}
