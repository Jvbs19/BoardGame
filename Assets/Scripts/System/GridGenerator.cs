using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    Transform Grid;
    [SerializeField]
    GameObject tilePrefab;
    [SerializeField]
    List<GameObject> gridTiles;

    [Header("Parametrs")]
    [SerializeField]
    [Range(3, 999)]
    int mapWidth = 16;
    [SerializeField]
    [Range(3, 999)]
    int mapHeight = 16;

    float tileXOffset = 1.8f;
    float tileZOffset = 1.5f;

    bool isGridComplete;
    void Start()
    {
        CreateTileMap();
    }
    void Update()
    {

    }
    void CreateTileMap()
    {
        for (int x = 0; x <= mapWidth; x++)
        {
            for (int z = 0; z <= mapHeight; z++)
            {
                GameObject tile = Instantiate(tilePrefab);

                tile.name = "Tile[" + x + " , " + z + "]";
                tile.transform.parent = Grid;

                if (z % 2 == 0)
                    tile.transform.position = new Vector3(x * tileXOffset, 0, z * tileZOffset);
                else
                    tile.transform.position = new Vector3(x * tileXOffset + tileXOffset / 2, 0, z * tileZOffset);

                if ((x == 0 && z == 0) || (x == mapWidth && z == mapHeight))
                {
                    tile.GetComponent<TileBehaviour>().SetPlayerStart(true);
                    tile.name += " START";
                }
                else
                {
                    int v = Random.Range(1, 6);
                    if (v == 1 || v == 6)
                    {
                        tile.GetComponent<TileBehaviour>().SetSpawnItem(true);
                    }
                }

                gridTiles.Add(tile);
            }
        }
        isGridComplete = true;
    }
    public bool IsGridComplete() { return isGridComplete; }
    public Transform[] GetPlayerStartPositions()
    {
        Transform[] startPos = new Transform[2];
        int i = 0;
        foreach (GameObject tile in gridTiles)
        {
            if (tile.GetComponent<TileBehaviour>().IsStartPosition())
            {
                startPos[i] = tile.transform;
                i++;
            }
        }

        return startPos;
    }

    public TileBehaviour[] SpawnTiles() 
    {
        int i = 0;
        foreach (GameObject tile in gridTiles)
        {
            if (tile.GetComponent<TileBehaviour>().CanSpawnItem())
            {
                i++;
            }
        }
        TileBehaviour[] tiles = new TileBehaviour[i];
        int j = 0;
        foreach (GameObject tile in gridTiles)
        {
            if (tile.GetComponent<TileBehaviour>().CanSpawnItem())
            {
                tiles[j] = tile.GetComponent<TileBehaviour>();
                j++;
            }
        }
        return tiles;
    }
}
