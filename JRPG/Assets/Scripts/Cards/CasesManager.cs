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
    private GameObject visualCardOnCaseIA;

    public float[,] casesPlacements;

    public void CaseIsClicker(int casenumber)
    {
        Vector2 position = CasesList[casenumber].transform.position;
        if (!placedCards.placedCardsList.Contains(placedCards.lastCardClicked) && playerCanPlay)
        {
            CasesListUsed.Add(CasesList[casenumber]);
            placedCards.placedCardsList.Add(placedCards.lastCardClicked);
            GameObject card = placedCards.lastCardClicked;
            card.GetComponent<OnMouseOverCard>().isPlaced = true;

            placedCards.lastCardClicked.transform.position = position;
            visualCard = card.GetComponent<CardDisplay>().visual;
            visualCardOnCase = card.GetComponent<CardDisplay>().onCase;
            card.GetComponent<BoxCollider2D>().size = new Vector2(101.1319f, 98.74604f);

            visualCard.SetActive(false);
            visualCardOnCase.SetActive(true);

            placedCards.OrderList.Add(card);
            OrderManagement();
            DetectCards(card.transform.position.x, card.transform.position.y);
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
        Vector2 positionC = randomCell.transform.position;
        CasesListUsed.Add(CasesList[randomCellIndex]);

        visualCardOnCaseIA = card.GetComponent<CardDisplay>().cardIA;
        visualCardOnCaseIA.SetActive(false);

        SpriteRenderer AnimalG = card.GetComponent<CardDisplay>().animalG;
        AnimalG.enabled = false;

        card.transform.position = positionC;
        CardDisplay display = card.GetComponent<CardDisplay>();
        visualCard = display.visual;
        visualCardOnCase = display.onCaseIA;

        visualCard.SetActive(false);
        visualCardOnCase.SetActive(true);

        card.GetComponent<BoxCollider2D>().size = new Vector2(101.1319f, 98.74604f);

        placedCards.OrderList.Add(card);
        OrderManagement();

        DetectCards(randomCell.transform.position.x, randomCell.transform.position.y);
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
    }

    public void DetectCards(float x, float y)
    {
        for (int i = 0; i < CasesListUsed.Count - 1; i++)
        {
            casesPlacements = new float[,] { { x, y } };
            Debug.Log(casesPlacements[0, 0]);
        }
    }
}

