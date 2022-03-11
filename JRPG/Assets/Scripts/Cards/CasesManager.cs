using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CasesManager : MonoBehaviour
{
    public List<GameObject> CasesList;
    public PlacedCards placedCards;
    public bool canPlay = false;

    public void CaseIsClicker(int casenumber)
    {
        Vector2 position = CasesList[casenumber].transform.position;
        if(!placedCards.placedCardsList.Contains(placedCards.lastCardClicked))
        {
            placedCards.lastCardClicked.transform.position = position;
            placedCards.placedCardsList.Add(placedCards.lastCardClicked);

            if (placedCards.placedCardsList.Count >= 6)
            {
                Debug.Log("le compte est bon");
                canPlay = true;
            }
            else
                canPlay = false;
        }
    }
}
