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
        if (!placedCards.placedCardsList.Contains(placedCards.lastCardClicked) && playerCanPlay && !CasesListUsed.Contains(cell))
        {
            CasesListUsed.Add(cell);
            placedCards.placedCardsList.Add(placedCards.lastCardClicked);
            GameObject card = placedCards.lastCardClicked;
            card.GetComponent<OnMouseOverCard>().isPlaced = true;
            placedCards.lastCardClicked.transform.position = position;
            visualCard = card.GetComponent<CardDisplay>().visual;
            visualCardOnCase = card.GetComponent<CardDisplay>().onCase;
            card.GetComponent<BoxCollider2D>().size = new Vector2(100, 100);
            card.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);

            visualCard.SetActive(false);
            visualCardOnCase.SetActive(true);
            card.GetComponent<CardDisplay>().cadreP.SetActive(false);

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
        card.GetComponent<CardDisplay>().cadreIA.SetActive(false);

        card.GetComponent<BoxCollider2D>().size = new Vector2(100 , 100);
        card.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);

        placedCards.OrderList.Add(card);
        OrderManagement();

        CaseSlot slot = randomCell.GetComponent<CaseSlot>();
        slot.card = card.GetComponent<CardDisplay>().card;
        card.GetComponent<CardDisplay>().card.cell = slot;
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

    public void DetectCardLeft(CaseSlot slot, int damage)
    {
        CaseSlot leftCell = GetCellOnGrid(slot.coordinates.x - 1, slot.coordinates.y); // gauche
        if (leftCell)
        {
            if (leftCell.card && slot.card.isDead == false)
            {
                if (slot.card.typeTxt == "Attaque" && (slot.card.isEnemy != leftCell.card.isEnemy))
                {
                    leftCell.card.power -= damage;
                    leftCell.card.damage = damage;
                    leftCell.card.signeDamage = "-";
                    leftCell.card.showDamage = true;
                }
                else if (slot.card.typeTxt == "Soutien" && (slot.card.isEnemy != leftCell.card.isEnemy))
                {
                    leftCell.card.power += damage;
                    leftCell.card.damage = damage;
                    leftCell.card.signeDamage = "+";
                    leftCell.card.showDamage = true;
                }
                StartCoroutine(placedCards.Damage(leftCell.card));
            }
        }
    }
    public void DetectCardRight(CaseSlot slot, int damage)
    {
        CaseSlot rightCell = GetCellOnGrid(slot.coordinates.x + 1, slot.coordinates.y); // droite
        if (rightCell)
        {
            if (rightCell.card && slot.card.isDead == false)
            {
                if (slot.card.typeTxt == "Attaque" && (slot.card.isEnemy != rightCell.card.isEnemy))
                {
                    rightCell.card.power -= damage;
                    rightCell.card.damage = damage;
                    rightCell.card.signeDamage = "-";
                    rightCell.card.showDamage = true;
                }
                else if (slot.card.typeTxt == "Soutien" && (slot.card.isEnemy != rightCell.card.isEnemy))
                {
                    rightCell.card.power += damage;
                    rightCell.card.damage = damage;
                    rightCell.card.signeDamage = "+";
                    rightCell.card.showDamage = true;
                }
                StartCoroutine(placedCards.Damage(rightCell.card));
            }
        }
    }
    public void DetectCardUp(CaseSlot slot, int damage)
    {
        CaseSlot upCell = GetCellOnGrid(slot.coordinates.x, slot.coordinates.y + 1); // haut
        if (upCell)
        {
            if (upCell.card && slot.card.isDead == false)
            {
                if (slot.card.typeTxt == "Attaque" && (slot.card.isEnemy != upCell.card.isEnemy))
                {
                    upCell.card.power -= damage;
                    upCell.card.damage = damage;
                    upCell.card.signeDamage = "-";
                    upCell.card.showDamage = true;
                }
                else if (slot.card.typeTxt == "Soutien" && (slot.card.isEnemy != upCell.card.isEnemy))
                {
                    upCell.card.power += damage;
                    upCell.card.damage = damage;
                    upCell.card.signeDamage = "+";
                    upCell.card.showDamage = true;
                }
                StartCoroutine(placedCards.Damage(upCell.card));
            }
        }
    }
    public void DetectCardDown(CaseSlot slot, int damage)
    {
        CaseSlot downCell = GetCellOnGrid(slot.coordinates.x, slot.coordinates.y - 1); // bas
        if (downCell)
        {
            if (downCell.card && slot.card.isDead == false)
            {
                if (slot.card.typeTxt == "Attaque" && (slot.card.isEnemy != downCell.card.isEnemy))
                {
                    downCell.card.power -= damage;
                    downCell.card.damage = damage;
                    downCell.card.signeDamage = "-";
                    downCell.card.showDamage = true;
                }
                else if (slot.card.typeTxt == "Soutien" && (slot.card.isEnemy != downCell.card.isEnemy))
                {
                    downCell.card.power += damage;
                    downCell.card.damage = damage;
                    downCell.card.signeDamage = "+";
                    downCell.card.showDamage = true;
                }
                StartCoroutine(placedCards.Damage(downCell.card));
            }
        }
    }
    public void DetectCardDR(CaseSlot slot, int damage)
    {
        CaseSlot diagDRCell = GetCellOnGrid(slot.coordinates.x + 1, slot.coordinates.y - 1);
        if (diagDRCell)
        {
            if (diagDRCell.card && slot.card.isDead == false)
            {
                if (slot.card.typeTxt == "Attaque" && (slot.card.isEnemy != diagDRCell.card.isEnemy))
                {
                    diagDRCell.card.power -= damage;
                    diagDRCell.card.damage = damage;
                    diagDRCell.card.signeDamage = "-";
                    diagDRCell.card.showDamage = true;
                }
                else if (slot.card.typeTxt == "Soutien" && (slot.card.isEnemy != diagDRCell.card.isEnemy))
                {
                    diagDRCell.card.power += damage;
                    diagDRCell.card.damage = damage;
                    diagDRCell.card.signeDamage = "+";
                    diagDRCell.card.showDamage = true;
                }
                StartCoroutine(placedCards.Damage(diagDRCell.card));
            }
        }
    }
    public void DetectCardDL(CaseSlot slot, int damage)
    {
        CaseSlot diagDLCell = GetCellOnGrid(slot.coordinates.x - 1, slot.coordinates.y - 1);
        if (diagDLCell)
        {
            if (diagDLCell.card && slot.card.isDead == false)
            {
                if (slot.card.typeTxt == "Attaque" && (slot.card.isEnemy != diagDLCell.card.isEnemy))
                {
                    diagDLCell.card.power -= damage;
                    diagDLCell.card.damage = damage;
                    diagDLCell.card.signeDamage = "-";
                    diagDLCell.card.showDamage = true;
                }
                else if (slot.card.typeTxt == "Soutien" && (slot.card.isEnemy != diagDLCell.card.isEnemy))
                {
                    diagDLCell.card.power += damage;
                    diagDLCell.card.damage = damage;
                    diagDLCell.card.signeDamage = "+";
                    diagDLCell.card.showDamage = true;
                }
                StartCoroutine(placedCards.Damage(diagDLCell.card));
            }
        }
    }
    public void DetectCardUR(CaseSlot slot, int damage)
    {
        CaseSlot diagURCell = GetCellOnGrid(slot.coordinates.x + 1, slot.coordinates.y + 1);
        if (diagURCell)
        {
            if (diagURCell.card && slot.card.isDead == false)
            {
                if (slot.card.typeTxt == "Attaque" && (slot.card.isEnemy != diagURCell.card.isEnemy))
                {
                    diagURCell.card.power -= damage;
                    diagURCell.card.damage = damage;
                    diagURCell.card.signeDamage = "-";
                    diagURCell.card.showDamage = true;
                }
                else if (slot.card.typeTxt == "Soutien" && (slot.card.isEnemy != diagURCell.card.isEnemy))
                {
                    diagURCell.card.power += damage;
                    diagURCell.card.damage = damage;
                    diagURCell.card.signeDamage = "+";
                    diagURCell.card.showDamage = true;
                }
                StartCoroutine(placedCards.Damage(diagURCell.card));
            }
        }
    }
    public void DetectCardUL(CaseSlot slot, int damage)
    {
        CaseSlot diagULCell = GetCellOnGrid(slot.coordinates.x - 1, slot.coordinates.y + 1);
        if (diagULCell)
        {
            if (diagULCell.card && slot.card.isDead == false)
            {
                if (slot.card.typeTxt == "Attaque" && (slot.card.isEnemy != diagULCell.card.isEnemy))
                {
                    diagULCell.card.power -= damage;
                    diagULCell.card.damage = damage;
                    diagULCell.card.signeDamage = "-";
                    diagULCell.card.showDamage = true;
                }
                else if (slot.card.typeTxt == "Soutien" && (slot.card.isEnemy != diagULCell.card.isEnemy))
                {
                    diagULCell.card.power += damage;
                    diagULCell.card.damage = damage;
                    diagULCell.card.signeDamage = "+";
                    diagULCell.card.showDamage = true;
                }
                StartCoroutine(placedCards.Damage(diagULCell.card));
            }
        }
    }

    public void DetectCardDL2(CaseSlot slot, int damage)
    {
        CaseSlot diagDL2Cell = GetCellOnGrid(slot.coordinates.x - 2, slot.coordinates.y - 2);
        if (diagDL2Cell)
        {
            if (diagDL2Cell.card && slot.card.isDead == false)
            {
                if (slot.card.typeTxt == "Attaque" && (slot.card.isEnemy != diagDL2Cell.card.isEnemy))
                {
                    diagDL2Cell.card.power -= damage;
                    diagDL2Cell.card.damage = damage;
                    diagDL2Cell.card.signeDamage = "-";
                }
                else if (slot.card.typeTxt == "Soutien" && (slot.card.isEnemy != diagDL2Cell.card.isEnemy))
                {
                    diagDL2Cell.card.power += damage;
                    diagDL2Cell.card.damage = damage;
                    diagDL2Cell.card.signeDamage = "+";
                }
                StartCoroutine(placedCards.Damage(diagDL2Cell.card));
            }
        }
    }

    public void DetectCardUp2(CaseSlot slot, int damage)
    {
        CaseSlot upCell2 = GetCellOnGrid(slot.coordinates.x, slot.coordinates.y + 2); // haut
        if (upCell2)
        {
            if (upCell2.card && slot.card.isDead == false)
            {
                if (slot.card.typeTxt == "Attaque" && (slot.card.isEnemy != upCell2.card.isEnemy))
                {
                    upCell2.card.power -= damage;
                    upCell2.card.damage = damage;
                    upCell2.card.signeDamage = "-";
                }
                else if (slot.card.typeTxt == "Soutien" && (slot.card.isEnemy != upCell2.card.isEnemy))
                {
                    upCell2.card.power += damage;
                    upCell2.card.damage = damage;
                    upCell2.card.signeDamage = "+";
                }
                StartCoroutine(placedCards.Damage(upCell2.card));
            }
        }
    }

    public void DetectCardDown2(CaseSlot slot, int damage)
    {
        CaseSlot downCell2 = GetCellOnGrid(slot.coordinates.x, slot.coordinates.y - 2); // bas
        if (downCell2)
        {
            if (downCell2.card && slot.card.isDead == false)
            {
                if (slot.card.typeTxt == "Attaque" && (slot.card.isEnemy != downCell2.card.isEnemy))
                {
                    downCell2.card.power -= damage;
                    downCell2.card.damage = damage;
                    downCell2.card.signeDamage = "-";
                }
                else if (slot.card.typeTxt == "Soutien" && (slot.card.isEnemy != downCell2.card.isEnemy))
                {
                    downCell2.card.power += damage;
                    downCell2.card.damage = damage;
                    downCell2.card.signeDamage = "+";
                }
                StartCoroutine(placedCards.Damage(downCell2.card));
            }
        }
    }

    public void DetectCardDown3(CaseSlot slot, int damage)
    {
        CaseSlot downCell3 = GetCellOnGrid(slot.coordinates.x, slot.coordinates.y - 3); // bas
        if (downCell3)
        {
            if (downCell3.card && slot.card.isDead == false)
            {
                if (slot.card.typeTxt == "Attaque" && (slot.card.isEnemy != downCell3.card.isEnemy))
                {
                    downCell3.card.power -= damage;
                    downCell3.card.damage = damage;
                    downCell3.card.signeDamage = "-";
                }
                else if (slot.card.typeTxt == "Soutien" && (slot.card.isEnemy != downCell3.card.isEnemy))
                {
                    downCell3.card.power += damage;
                    downCell3.card.damage = damage;
                    downCell3.card.signeDamage = "+";
                }
                StartCoroutine(placedCards.Damage(downCell3.card));
            }
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
        if (x >= gridSize.x || x < 0 || y >= gridSize.y || y < 0)
        {
            return null;
        }
        return allCases[x, y];
    }
}
