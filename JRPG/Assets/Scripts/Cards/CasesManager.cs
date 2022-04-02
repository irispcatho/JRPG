using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

    private bool healer;

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
            AudioManager.instance.Play("CardPlacement");
            playerCanPlay = false;
            CasesListUsed.Add(cell);
            placedCards.placedCardsList.Add(placedCards.lastCardClicked);
            GameObject card = placedCards.lastCardClicked;
            card.GetComponent<CardDisplay>().card.isPlaced = true;


            placedCards.lastCardClicked.transform.DOComplete();
            placedCards.lastCardClicked.transform.position = position;
            visualCard = card.GetComponent<CardDisplay>().visual;
            visualCardOnCase = card.GetComponent<CardDisplay>().onCase;
            card.GetComponent<BoxCollider2D>().enabled = false;

            visualCard.SetActive(false);
            visualCardOnCase.SetActive(true);
            card.GetComponent<CardDisplay>().cadreP.SetActive(false);

            placedCards.OrderList.Add(card);
            OrderManagement();

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
        if (!playerCanPlay)
        {
            AudioManager.instance.Play("CardPlacement");
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
            card.GetComponent<BoxCollider2D>().enabled = false;

            placedCards.OrderList.Add(card);
            OrderManagement();

            CaseSlot slot = randomCell.GetComponent<CaseSlot>();
            slot.card = card.GetComponent<CardDisplay>().card;
            card.GetComponent<CardDisplay>().card.cell = slot;

            playerCanPlay = true;

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


    private void AttackGlobal(CaseSlot cellToAttack, int damage, string signe)
    {
        if (!healer)
        {
            if (!cellToAttack.card.isEnnemy)
                cellToAttack.card.powerPlayer -= damage;
            else
                cellToAttack.card.powerIA -= damage;
        }
        else
        {
            if (!cellToAttack.card.isEnnemy)
                cellToAttack.card.powerPlayer += damage;
            else
                cellToAttack.card.powerIA += damage;
        }
        cellToAttack.card.damage = damage;
        cellToAttack.card.signeDamage = signe;
        cellToAttack.card.showDamage = true;
    }

    public void DetectCard(PatternAttack pattern, CaseSlot slot, int damage, Vector2 newVector, int x, int y)
    {
        CaseSlot cellToAttack = GetCellOnGrid((int)newVector.x + x, (int)newVector.y + y);
        if (cellToAttack)
        {
            if (cellToAttack.card && slot.card.isDead == false)
            {
                if (pattern.attackEnnemies && (slot.card.isEnnemy != cellToAttack.card.isEnnemy))
                {
                    healer = false;
                    AudioManager.instance.Play("CardAttack");
                    AttackGlobal(cellToAttack, damage, "-");
                }

                else if (pattern.attackAllies && (slot.card.isEnnemy == cellToAttack.card.isEnnemy))
                {
                    healer = false;
                    AudioManager.instance.Play("CardAttack");
                    AttackGlobal(cellToAttack, damage, "-");
                }

                else if (pattern.healAllies && (slot.card.isEnnemy == cellToAttack.card.isEnnemy))
                {
                    if(!cellToAttack.card.isEnnemy)
                    {
                        if (cellToAttack.card.powerPlayer > 0 )
                        {
                            healer = true;
                            AudioManager.instance.Play("CardHeal");
                            AttackGlobal(cellToAttack, damage, "+");
                        }
                    }
                    else
                    {
                        if (cellToAttack.card.powerIA > 0)
                        {
                            healer = true;
                            AudioManager.instance.Play("CardHeal");
                            AttackGlobal(cellToAttack, damage, "+");
                        }
                    }
                }
                StartCoroutine(placedCards.Damage(cellToAttack.card));
            }
            else if (!cellToAttack.card || cellToAttack.card.isDead == true || slot.card.isEnnemy == cellToAttack.card.isEnnemy)
                AudioManager.instance.Play("CardCantAttack");
        }
        else
            AudioManager.instance.Play("CardCantAttack");
    }

    IEnumerator WaitToPlay()
    {
        yield return new WaitForSeconds(1);
        RandomC();
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
