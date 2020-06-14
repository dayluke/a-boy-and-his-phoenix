using UnityEngine;
using UnityEngine.Tilemaps;

public class HighlightTile : MonoBehaviour
{
    public Tilemap tileMap;
    public Color highlightColor;
    public bool debug = false;
    private Vector3Int currentTilePos;
    private bool tileHighlighted = false;
    public GameObject firePS;
    public float treeBurnTime = 2f;

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
        Vector3Int tilePos = tileMap.layoutGrid.WorldToCell(worldPos);
        
        if (tileMap.HasTile(tilePos))
        {
            if (Input.GetMouseButtonDown(0) && tileHighlighted)
            {
                GameObject particles = Instantiate(firePS, worldPos, Quaternion.Euler(Vector3.left * 90f));
                Destroy(particles, treeBurnTime);
            }

            if (debug) Debug.Log(tileMap.GetTile(tilePos).name + ", Tile: " + tilePos + ", World: " + worldPos);

            currentTilePos = tilePos;

            tileMap.SetTileFlags(currentTilePos, TileFlags.None);
            tileMap.SetColor(currentTilePos, highlightColor);
            tileHighlighted = true;
        }
        else
        {
            if (tileHighlighted)
            {
                tileMap.SetColor(currentTilePos, Color.white);
                tileHighlighted = false;
            }
        }

    }
}
