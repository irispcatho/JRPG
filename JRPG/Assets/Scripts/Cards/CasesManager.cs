using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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

    public Vector2Int gridSize = new Vector2Int(4, 4);
    public CaseSlot[,] allCases;
    private int caseNextTo;

    private void Start()
    {
        GetCellArray();
    }

    void GetCellArray()
    {
        allCases = new CaseSlot[gridSize.x, gridSize.y];
        Queue<CaseSlot> cellQueue = new Queue<CaseSlot>(GetComponentsInChildren<CaseSlot>()); // on fait une queue a partir des cases récupérées 

        for (int y = gridSize.y - 1; y >= 0; y--)
        {
            for (int x = 0; x < gridSize.x; x++)
            { // on ajoute la case dans le tableau selon les coordonées

                CaseSlot cell = cellQueue.Dequeue(); //ça nous permet de récup le dernier élément ajouté dans la queue,
                                                     //pratique quand on veut recup les éléments d'une liste dans le bonne ordre et la vider en même temps
                if (cell)
                {
                    allCases[x, y] = cell;
                    cell.coordinates = new Vector2Int(x, y);
                }
                else
                    return;
            }
        }
    }

    public void CaseIsClicker(int casenumber)
    {
        GameObject cell = CasesList[casenumber];
        Vector2 position = cell.transform.position;
        if (!placedCards.placedCardsList.Contains(placedCards.lastCardClicked) && playerCanPlay)
        {
            CasesListUsed.Add(cell);
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
            playerCanPlay = false;

            CaseSlot slot = cell.GetComponent<CaseSlot>();
            slot.card = card.GetComponent<CardDisplay>().card;
            if (caseNextTo != 1)
            {
                CaseSlot rightCell = GetCellOnGrid(slot.coordinates.x + 1, slot.coordinates.y); // droite
                if (rightCell.card)
                    print("Carte à droite");

            }

            if (caseNextTo != 2)
            {
                CaseSlot leftCell = GetCellOnGrid(slot.coordinates.x - 1, slot.coordinates.y); // gauche
                if (leftCell.card)
                    print("Carte à gauche");
            }

            if (caseNextTo != 3)
            {
                CaseSlot upCell = GetCellOnGrid(slot.coordinates.x, slot.coordinates.y - 1); // haut
                if (upCell.card)
                    print("Carte en haut");
            }

            if (caseNextTo != 3)
            {
                CaseSlot downCell = GetCellOnGrid(slot.coordinates.x, slot.coordinates.y + 1); // bas
                if (downCell.card)
                    print("Carte en bas");
            }
            

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


        CaseSlot slot = randomCell.GetComponent<CaseSlot>();
        slot.card = card.GetComponent<CardDisplay>().card;
        if (caseNextTo != 1)
        {
            CaseSlot rightCell = GetCellOnGrid(slot.coordinates.x + 1, slot.coordinates.y); // droite
            if (rightCell.card)
                print("Carte à droite");
        }

        if (caseNextTo != 2)
        {
            CaseSlot leftCell = GetCellOnGrid(slot.coordinates.x - 1, slot.coordinates.y); // gauche
            if (leftCell.card)
                print("Carte à gauche");
        }

        if (caseNextTo != 3)
        {
            CaseSlot upCell = GetCellOnGrid(slot.coordinates.x, slot.coordinates.y - 1); // haut
            if (upCell.card)
                print("Carte en haut");
        }

        if (caseNextTo != 4)
        {
            CaseSlot downCell = GetCellOnGrid(slot.coordinates.x, slot.coordinates.y + 1); // bas
            if (downCell.card)
                print("Carte en bas");
        }
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

    public CaseSlot GetCellOnGrid(int x, int y)
    {
        if (x >= gridSize.x)
        {
            caseNextTo = 1;
            return null;
        }

        if (x < 0)
        {
            caseNextTo = 2;
            return null;
        }

        if (y >= gridSize.y)
        {
            caseNextTo = 3;
            return null;
        }

        if (y < 0)
        {
            caseNextTo = 4;
            return null;
        }

        caseNextTo = 0;
        return allCases[x, y];
    }
}
