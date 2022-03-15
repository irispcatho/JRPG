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

        if (!playerCanPlay)
        {
            TakeACard(casenumber);
            playerCanPlay = true;
        }
    }
    public void TakeACard(int casenumber)
    {
        int rnd = Random.Range(0, playerDeck.cardsIA.Count);
        GameObject card = playerDeck.parentIADeck.transform.GetChild(rnd).gameObject;
        if (!placedCards.placedCardsList.Contains(card))
        {
            placedCards.placedCardsList.Add(card);
            Debug.Log(card);
            int rndC = Random.Range(0, CasesList.Count);
            Vector2 positionC = CasesList[rndC].transform.position;
            if(!CasesListUsed.Contains(CasesList[rndC]))
            {
                card.transform.position = positionC;
                visualCard = card.GetComponent<CardDisplay>().visual;
                visualCardOnCase = card.GetComponent<CardDisplay>().onCase;

                visualCard.SetActive(false);
                visualCardOnCase.SetActive(true);

                card.GetComponent<BoxCollider2D>().size = new Vector2(101.1319f, 98.74604f);
                Debug.Log("Carte IA placée");
            }
            else
                TakeACard(casenumber);
        }
        else
            TakeACard(casenumber);
    }

    private static int CompareCardOrder(GameObject cardone, GameObject cardtwo)
    {
        return (cardone.GetComponent<CardDisplay>().card.gameOrder < cardtwo.GetComponent<CardDisplay>().card.gameOrder) ? -1 : 1;
    }
    private void OrderManagement()
    {
        placedCards.OrderList.Sort(CompareCardOrder);
    }
}

