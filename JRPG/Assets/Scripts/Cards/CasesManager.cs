using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CasesManager : MonoBehaviour
{
    public PlacedCards placedCards;
    public CardDisplay cardDisplay;
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
            visualCard = go.GetComponent<CardDisplay>().visual;
            visualCardOnCase = go.GetComponent<CardDisplay>().onCase;
            order = go.GetComponent<CardDisplay>().card.gameOrder;

            //order = onClickCard.order;

            placedCards.orderPlacedCardsList.Add(order);

            visualCard.SetActive(false);
            visualCardOnCase.SetActive(true);
            
            if (placedCards.placedCardsList.Count >= 6)
            {
                Debug.Log("le compte est bon");
                canPlay = true;
                //int min_value = placedCards.orderPlacedCardsList.AsQueryable().Min();
            }
            else
                canPlay = false;

            //List<OnClickCard> SortedList = placedCards.placedCardsList.OrderBy(o => o.).ToList();
            //for (int i = 0; i < SortedList.Count; i++)
            //{
            //    Debug.Log(SortedList[i].order);
            //}


        }
    }
}
