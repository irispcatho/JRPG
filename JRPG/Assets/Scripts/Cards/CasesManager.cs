using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CasesManager : MonoBehaviour
{
    public PlacedCards placedCards;
    public List<GameObject> CasesList;
    public bool canPlay = false;
    public int order;
    public void CaseIsClicker(int casenumber)
    {
        Vector2 position = CasesList[casenumber].transform.position;
        if(!placedCards.placedCardsList.Contains(placedCards.lastCardClicked))
        {
            placedCards.lastCardClicked.transform.position = position;
            placedCards.placedCardsList.Add(placedCards.lastCardClicked);

            GameObject go = placedCards.lastCardClicked;
            order = go.GetComponent<CardDisplay>().card.gameOrder;

            placedCards.orderPlacedCardsList.Add(order);

            int min_value = placedCards.orderPlacedCardsList.AsQueryable().Min();
            Debug.Log(min_value);

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
