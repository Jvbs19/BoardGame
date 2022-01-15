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

    public string GetPlayerName() { return Name; }
    public int GetPlayerHP() { return HP; }
    public float GetPlayerAttack() { return Attack; }
    public int GetPlayerMoviment() { return Moviment; }
    public float GetPlayerSpeed() { return Speed; }
}
