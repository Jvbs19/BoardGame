using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject objectInTile;
    [SerializeField]
    List<GameObject> adjacentTiles;
    [SerializeField]
    [Range(0.1f, 10)]
    float castDistance = 1.2f;
    [SerializeField]
    bool playerStart;
    void Start()
    {
        StartCoroutine(FindAjacents());
    }

    #region Colision
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            PlayerBehaviour playerBehav = col.GetComponent<PlayerBehaviour>();
            objectInTile = col.transform.gameObject;
            playerBehav.SetMyCurrentTile(this.transform.gameObject);
        }
        if (col.tag == "Item")
        {
            ItemBehaviour itemBehav = col.GetComponent<ItemBehaviour>();
            objectInTile = col.transform.gameObject;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            objectInTile = null;
            PlayerBehaviour playerBehav = col.GetComponent<PlayerBehaviour>();
            playerBehav.SetMyCurrentTile(null);
        }
    }
    #endregion

    #region Adjacent
    IEnumerator FindAjacents()
    {
        yield return new WaitForSeconds(1f);
        CastToAdjacent();
    }
    void CastToAdjacent()
    {
        Vector3 N = transform.TransformDirection(Vector3.forward);
        Vector3 S = transform.TransformDirection(-Vector3.forward);
        Vector3 E = transform.TransformDirection(Vector3.right);
        Vector3 W = transform.TransformDirection(-Vector3.right);
        Vector3 NE = (N + E) / 2;
        Vector3 NW = (N + W) / 2;
        Vector3 SE = (S + E) / 2;
        Vector3 SW = (S + W) / 2;

        CastRayAtDirection(transform.position, N, castDistance);
        CastRayAtDirection(transform.position, S, castDistance);
        CastRayAtDirection(transform.position, E, castDistance);
        CastRayAtDirection(transform.position, W, castDistance);

        CastRayAtDirection(transform.position, NE, castDistance * 2);
        CastRayAtDirection(transform.position, NW, castDistance * 2);

        CastRayAtDirection(transform.position, SE, castDistance * 2);
        CastRayAtDirection(transform.position, SW, castDistance * 2);
    }
    void CastRayAtDirection(Vector3 start, Vector3 end, float distance)
    {
        RaycastHit hit;
        if (Physics.Raycast(start, end, out hit, distance))
        {
            if (hit.transform.tag == "Tile")
            {
                if (!adjacentTiles.Contains(hit.transform.gameObject))
                {
                    adjacentTiles.Add(hit.transform.gameObject);
                }
            }
        }
    }
    #endregion

    public GameObject GetObjectInTile() { return objectInTile; }
    public bool isAdjacent(GameObject tile) 
    {
        if (adjacentTiles.Contains(tile))
            return true;
        else
            return false;
    }
    public void SetPlayerStart(bool v) { playerStart = v; }
    public bool IsStartPosition() { return playerStart; }
}
