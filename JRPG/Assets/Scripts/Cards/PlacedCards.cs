using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class PlacedCards : MonoBehaviour
{
    public List<GameObject> placedCardsList;
    public List<GameObject> OrderList;
    public CasesManager casesManager;
    public PlayerDeck playerDeck;

    private int pdvPlayer;
    private int pdvIA;
    private int numberCardsPlayer;
    private int numberCardsIA;
    public int round = 0;
    public GameObject lastCardClicked;
    public GameObject infoClone;
    public int whoWon = -1;
    public int numberWinPlayer = 0;
    public int numberWinIA = 0;

    public List<PatternAttack> patternAttacks;

    bool launchedattack = false;

    public void Update()
    {
        if (OrderList.Count >= 12 && launchedattack == false)
        {
            StartCoroutine(Attack());
            launchedattack = true;
        }

        Damage();
   
    }

    public PatternAttack GetPattern(string name)
    {
        foreach (var item in patternAttacks)
        {
            if (item.name == name)
                return item;
        }

        return null;
    }

    public IEnumerator Attack()
    {
        for (int i = 0; i <= OrderList.Count - 1; i++)
        {
            for (int j = 0; j <= OrderList.Count - 1; j++)
            {
                if (j > 0)
                {
                    if (OrderList[j] != OrderList[j - 1])
                    {
                        OrderList[j - 1].GetComponent<CardDisplay>().cadreP.SetActive(false);
                        OrderList[j - 1].GetComponent<CardDisplay>().cadreIA.SetActive(false);
                    }
                    if (j == OrderList.Count && OrderList[j].GetComponent<CardDisplay>().card.isDead)
                        break;
                }

            }
            if (OrderList[i].GetComponent<CardDisplay>().card.power <= 0)
            {
                print("carte morte");
                i++;
            }



            CaseSlot slot = OrderList[i].GetComponent<CardDisplay>().card.cell;
            GameObject cadreP = OrderList[i].GetComponent<CardDisplay>().cadreP;
            GameObject cadreIA = OrderList[i].GetComponent<CardDisplay>().cadreIA;
            cadreP.SetActive(true);
            cadreIA.SetActive(true);

            gameObject.GetComponent<PlacedCards>().infoClone = infoClone;
            InfosCard infosCard = infoClone.GetComponent<InfosDisplay>().infosCard;
            Card vars = OrderList[i].GetComponent<CardDisplay>().card;
            infosCard.cardName = vars.frenchName;
            infosCard.power = vars.power;
            infosCard.order = vars.gameOrder;
            infosCard.description = vars.description;
            infosCard.pattern = vars.pattern;


            #region AttackPattern

            PatternAttack pattern = GetPattern(OrderList[i].GetComponent<CardDisplay>().card.frenchName);
            Vector2 cardToAttack = new Vector2(slot.coordinates.x, slot.coordinates.y);
            foreach (var item in pattern.position)
            {
                casesManager.DetectCard(pattern, slot, slot.card.power, cardToAttack, item.x, item.y);
            }            

            yield return new WaitForSeconds(2);

            if (i >= 11)
            {
                for (int j = 0; j <= placedCardsList.Count - 1; j++)
                {
                    int power = placedCardsList[j].GetComponent<CardDisplay>().card.power;
                    if (placedCardsList[j].GetComponent<CardDisplay>().card.isEnnemy == false)
                    {
                        numberCardsPlayer++;
                        pdvPlayer += power;
                    }
                    else
                    {
                        numberCardsIA++;
                        pdvIA += power;
                    }
                }
                if (numberCardsPlayer > numberCardsIA)
                {
                    whoWon = 0;
                    numberWinPlayer++;
                    print("Le joueur a gagn�");

                }
                else if (numberCardsPlayer < numberCardsIA)
                {
                    whoWon = 1;
                    numberWinIA++;
                    print("L'IA a gagn�");

                }
                else if (numberCardsPlayer == numberCardsIA)
                {
                    if (pdvPlayer > pdvIA)
                    {
                        whoWon = 0;
                        numberWinPlayer++;
                        print("Le joueur a gagn�");

                    }
                    else if (pdvPlayer < pdvIA)
                    {
                        whoWon = 1;
                        numberWinIA++;
                        print("L'IA a gagn�");

                    }
                }

                StartCoroutine(WaitToUpdateRound());

            }
            #endregion
        }
    }

    private void Damage()
    {
        for (int c = 0; c <= OrderList.Count - 1; c++) //show damage
        {
            CardDisplay cardDisplay = OrderList[c].GetComponent<CardDisplay>();
            cardDisplay.onCaseTextIAPower.text = cardDisplay.card.power.ToString();
            cardDisplay.onCaseTextPower.text = cardDisplay.card.power.ToString();

            cardDisplay.damageGoPText.text = cardDisplay.card.damage.ToString();
            cardDisplay.damageGoIAText.text = cardDisplay.card.damage.ToString();

            cardDisplay.signeDamageGoPText.text = cardDisplay.card.signeDamage.ToString();
            cardDisplay.signeDamageGoIAText.text = cardDisplay.card.signeDamage.ToString();

            if (cardDisplay.card.showDamage)
            {
                cardDisplay.damageGoP.SetActive(true);
                cardDisplay.damageGoIA.SetActive(true);
            }
            else
            {
                cardDisplay.damageGoP.SetActive(false);
                cardDisplay.damageGoIA.SetActive(false);
            }
            if (cardDisplay.card.power <= 0)
            {
                cardDisplay.card.power = 0;
                cardDisplay.card.isDead = true;
                cardDisplay.onCaseIA.SetActive(false);
                cardDisplay.onCase.SetActive(false);
                if (cardDisplay.card.isEnnemy == false)
                    cardDisplay.imageDeadP.SetActive(true);
                else
                    cardDisplay.imageDeadIA.SetActive(true);
                OrderList[c].GetComponent<BoxCollider2D>().enabled = false;
                placedCardsList.Remove(OrderList[c]);
            }
        }
    }
    public void ResetCards()
    {
        placedCardsList.Clear();
        OrderList.Clear();
        casesManager.CasesListUsed.Clear();
        lastCardClicked = gameObject;
        for (int i = playerDeck.parentPlayerDeck.transform.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(playerDeck.parentPlayerDeck.transform.GetChild(i).gameObject);
        }

        for (int i = playerDeck.parentIADeck.transform.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(playerDeck.parentIADeck.transform.GetChild(i).gameObject);
        }
        playerDeck.CardsCreation();
        launchedattack = false;
    }

    private IEnumerator WaitForRound()
    {
        yield return new WaitForSeconds(2);
        ResetCards();
    }

    private IEnumerator WaitToUpdateRound()
    {
        yield return new WaitForSeconds(3);
        if (numberWinPlayer == 2 || numberWinIA == 2)
            StartCoroutine(WaitForClose());

        whoWon = -1;
        round++;

        if (round < 3)
            StartCoroutine(WaitForRound());
        else
            StartCoroutine(WaitForClose());
    }

    IEnumerator WaitForClose()
    {
        yield return new WaitForSeconds(1.95f);
        SceneManager.UnloadSceneAsync("CardSystem");
        round = 0;
        whoWon = -1;
    }
    public IEnumerator Damage(Card card)
    {
        yield return new WaitForSeconds(2);
        card.showDamage = false;
    }
}