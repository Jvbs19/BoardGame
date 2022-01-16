using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] GameObject[] playersInGame;
    [SerializeField] PlayerBehaviour currentPlayingPlayer;
    [SerializeField] LayerMask mouseCastLayer; 
    void Start()
    {
        playersInGame = GameObject.FindGameObjectsWithTag("Player");
        currentPlayingPlayer = playersInGame[0].GetComponent<PlayerBehaviour>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            MovimentCast();
    }

    void MovimentCast()
    {
        if (!currentPlayingPlayer.CanMovePlayer())
        {
            EndTurn();
            return;
        }

        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))//, mouseCastLayer))
        {
            if (hit.transform.tag == "Tile")
            {
                Debug.Log("Hit tile");
                TileBehaviour tile = hit.transform.GetComponent<TileBehaviour>();

                if (tile.isAdjacent(currentPlayingPlayer.GetMyCurrentTile()))
                {
                    if (tile.GetObjectInTile() != null)
                        if (tile.GetObjectInTile().tag == "Player")
                            return;
                        
                    currentPlayingPlayer.MovePlayer(hit.transform);
                    Debug.Log("Player Moved");
                }
            }
        }
    }
    void EndTurn()
    {
        Debug.Log("Turn Ended");
        cam.GetComponent<CameraFollow>().SwitchTarget(currentPlayingPlayer.transform);
    }
}
