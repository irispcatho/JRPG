using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CasesManager : MonoBehaviour
{
    public PlacedCards placedCards;
    //public CardDisplay cardDisplay;
    public OnClickCard onClickCard;

    public List<GameObject> CasesList;

    public Card card;
    public bool canPlay = false;
    public int order;
    private GameObject visualCard;
    private GameObject visualCardOnCase;

    public void CaseIsClicker(int casenumber)
    {
        Vector2 position = CasesList[casenumber].transform.position;
        if(!placedCards.placedCardsList.Contains(placedCards.lastCardClicked))
        {
            placedCards.lastCardClicked.transform.position = position;
            placedCards.placedCardsList.Add(placedCards.lastCardClicked);

            GameObject go = placedCards.lastCardClicked;
            placedCards.OrderList.Add(go);
            visualCard = go.GetComponent<CardDisplay>().visual;
            visualCardOnCase = go.GetComponent<CardDisplay>().onCase;

            visualCard.SetActive(false);
            visualCardOnCase.SetActive(true);
            
            if (placedCards.placedCardsList.Count >= 6)
            {
                Debug.Log("le compte est bon");
                canPlay = true;
                OrderManagement();
                //int min_value = placedCards.orderPlacedCardsList.AsQueryable().Min();
            }
            else
                canPlay = false;
        }
    }
    private static int CompareCardOrder(GameObject cardone, GameObject cardtwo)
    {
        return (cardone.GetComponent<CardDisplay>().card.gameOrder < cardtwo.GetComponent<CardDisplay>().card.gameOrder) ? -1 : 1;
    }
    private void OrderManagement()
    {
        placedCards.OrderList.Sort(CompareCardOrder);
        for (int i = 0; i < placedCards.OrderList.Count; i++)
        {
            Debug.Log($"card {placedCards.OrderList[i].name} order : {placedCards.OrderList[i].GetComponent<CardDisplay>().card.gameOrder} addded to order list");
        }
    }

}

