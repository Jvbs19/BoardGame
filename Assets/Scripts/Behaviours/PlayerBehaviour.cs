using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    PlayerStatus status;

    #region CharacterStatus

    string charName;
    int maxHP, currentHP;
    float maxAttack, currentAttack;
    int maxMoviment, movimentsLeft;
    float maxSpeed, currentSpeed;
    int maxDices, currentDices;
    int maxHits, currentHits;

    #endregion

    GameObject myTile;

    bool hasAttacked, isDead;

    void Start() { StatusSetup(); }

    void Update()
    {

    }

    #region Setup
    void StatusSetup()
    {
        charName = status.GetPlayerName();
        maxHP = status.GetPlayerHP();
        maxMoviment = status.GetPlayerMoviment();
        maxSpeed = status.GetPlayerSpeed();
        maxAttack = status.GetPlayerAttack();
        maxDices = status.GetPlayerDices();
        maxHits = status.GetPlayerHits();

        ResetAllGameStatus();
    }
    public void SetMyCurrentTile(GameObject tile) { myTile = tile; }
    public GameObject GetMyCurrentTile() { return myTile; }
    #endregion

    #region Player Action
    public bool CanMovePlayer()
    {
        if (movimentsLeft > 0)
        {
            Debug.Log("Can Still Move");
            return true;
        }
        else
        {
            Debug.Log("Cannot Move Anymore");
            return false;
        }
    }
    public void MovePlayer(Transform Direction)
    {
        movimentsLeft--;

        this.transform.position = new Vector3(Direction.position.x, transform.position.y, Direction.position.z);
    }

    public void ResetMoviment()
    {
        movimentsLeft = maxMoviment;
    }

    #endregion

    #region Status Control

    #region Reset
    public void ResetAllGameStatus()
    {
        currentHP = maxHP;
        movimentsLeft = maxMoviment;
        currentSpeed = maxSpeed;
        currentAttack = maxAttack;
        currentDices = maxDices;
        currentHits = maxHits;
    }
    public void ResetTurnStatus() 
    {
        movimentsLeft = maxMoviment;
        currentSpeed = maxSpeed;
        currentAttack = maxAttack;
        currentDices = maxDices;
        currentHits = maxHits;
    }
    public void ResetPlayerMoviment()
    {
        movimentsLeft = maxMoviment;
    }
    #endregion

    #region ADD
    public void AddHP(int i)
    {
        currentHP += i;

        if (currentHP > maxHP)
            currentHP = maxHP;
    }
    public void AddMoviment(int i)
    {
        movimentsLeft += i;
    }
    public void AddSpeed(float i)
    {
        currentSpeed += i;
    }
    public void AddAtack(int i)
    {
        currentAttack += i;
    }
    public void AddDices(int i)
    {
        currentDices += i;
    }
    public void AddHits(int i)
    {
        currentHits += i;
    }
    #endregion

    #endregion

}