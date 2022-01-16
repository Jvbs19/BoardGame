using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Collectable", menuName = "Itens/Collectables")]
public class ItemStatus : ScriptableObject
{
    public enum Tier { Low, Medium, High, Random};

    [SerializeField]
    Tier myTier;
    [SerializeField]
    int LifeBonus;
    [SerializeField]
    int HitBonus;
    [SerializeField]
    int MovimentBonus;
    [SerializeField]
    float SpeedBonus;
    [SerializeField]
    int DiceBonus;

    public string GetTierName() { return System.Enum.GetName(typeof(Tier), myTier); }
    public int GetTier() { return (int)myTier; }
    public int GetLifeBonus() { return LifeBonus; }
    public int GetHitBonus() { return HitBonus; }
    public int GetMovimentBonust() { return MovimentBonus; }
    public float GetSpeedBonus() { return SpeedBonus; }
    public int GetDiceBonus() { return DiceBonus; }
}
