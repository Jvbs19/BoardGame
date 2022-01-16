using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUIBehaviour : MonoBehaviour
{
    [SerializeField]
    TMP_Text nameText, hpText, dicesText, atkText, hitText, movimentText, speedText;
    public void SetupPlayerUI(string name, string hp, string dice, string atk, string hit, string moviment, string speed)
    {
        nameText.text = name;
        hpText.text = hp;
        dicesText.text = dice;
        atkText.text = atk;
        hitText.text = hit;
        movimentText.text = moviment;
        speedText.text = speed;
    }
}
