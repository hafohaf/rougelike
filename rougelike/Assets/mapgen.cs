using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class mapgen : MonoBehaviour
{
    public Grid grid;
    public Tilemap tileMap;
    public TileBase bottomTile;
    public TileBase groudTile;
    public int maxsize=0;
    public int maxwolker=10;

    private int currentxsize;
    private int currentysize;
    void Start()
    {
        GenerateMaparea();
        Randomwolker();
    }

    void GenerateMaparea()
    {
        currentxsize=Random.Range(10,maxsize+1);
        currentysize=Random.Range(10,maxsize+1);
        for(int x=0;x<currentxsize;x++)
        {
            for(int y=0;y<currentysize;y++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);
                Vector3 worldPosition = grid.CellToWorld(cellPosition);
                tileMap.SetTile(cellPosition, bottomTile);
            }
        }
    }
    void Randomwolker()
    {
        int currentwolker= Random.Range(5,maxwolker+1);
        //Debug.Log($"随机currentwolker: {currentwolker}");
        Vector3Int[] directions = new Vector3Int[]
    {
        Vector3Int.up,
        Vector3Int.down,
        Vector3Int.left,
        Vector3Int.right
    };
        for(int x=0;x<currentwolker;x++)
        {
            Vector3Int stratposition = new Vector3Int (Random.Range(0,maxsize + 1),Random.Range(0,maxsize + 1),0);
            
            Vector3Int currtpos=stratposition;
           // Debug.Log($"随机stratpos: {currtpos}");

            int currschritt= Random.Range(2,9);
            for(int y=0;y<currschritt;y++)
            {
                Vector3Int dir= directions[Random.Range(0,directions.Length)];
                currtpos+=dir;
                currtpos.x = Mathf.Clamp(currtpos.x, 0, currentxsize - 1);
                currtpos.y = Mathf.Clamp(currtpos.y, 0, currentysize - 1);
                tileMap.SetTile(currtpos, null);
                tileMap.SetTile(currtpos, groudTile);
            }

        }
        
        


    }

  
}
