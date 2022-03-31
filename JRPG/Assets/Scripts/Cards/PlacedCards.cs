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

    public List<PatternAttack> patternAttacks;

    bool launchedattack = false;

    public void Update()
    {
        //if(Input.GetMouseButtonDown(0))
        //{

        //    if(lastCardClicked != )
        //}
        if (OrderList.Count >= 12 && launchedattack == false)
        {
            StartCoroutine(Attack());
            launchedattack = true;
        }

        for (int c = 0; c <= OrderList.Count - 1; c++)
        {
            CardDisplay cardDisplay = OrderList[c].GetComponent<CardDisplay>();
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
        }
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
            yield return new WaitForSeconds(2);
            CaseSlot slot = OrderList[i].GetComponent<CardDisplay>().card.cell;
            GameObject cadreP = OrderList[i].GetComponent<CardDisplay>().cadreP;
            GameObject cadreIA = OrderList[i].GetComponent<CardDisplay>().cadreIA;
            cadreP.SetActive(true);
            cadreIA.SetActive(true);

            #region AttackPattern
            if (i > 0)
            {
                if (OrderList[i] != OrderList[i - 1])
                {
                    OrderList[i - 1].GetComponent<CardDisplay>().cadreP.SetActive(false);
                    OrderList[i - 1].GetComponent<CardDisplay>().cadreIA.SetActive(false);
                }
            }

            PatternAttack pattern = GetPattern(OrderList[i].GetComponent<CardDisplay>().card.frenchName);
            Vector2 cardToAttack = new Vector2(slot.coordinates.x, slot.coordinates.y);
            foreach (var item in pattern.position)
            {
                casesManager.DetectCard(pattern, slot, slot.card.power, cardToAttack, item.x, item.y);
            }            


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

                print("Manche finie !");
                print(pdvIA + " pdv IA");
                print(pdvPlayer + " pdv Player");

                if (numberCardsPlayer > numberCardsIA)
                    print("Le joueur a gagné");
                else if (numberCardsPlayer < numberCardsIA)
                    print("L'IA a gagné");
                else if (numberCardsPlayer == numberCardsIA)
                {
                    if (pdvPlayer > pdvIA)
                        print("Le joueur a gagné");
                    else if (pdvPlayer < pdvIA)
                        print("L'IA a gagné");
                }

                round++;
                if (round < 3)
                    StartCoroutine(WaitForRound());
                else
                    SceneManager.UnloadSceneAsync("CardSystem");
            }
            #endregion
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
        yield return new WaitForSeconds(4);
        ResetCards();
    }
    public IEnumerator Damage(Card card)
    {
        yield return new WaitForSeconds(2);
        card.showDamage = false;
    }
}