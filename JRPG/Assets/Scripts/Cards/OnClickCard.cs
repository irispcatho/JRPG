using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickCard : MonoBehaviour
{
    public PlacedCards placedCards;

    private void OnMouseDown()
    {
        Debug.Log(this.gameObject.name + "is selected");
        placedCards.lastCardClicked = gameObject;
    }
}
