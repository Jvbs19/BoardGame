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

    [Header("UI")]
    [SerializeField]
    PlayerUIBehaviour PlayerUI;
    [SerializeField]
    GameObject GameOverUI;
    [SerializeField]
    GameObject LoadingPanelUI;

    bool gameOver;
    void Start()
    {
        StartCoroutine(SetupPlayers());
    }
    void Update()
    {
        if (!gameOver)
            if (currentPlayingPlayer != null)
                CheckUp();
    }
    void CheckUp()
    {
        if (Input.GetMouseButtonDown(0))
            MovimentCast();

        if (currentPlayingPlayer.GetUsingItem())
        {
            SetPlayerUI();
            currentPlayingPlayer.SetUsingItem(false);
        }
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

        for (int i = 0; i < playersInGame.Length; i++)
        {
            yield return new WaitUntil(playersInGame[i].GetComponent<PlayerBehaviour>().IsSetupCompleted);
        }

        if (playersInGame[1].GetComponent<PlayerBehaviour>().GetSpeed() > playersInGame[0].GetComponent<PlayerBehaviour>().GetSpeed())
        {
            currentPlayingPlayer = playersInGame[1].GetComponent<PlayerBehaviour>();
            playingNumber = 1;

            playersInGame[1].transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.red;
            playersInGame[0].transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        else
        {
            currentPlayingPlayer = playersInGame[0].GetComponent<PlayerBehaviour>();
            playingNumber = 0;

            playersInGame[0].transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.red;
            playersInGame[1].transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.blue;
        }

        SetPlayerUI();
        SwitchCameraTarget();

        LoadingPanelUI.SetActive(false);
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
                        {
                            PlayerBattle(tile.GetObjectInTile().GetComponent<PlayerBehaviour>());
                            return;
                        }

                    currentPlayingPlayer.MovePlayer(hit.transform);
                    SetPlayerUI();

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
        SetPlayerUI();
    }
    void PlayerBattle(PlayerBehaviour enemy)
    {
        if (currentPlayingPlayer.GetCanAttack())
        {
            Debug.Log("Battle Started");
            int[] currentPlayerDices = new int[currentPlayingPlayer.GetDices()];
            int[] enemyDices = new int[enemy.GetDices()];

            for (int i = 0; i < currentPlayerDices.Length; i++)
            {
                currentPlayerDices[i] = Random.Range(1, 20);
            }
            Sort(currentPlayerDices);

            for (int i = 0; i < enemyDices.Length; i++)
            {
                enemyDices[i] = Random.Range(1, 20);
            }
            Sort(enemyDices);

            ShowResult(currentPlayerDices);
            ShowResult(enemyDices);

            int currentPlayerWins = 0, enemyWins = 0;

            if (currentPlayerDices.Length > enemyDices.Length)
            {
                currentPlayerWins = currentPlayerDices.Length - enemyDices.Length;
                for (int i = 0; i < enemyDices.Length; i++)
                {
                    if (currentPlayerDices[i] >= enemyDices[i])
                        currentPlayerWins++;
                    else
                        enemyWins++;
                }
            }
            else
            {
                enemyWins = enemyDices.Length - currentPlayerDices.Length;
                for (int i = 0; i < enemyDices.Length; i++)
                {
                    if (currentPlayerDices[i] >= enemyDices[i])
                        currentPlayerWins++;
                    else
                        enemyWins++;
                }
            }

            if (enemyWins > currentPlayerWins)
                Damage(currentPlayingPlayer, enemy);
            else
                Damage(enemy, currentPlayingPlayer);

            currentPlayingPlayer.AddHits(-1);

            currentPlayingPlayer.AddMoviment(-1);

            SetPlayerUI();

            Debug.Log("Battle Ended");
            if (!currentPlayingPlayer.CanMovePlayer())
                EndTurn();
        }
    }
    void EndGame()
    {
        Debug.Log("Game Ended");
        GameOverUI.SetActive(true);
        gameOver = true;
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

    public void SetPlayerUI()
    {
        PlayerUI.SetupPlayerUI(currentPlayingPlayer.GetName(), currentPlayingPlayer.GetHP().ToString(), currentPlayingPlayer.GetDices().ToString(), currentPlayingPlayer.GetAtk().ToString(), currentPlayingPlayer.GetHits().ToString(), currentPlayingPlayer.GetMoviment().ToString(), currentPlayingPlayer.GetSpeed().ToString());
    }

    void Sort(int[] a)
    {
        for (int x = 0; x < a.Length - 1; x++)
        {
            for (int i = 0; i < a.Length - 1; i++)
            {
                if (a[i] < a[i + 1])
                {
                    int sup = a[i];
                    a[i] = a[i + 1];
                    a[i + 1] = sup;
                }
            }
        }
    }

    void Damage(PlayerBehaviour Target, PlayerBehaviour Attacker)
    {
        Target.DealDamage((int)Attacker.GetAtk());

        if (Target.GetIsDead() || Attacker.GetIsDead())
        {
            EndGame();
        }

    }
    void ShowResult(int[] a)
    {
        for (int i = 0; i < a.Length; i++)
        {
            Debug.Log("Roll " + a[i]);
        }
    }

    #endregion

}
