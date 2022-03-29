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

    public List<PatternAttack> patternAttacks;

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
            card.GetComponent<OnClickCard>().isPlaced = true;
            placedCards.lastCardClicked.transform.position = position;
            visualCard = card.GetComponent<CardDisplay>().visual;
            visualCardOnCase = card.GetComponent<CardDisplay>().onCase;
            card.GetComponent<BoxCollider2D>().size = new Vector2(164, 164);
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

        card.GetComponent<BoxCollider2D>().size = new Vector2(164, 164);
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


    private void AttackGlobal(CaseSlot cellToAttack, int damage)
    {
        cellToAttack.card.power -= damage;
        cellToAttack.card.damage = damage;
        cellToAttack.card.signeDamage = "-";
        cellToAttack.card.showDamage = true;
    }

    public void DetectCard(CaseSlot slot, int damage, Vector2 newVector, int x, int y)
    {
        CaseSlot cellToAttack = GetCellOnGrid((int)newVector.x + x, (int)newVector.y + y);
        if(cellToAttack)
        {
            if(cellToAttack.card && slot.card.isDead == false)
            {                
                if (slot.card.cardType == Card.CardType.Attack && (slot.card.isEnemy != cellToAttack.card.isEnemy))
                {
                    AttackGlobal(cellToAttack, damage);
                }
                else if (slot.card.cardType == Card.CardType.Backup && (slot.card.isEnemy == cellToAttack.card.isEnemy))
                {
                    if(cellToAttack.card.power > 0)
                    {
                        cellToAttack.card.power += damage;
                        cellToAttack.card.damage = damage;
                        cellToAttack.card.signeDamage = "+";
                        cellToAttack.card.showDamage = true;
                    }
                }
                else if(slot.card.cardType == Card.CardType.Bis)
                {
                    if(slot.card.isEnemy != cellToAttack.card.isEnemy)
                    {
                        AttackGlobal(cellToAttack, damage);
                    }
                    else
                    {
                        if (slot.card.power > 0)
                        {
                            cellToAttack.card.power += 2;
                            cellToAttack.card.damage = slot.card.power;
                            cellToAttack.card.signeDamage = "+";
                            cellToAttack.card.showDamage = true;
                        }
                        else
                            cellToAttack.card.power += 0;
                    }
                }
                else if(slot.card.cardType == Card.CardType.Dragon)
                {
                    AttackGlobal(cellToAttack, damage);
                }
                StartCoroutine(placedCards.Damage(cellToAttack.card));
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
