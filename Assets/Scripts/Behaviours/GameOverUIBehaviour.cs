using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUIBehaviour : MonoBehaviour
{
    [SerializeField]
    TMP_Text winnerText;

    public void ShowWinner(bool isRed) 
    {
        if (isRed)
            winnerText.text = "Winner is Red";
        else
            winnerText.text = "Winner is Blue";
    }
}
