using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickCard : MonoBehaviour
{
    public bool isSelected = false;
    public PlacedCards placedCards;
    public GameObject thisCard;
    public CaseController caseController;
    private int count = 0;
    public bool hasCard = false;

    private void OnMouseDown()
    {
        Debug.Log(this.gameObject.name + "is selected");
        if (count == 0)
        {
            count++;
            hasCard = true;
            placedCards.placedCardsList.Add(thisCard);
        }
    }
}
