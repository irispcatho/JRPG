using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickCard : MonoBehaviour
{
    public bool isSelected = false;
    public PlacedCards placedCards;
    public GameObject thisCard;

    private void OnMouseDown()
    {
        isSelected = true;
        Debug.Log(this.gameObject.name + "is selected");
        placedCards.placedCards.Add(thisCard);
    }
}
