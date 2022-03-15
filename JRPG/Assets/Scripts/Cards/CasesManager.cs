using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CasesManager : MonoBehaviour
{
    public PlacedCards placedCards;
    public PlayerDeck playerDeck;

    public List<GameObject> CasesList;
    public List<GameObject> CasesListUsed;
    public bool playerCanPlay = true;
    private GameObject visualCard;
    private GameObject visualCardOnCase;

    public void CaseIsClicker(int casenumber)
    {
        Vector2 position = CasesList[casenumber].transform.position;
        if (!placedCards.placedCardsList.Contains(placedCards.lastCardClicked) && playerCanPlay)
        {
            CasesListUsed.Add(CasesList[casenumber]);
            placedCards.lastCardClicked.transform.position = position;
            placedCards.placedCardsList.Add(placedCards.lastCardClicked);
            GameObject go = placedCards.lastCardClicked;
            visualCard = go.GetComponent<CardDisplay>().visual;
            visualCardOnCase = go.GetComponent<CardDisplay>().onCase;
            go.GetComponent<BoxCollider2D>().size = new Vector2(101.1319f, 98.74604f);

            visualCard.SetActive(false);
            visualCardOnCase.SetActive(true);

            if (placedCards.placedCardsList.Count >= 12)
            { 
                for (int i = 0; i < 12; i++)
                {
                    placedCards.OrderList.Add(go);
                }
                OrderManagement();
            }
            playerCanPlay = false;
        }

        if(!playerCanPlay)
        {
            Debug.Log("IA turn");
            int rnd = Random.Range(0, playerDeck.cardsIA.Count);
            GameObject card = playerDeck.parentIADeck.transform.GetChild(rnd).gameObject;
            Debug.Log(card);
        }
    }

    private static int CompareCardOrder(GameObject cardone, GameObject cardtwo)
    {
        return (cardone.GetComponent<CardDisplay>().card.gameOrder < cardtwo.GetComponent<CardDisplay>().card.gameOrder) ? -1 : 1;
    }
    private void OrderManagement()
    {
        placedCards.OrderList.Sort(CompareCardOrder);
        //for (int i = 0; i < placedCards.OrderList.Count; i++)
        //{
        //    Debug.Log($"card {placedCards.OrderList[i].name} order : {placedCards.OrderList[i].GetComponent<CardDisplay>().card.gameOrder} addded to order list");
        //}
    }
}

