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
    private GameObject visual;
    private GameObject onCase;
    public void CaseIsClicker(int casenumber)
    {
        Vector2 position = CasesList[casenumber].transform.position;
        if(!placedCards.placedCardsList.Contains(placedCards.lastCardClicked))
        {
            placedCards.lastCardClicked.transform.position = position;
            placedCards.placedCardsList.Add(placedCards.lastCardClicked);

            CasesList[casenumber].SetActive(false);

            GameObject go = placedCards.lastCardClicked;
            visual = go.GetComponent<CardDisplay>().visual;
            onCase = go.GetComponent<CardDisplay>().onCase;
            order = go.GetComponent<CardDisplay>().card.gameOrder;

            placedCards.orderPlacedCardsList.Add(order);
            int min_value = placedCards.orderPlacedCardsList.AsQueryable().Min();
            Debug.Log(min_value);

            visual.SetActive(false);
            onCase.SetActive(true);
            
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
