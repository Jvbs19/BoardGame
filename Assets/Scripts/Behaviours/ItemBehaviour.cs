using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    [SerializeField]
    ItemStatus status;

    int myTier;
    int lifeBonus;
    int attackBonus;
    int hitBonus;
    int movimentBonus;
    float speedBonus;
    int diceBonus;

    float[] multy = new float[3] { 1, 1.5f, 2f };

    void Start() { SetupItem(); }

    public void UseItem(PlayerBehaviour player)
    {
        player.AddAtack(attackBonus);
        player.AddDices(diceBonus);
        player.AddHits(hitBonus);
        player.AddHP(lifeBonus);
        player.AddMoviment(movimentBonus);
        player.AddSpeed(speedBonus);

        Debug.Log("Player: " + player.name + " Used Item " + status.name);
        Destroy(this.gameObject);
    }
    public void SetupItem()
    {
        if (status.GetTier() == 3)
            myTier = Random.Range(0, 2);
        else
            myTier = status.GetTier();

        lifeBonus = (int)(status.GetLifeBonus() * multy[myTier]);
        attackBonus = (int)(status.GetLifeBonus() * multy[myTier]);
        hitBonus = (int)(status.GetHitBonus() * multy[myTier]);
        movimentBonus = (int)(status.GetMovimentBonus() * multy[myTier]);
        speedBonus = status.GetSpeedBonus() * multy[myTier];
        diceBonus = (int)(status.GetDiceBonus() * multy[myTier]);
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            PlayerBehaviour playerBehav = col.GetComponent<PlayerBehaviour>();
            playerBehav.SetUsingItem(true);
            UseItem(playerBehav);
        }
    }
}
