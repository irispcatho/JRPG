using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickCard : MonoBehaviour
{
    public PlacedCards placedCards;
    private int count = 0;
    public PlayerDeck playerDeck;

    private void OnMouseDown()
    {
        if (count == 0)
        {
            count++;
            Debug.Log(this.gameObject.name + "is selected");
            placedCards.lastCardClicked = gameObject;
        }
    }
}
