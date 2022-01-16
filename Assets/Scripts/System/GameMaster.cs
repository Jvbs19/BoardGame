using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] GridGenerator gridGenerator;
    [SerializeField] Camera cam;
    [SerializeField] GameObject[] playersInGame;
    [SerializeField] LayerMask mouseCastLayer;

    [Header("Spawn")]
    [SerializeField]
    GameObject playerPrefab;

    Transform[] startingPos;

    PlayerBehaviour currentPlayingPlayer;
    int MaxPlayerInGame = 2;

    int playingNumber;
    void Start()
    {
        StartCoroutine(SetupPlayers());
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            MovimentCast();
    }

    #region Game Actions
    IEnumerator SetupPlayers()
    {
        yield return new WaitUntil(gridGenerator.IsGridComplete);

        startingPos = gridGenerator.GetPlayerStartPositions();

        for (int i = 0; i < MaxPlayerInGame; i++)
        {
            GameObject player = Instantiate(playerPrefab);
            playerPrefab.transform.position = new Vector3(startingPos[i].position.x, playerPrefab.transform.position.y, startingPos[i].position.z);
        }

        playersInGame = GameObject.FindGameObjectsWithTag("Player");

        if (playersInGame[1].GetComponent<PlayerBehaviour>().GetSpeed() > playersInGame[0].GetComponent<PlayerBehaviour>().GetSpeed())
        {
            currentPlayingPlayer = playersInGame[1].GetComponent<PlayerBehaviour>();
            playingNumber = 1;
        }
        else
        {
            currentPlayingPlayer = playersInGame[0].GetComponent<PlayerBehaviour>();
            playingNumber = 0;
        }

        SwitchCameraTarget();
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
                TileBehaviour tile = hit.transform.GetComponent<TileBehaviour>();

                if (tile.isAdjacent(currentPlayingPlayer.GetMyCurrentTile()))
                {
                    if (tile.GetObjectInTile() != null)
                        if (tile.GetObjectInTile().tag == "Player")
                            return;

                    currentPlayingPlayer.MovePlayer(hit.transform);

                    if (!currentPlayingPlayer.CanMovePlayer())
                        EndTurn();
                }
            }
        }
    }
    void EndTurn()
    {
        Debug.Log("Turn Ended");
        ChangeCurrentPlayer();
        SwitchCameraTarget();
    }
    #endregion

    #region Assistents
    public void SwitchCameraTarget()
    {
        cam.GetComponent<CameraFollow>().SwitchTarget(currentPlayingPlayer.transform);
    }

    public void ChangeCurrentPlayer()
    {
        playingNumber++;
        if (playingNumber > 1)
            playingNumber = 0;

        currentPlayingPlayer = playersInGame[playingNumber].GetComponent<PlayerBehaviour>();
        currentPlayingPlayer.ResetTurnStatus();
    }
    #endregion

}
