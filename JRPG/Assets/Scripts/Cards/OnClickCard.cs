using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickCard : MonoBehaviour
{
    public PlacedCards placedCards;
    public int order;

    private void OnMouseDown()
    {
        placedCards.lastCardClicked = gameObject;
    }
}
