using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CasesManager : MonoBehaviour
{
    public static CasesManager instance;
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

    private void Awake()
    {
        instance = this;
    }

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
            card.GetComponent<CardDisplay>().card.cell = slot;
            //DetectCards(slot);
        }

        if (!playerCanPlay)
        {
            StartCoroutine(WaitToPlay());
        }
    }
    public void DetectCardLeft(CaseSlot slot)
    {
        CaseSlot leftCell = GetCellOnGrid(slot.coordinates.x - 1, slot.coordinates.y); // gauche
        if (leftCell)
        {
            if (leftCell.card)
                print("Carte à gauche");
        }
    }
    public void DetectCardRight(CaseSlot slot)
    {
        CaseSlot rightCell = GetCellOnGrid(slot.coordinates.x + 1, slot.coordinates.y); // droite
        if (rightCell)
        {
            if (rightCell.card)
                print("Carte à droite");
        }
    }
    public void DetectCardUp(CaseSlot slot)
    {
        CaseSlot upCell = GetCellOnGrid(slot.coordinates.x, slot.coordinates.y + 1); // haut
        if (upCell)
        {
            if (upCell.card)
                print("Carte en haut");
        }
    }
    public void DetectCardDown(CaseSlot slot)
    {
        CaseSlot downCell = GetCellOnGrid(slot.coordinates.x, slot.coordinates.y - 1); // bas
        if (downCell)
        {
            if (downCell.card)
                print("Carte en bas");
        }
    }
    public void DetectCardDR(CaseSlot slot)
    {
        CaseSlot diagDRCell = GetCellOnGrid(slot.coordinates.x + 1, slot.coordinates.y - 1);
        if (diagDRCell)
        {
            if (diagDRCell.card)
                print("Carte en diagonale en bas à droite");
        }
    }
    public void DetectCardDL(CaseSlot slot)
    {
        CaseSlot diagDLCell = GetCellOnGrid(slot.coordinates.x - 1, slot.coordinates.y - 1);
        if (diagDLCell)
        {
            if (diagDLCell.card)
                print("Carte en diagonale en bas à gauche");
        }
    }
    public void DetectCardUR(CaseSlot slot)
    {
        CaseSlot diagURCell = GetCellOnGrid(slot.coordinates.x + 1, slot.coordinates.y + 1);
        if (diagURCell)
        {
            if (diagURCell.card)
                print("Carte en diagonale en haut à droite");
        }
    }
    public void DetectCardUL(CaseSlot slot)
    {
        CaseSlot diagULCell = GetCellOnGrid(slot.coordinates.x - 1, slot.coordinates.y + 1);
        if (diagULCell)
        {
            if (diagULCell.card)
                print("Carte en diagonale en haut à gauche");
        }
    }
                     
    public void DetectCardDL2(CaseSlot slot)
    {
        CaseSlot diagDL2Cell = GetCellOnGrid(slot.coordinates.x - 2, slot.coordinates.y - 2);
        if (diagDL2Cell)
        {
            if (diagDL2Cell.card)
                print("Carte en diagonale en bas à gauche");
        }
    }

    public void DetectCardUp2(CaseSlot slot)
    {
        CaseSlot upCell2 = GetCellOnGrid(slot.coordinates.x, slot.coordinates.y + 2); // haut
        if (upCell2)
        {
            if (upCell2.card)
                print("Carte en haut");
        }
    }

    public void DetectCardDown2(CaseSlot slot)
    {
        CaseSlot downCell2 = GetCellOnGrid(slot.coordinates.x, slot.coordinates.y - 2); // bas
        if (downCell2)
        {
            if (downCell2.card)
                print("Carte en bas");
        }
    }

    public void DetectCardDown3(CaseSlot slot)
    {
        CaseSlot downCell3 = GetCellOnGrid(slot.coordinates.x, slot.coordinates.y - 3); // bas
        if (downCell3)
        {
            if (downCell3.card)
                print("Carte en bas");
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
        card.GetComponent<CardDisplay>().card.cell = slot;
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
        if (x >= gridSize.x || x < 0 || y >= gridSize.y || y < 0)
        {
            return null;
        }
        return allCases[x, y];
    }
}
