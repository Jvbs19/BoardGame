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
    GameObject playerPrefab;

    [Header("Parametrs")]
    [SerializeField]
    [Range(3, 999)]
    int mapWidth = 16;
    [SerializeField]
    [Range(3, 999)]
    int mapHeight = 16;

    float tileXOffset = 1.8f;
    float tileZOffset = 1.5f;

    List<GameObject> gridTiles;

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
            }
        }
        isGridComplete = true;
    }
}