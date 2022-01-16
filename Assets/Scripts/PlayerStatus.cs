using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatus", menuName = "Status/PlayerStatus")]
public class PlayerStatus : ScriptableObject
{
    [SerializeField]
    string Name;
    [SerializeField]
    int HP;
    [SerializeField]
    float Attack;
    [SerializeField]
    int Moviment;
    [SerializeField]
    float Speed;
    [SerializeField]
    int Dices;
    [SerializeField]
    int Hits;

    public string GetPlayerName() { return Name; }
    public float GetPlayerSpeed() { return Speed; }
    public float GetPlayerAttack() { return Attack; }
    public int GetPlayerHP() { return HP; }
    public int GetPlayerMoviment() { return Moviment; }
    public int GetPlayerHits() { return Hits; }
    public int GetPlayerDices() { return Dices; }
}
