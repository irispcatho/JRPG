using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseController : MonoBehaviour
{
    public PlacedCards placedCards;

    private void OnMouseDown()
    {
        if(placedCards.placedCardsList.Count > 0)
        {
            Debug.Log("click");
            placedCards.placedCardsList[0].SetActive(false);
        }
    }
}
