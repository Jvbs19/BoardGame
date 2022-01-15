using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    PlayerStatus status;


    string charName;
    int hp;
    float attack;
    int maxMoviment, movimentsLeft;
    float speed;

    GameObject myTile;

    bool hasAttacked;
    void Start() { StatusSetup(); }

    void Update()
    {

    }

    #region Setup
    void StatusSetup()
    {
        charName = status.GetPlayerName();
        hp = status.GetPlayerHP();
        maxMoviment = status.GetPlayerMoviment();
        speed = status.GetPlayerSpeed();
        attack = status.GetPlayerAttack();

        movimentsLeft = maxMoviment;
    }
    public void SetMyCurrentTile(GameObject tile) { myTile = tile; }
    public GameObject GetMyCurrentTile() { return myTile; }

    #endregion

    #region PlayerAction
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

}
