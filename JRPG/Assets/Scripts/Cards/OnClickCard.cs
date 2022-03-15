using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickCard : MonoBehaviour
{
    public PlacedCards placedCards;

    private void OnMouseDown()
    {
        placedCards.lastCardClicked = gameObject;
    }
}
