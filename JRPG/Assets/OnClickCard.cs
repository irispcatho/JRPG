using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickCard : MonoBehaviour
{
    public bool isSelected = false;
    public PlacedCards placedCards;
    public PlayerDeck playerDeck;
    public GameObject thisCard;
    private int count = 0;

    private void OnMouseDown()
    {
        Debug.Log(this.gameObject.name + "is selected");
        if(count == 0)
        {
            count++;
            placedCards.placedCardsList.Add(thisCard);
            thisCard.SetActive(false);
        }
    }
}
