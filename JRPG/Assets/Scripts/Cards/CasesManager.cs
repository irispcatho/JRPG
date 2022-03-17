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

            placedCards.OrderList.Add(go);
            OrderManagement();

            playerCanPlay = false;
        }


        if (!playerCanPlay)
        {
            StartCoroutine(WaitToPlay());
        }
    }

    public void RandomC()
    {
        int randomCardIndex = Random.Range(0, playerDeck.cardsIA.Count);
        int randomCellIndex = Random.Range(0, CasesList.Count);
        GameObject randomCard = playerDeck.parentIADeck.transform.GetChild(randomCardIndex).gameObject;

        while (placedCards.placedCardsList.Contains(randomCard) || CasesListUsed.Contains(CasesList[randomCellIndex]))
        {
            randomCardIndex = Random.Range(0, playerDeck.cardsIA.Count);
            randomCellIndex = Random.Range(0, CasesList.Count);
            randomCard = playerDeck.parentIADeck.transform.GetChild(randomCardIndex).gameObject;
        }

        TakeCard(randomCard, CasesList[randomCellIndex], randomCellIndex);
    }

    void TakeCard(GameObject card, GameObject randomCell, int randomCellIndex)
    {
        placedCards.placedCardsList.Add(card);
        Debug.Log(card);
        Vector2 positionC = randomCell.transform.position;
        CasesListUsed.Add(CasesList[randomCellIndex]);

        card.transform.position = positionC;
        CardDisplay display = card.GetComponent<CardDisplay>();
        visualCard = display.visual;
        visualCardOnCase = display.onCase;

        visualCard.SetActive(false);
        visualCardOnCase.SetActive(true);

        card.GetComponent<BoxCollider2D>().size = new Vector2(101.1319f, 98.74604f);
        Debug.Log("Carte IA placée");

        placedCards.OrderList.Add(card);
        OrderManagement();
    }

    IEnumerator WaitToPlay()
    {
        yield return new WaitForSeconds(1);
        RandomC();
        playerCanPlay = true;
    }

    private static int CompareCardOrder(GameObject cardone, GameObject cardtwo)
    {
        return (cardone.GetComponent<CardDisplay>().card.gameOrder < cardtwo.GetComponent<CardDisplay>().card.gameOrder) ? -1 : 1;
    }
    private void OrderManagement()
    {
        placedCards.OrderList.Sort(CompareCardOrder);
        Debug.Log("Cartes dans l'ordre");
    }
}

