using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CasesManager : MonoBehaviour
{
    public List<GameObject> CasesList;
    public PlacedCards placedCards;

    public void CaseIsClicker(int casenumber)
    {
        Vector2 position = CasesList[casenumber].transform.position;
        placedCards.lastCardClicked.transform.position = position;
        if(!placedCards.placedCardsList.Contains(placedCards.lastCardClicked))
            placedCards.placedCardsList.Add(placedCards.lastCardClicked);
    }
}
