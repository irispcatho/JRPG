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
            //TakeACard(casenumber);
            enviedecrever();
            playerCanPlay = true;
        }
    }

    public void enviedecrever()
    {
        int randomCardIndex = Random.Range(0, playerDeck.cardsIA.Count);
        int randomCellIndex = Random.Range(0, CasesList.Count);
        GameObject randomCard = playerDeck.parentIADeck.transform.GetChild(randomCardIndex).gameObject;

        // fait une boucle pour verifier si la carte random est contenu dans la cellule random.
        // faire gaffe pck on peut facilement faire freeze unity et tomber sur un cas ou la liste
        // est trop petite pour qu'on puisse trouver un nombre random qui remplisse toute les conditions.
        while (placedCards.placedCardsList.Contains(randomCard) || CasesListUsed.Contains(CasesList[randomCellIndex]))
        {
            randomCardIndex = Random.Range(0, playerDeck.cardsIA.Count);
            randomCellIndex = Random.Range(0, CasesList.Count);
            randomCard = playerDeck.parentIADeck.transform.GetChild(randomCardIndex).gameObject;
        }

        TakeCard(randomCard, CasesList[randomCellIndex]);
    }

    void TakeCard(GameObject card, GameObject randomCell)
    {
        placedCards.placedCardsList.Add(card);
        Debug.Log(card);
        Vector2 positionC = randomCell.transform.position;

        card.transform.position = positionC;
        CardDisplay display = card.GetComponent<CardDisplay>();
        visualCard = display.visual;
        visualCardOnCase = display.onCase;

        visualCard.SetActive(false);
        visualCardOnCase.SetActive(true);

        card.GetComponent<BoxCollider2D>().size = new Vector2(101.1319f, 98.74604f);
        Debug.Log("Carte IA placée");
        Debug.Log("Alors mattéo il fait moins le malin la j'ai pas raison ? ;) ");
    }

    /*public void TakeACard(int casenumber)
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
    }*/

    private static int CompareCardOrder(GameObject cardone, GameObject cardtwo)
    {
        return (cardone.GetComponent<CardDisplay>().card.gameOrder < cardtwo.GetComponent<CardDisplay>().card.gameOrder) ? -1 : 1;
    }
    private void OrderManagement()
    {
        placedCards.OrderList.Sort(CompareCardOrder);
    }
}

