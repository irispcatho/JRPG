using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


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

    bool launchedattack = false;

    public void Update()
    {
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
                if (cardDisplay.card.isEnemy == false)
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

            if (OrderList[i].GetComponent<CardDisplay>().card.frenchName == "Cheval")
            {
                Vector2 cardToAttack = new Vector2(slot.coordinates.x, slot.coordinates.y);
                casesManager.DetectCard(slot, slot.card.power, cardToAttack, 1, 1);
                casesManager.DetectCard(slot, slot.card.power, cardToAttack, 1, -1);
                casesManager.DetectCard(slot, slot.card.power, cardToAttack, -1, -1);
                casesManager.DetectCard(slot, slot.card.power, cardToAttack, -1, 1);
                casesManager.DetectCard(slot, slot.card.power, cardToAttack, -2, -2);
            }

            if (OrderList[i].GetComponent<CardDisplay>().card.frenchName == "Coq")
            {
                CaseSlot leftCell = casesManager.GetCellOnGrid(slot.coordinates.x - 1, slot.coordinates.y); // gauche
                if (leftCell)
                {
                    if (leftCell.card && slot.card.isDead == false)
                    {
                        if (slot.card.isEnemy != leftCell.card.isEnemy)
                        {
                            leftCell.card.power -= slot.card.power;
                            leftCell.card.damage = slot.card.power;
                            leftCell.card.signeDamage = "-";
                            leftCell.card.showDamage = true;
                        }
                        else
                        {
                            if (slot.card.power > 0)
                            {
                                leftCell.card.power += 2;
                                leftCell.card.damage = slot.card.power;
                                leftCell.card.signeDamage = "+";
                                leftCell.card.showDamage = true;
                            }
                            else
                                leftCell.card.power += 0;
                        }
                        StartCoroutine(Damage(leftCell.card));
                    }
                }
                CaseSlot rightCell = casesManager.GetCellOnGrid(slot.coordinates.x + 1, slot.coordinates.y); // droite
                if (rightCell)
                {
                    if (rightCell.card && slot.card.isDead == false)
                    {
                        if (slot.card.isEnemy != rightCell.card.isEnemy)
                        {
                            rightCell.card.power -= slot.card.power;
                            rightCell.card.damage = slot.card.power;
                            rightCell.card.signeDamage = "-";
                            rightCell.card.showDamage = true;
                        }
                        else
                        {
                            if (slot.card.power > 0)
                            {
                                rightCell.card.power += 2;
                                rightCell.card.damage = slot.card.power;
                                rightCell.card.signeDamage = "+";
                                rightCell.card.showDamage = true;
                            }
                            else
                                rightCell.card.power += 0;
                        }
                        StartCoroutine(Damage(rightCell.card));
                    }
                }
                CaseSlot upCell = casesManager.GetCellOnGrid(slot.coordinates.x, slot.coordinates.y + 1); // haut
                if (upCell)
                {
                    if (upCell.card && slot.card.isDead == false)
                    {
                        if (slot.card.isEnemy != upCell.card.isEnemy)
                        {
                            upCell.card.power -= slot.card.power;
                            upCell.card.damage = slot.card.power;
                            upCell.card.signeDamage = "-";
                            upCell.card.showDamage = true;
                        }
                        else
                        {
                            if (slot.card.power > 0)
                            {
                                upCell.card.power += 2;
                                upCell.card.damage = slot.card.power;
                                upCell.card.signeDamage = "+";
                                upCell.card.showDamage = true;
                            }
                            else
                                upCell.card.power += 0;
                        }
                        StartCoroutine(Damage(upCell.card));
                    }
                }
                CaseSlot downCell = casesManager.GetCellOnGrid(slot.coordinates.x, slot.coordinates.y - 1); // bas
                if (downCell)
                {
                    if (downCell.card && slot.card.isDead == false)
                    {
                        if (slot.card.isEnemy != downCell.card.isEnemy)
                        {
                            downCell.card.power -= slot.card.power;
                            downCell.card.damage = slot.card.power;
                            downCell.card.signeDamage = "-";
                            downCell.card.showDamage = true;
                        }
                        else
                        {
                            if (slot.card.power > 0)
                            {
                                downCell.card.power += 2;
                                downCell.card.damage = slot.card.power;
                                downCell.card.signeDamage = "+";
                                downCell.card.showDamage = true;
                            }
                            else
                                downCell.card.power += 0;
                        }
                        StartCoroutine(Damage(downCell.card));
                    }
                }
            }


            if (OrderList[i].GetComponent<CardDisplay>().card.frenchName == "Rat")
            {
                Vector2 cardToAttack = new Vector2(slot.coordinates.x, slot.coordinates.y);
                casesManager.DetectCard(slot, slot.card.power, cardToAttack, 0, -1);
                casesManager.DetectCard(slot, slot.card.power, cardToAttack, -1, 0);
            }

            if (OrderList[i].GetComponent<CardDisplay>().card.frenchName == "Buffle")
            {
                Vector2 cardToAttack = new Vector2(slot.coordinates.x, slot.coordinates.y);
                casesManager.DetectCard(slot, slot.card.power, cardToAttack, 0, -1);
                casesManager.DetectCard(slot, slot.card.power, cardToAttack, 0, 1);
                casesManager.DetectCard(slot, slot.card.power, cardToAttack, 0, 2);
            }

            if (OrderList[i].GetComponent<CardDisplay>().card.frenchName == "Sanglier")
            {
                Vector2 cardToAttack = new Vector2(slot.coordinates.x, slot.coordinates.y);
                casesManager.DetectCard(slot, slot.card.power, cardToAttack, 0, -1);
                casesManager.DetectCard(slot, slot.card.power, cardToAttack, 0, -2);
                casesManager.DetectCard(slot, slot.card.power, cardToAttack, 0, -3);
            }

            if (OrderList[i].GetComponent<CardDisplay>().card.frenchName == "Abeille")
            {
                Vector2 cardToAttack = new Vector2(slot.coordinates.x, slot.coordinates.y);
                casesManager.DetectCard(slot, slot.card.power, cardToAttack, -1, 0);
            }

            if (OrderList[i].GetComponent<CardDisplay>().card.frenchName == "Dragon")
            {
                CaseSlot leftCell = casesManager.GetCellOnGrid(slot.coordinates.x - 1, slot.coordinates.y); // gauche
                if (leftCell)
                {
                    if (leftCell.card && slot.card.isDead == false)
                    {
                        leftCell.card.power -= OrderList[i].GetComponent<CardDisplay>().card.power;
                        leftCell.card.damage = slot.card.power;
                        leftCell.card.signeDamage = "-";
                        leftCell.card.showDamage = true;
                        StartCoroutine(Damage(leftCell.card));
                    }
                }

                CaseSlot rightCell = casesManager.GetCellOnGrid(slot.coordinates.x + 1, slot.coordinates.y); // gauche
                if (rightCell)
                {
                    if (rightCell.card && slot.card.isDead == false)
                    {
                        rightCell.card.power -= OrderList[i].GetComponent<CardDisplay>().card.power;
                        rightCell.card.damage = slot.card.power;
                        rightCell.card.signeDamage = "-";
                        rightCell.card.showDamage = true;
                        StartCoroutine(Damage(rightCell.card));
                    }
                }

                CaseSlot upCell = casesManager.GetCellOnGrid(slot.coordinates.x, slot.coordinates.y + 1); // gauche
                if (upCell)
                {
                    if (upCell.card && slot.card.isDead == false)
                    {
                        upCell.card.power -= OrderList[i].GetComponent<CardDisplay>().card.power;
                        upCell.card.damage = slot.card.power;
                        upCell.card.signeDamage = "-";
                        upCell.card.showDamage = true;
                        StartCoroutine(Damage(upCell.card));
                    }
                }

                CaseSlot downCell = casesManager.GetCellOnGrid(slot.coordinates.x, slot.coordinates.y - 1); // gauche
                if (downCell)
                {
                    if (downCell.card && slot.card.isDead == false)
                    {
                        downCell.card.power -= OrderList[i].GetComponent<CardDisplay>().card.power;
                        downCell.card.damage = slot.card.power;
                        downCell.card.signeDamage = "-";
                        downCell.card.showDamage = true;
                        StartCoroutine(Damage(downCell.card));
                    }
                }

                CaseSlot diagDRCell = casesManager.GetCellOnGrid(slot.coordinates.x + 1, slot.coordinates.y - 1);
                if (diagDRCell)
                {
                    if (diagDRCell.card)
                    {
                        if (diagDRCell.card && slot.card.isDead == false)
                        {
                            diagDRCell.card.power -= OrderList[i].GetComponent<CardDisplay>().card.power;
                            diagDRCell.card.damage = slot.card.power;
                            diagDRCell.card.signeDamage = "-";
                            diagDRCell.card.showDamage = true;
                            StartCoroutine(Damage(diagDRCell.card));
                        }
                    }
                }

                CaseSlot diagDLCell = casesManager.GetCellOnGrid(slot.coordinates.x - 1, slot.coordinates.y - 1);
                if (diagDLCell)
                {
                    if (diagDLCell.card)
                    {
                        if (diagDLCell.card && slot.card.isDead == false)
                        {
                            diagDLCell.card.power -= OrderList[i].GetComponent<CardDisplay>().card.power;
                            diagDLCell.card.damage = slot.card.power;
                            diagDLCell.card.signeDamage = "-";
                            diagDLCell.card.showDamage = true;
                            StartCoroutine(Damage(diagDLCell.card));
                        }
                    }
                }

                CaseSlot diagURCell = casesManager.GetCellOnGrid(slot.coordinates.x + 1, slot.coordinates.y + 1);
                if (diagURCell)
                {
                    if (diagURCell.card)
                    {
                        if (diagURCell.card && slot.card.isDead == false)
                        {
                            diagURCell.card.power -= OrderList[i].GetComponent<CardDisplay>().card.power;
                            diagURCell.card.damage = slot.card.power;
                            diagURCell.card.signeDamage = "-";
                            diagURCell.card.showDamage = true;
                            StartCoroutine(Damage(diagURCell.card));
                        }
                    }
                }

                CaseSlot diagULCell = casesManager.GetCellOnGrid(slot.coordinates.x - 1, slot.coordinates.y + 1);
                if (diagULCell)
                {
                    if (diagULCell.card)
                    {
                        if (diagULCell.card && slot.card.isDead == false)
                        {
                            diagULCell.card.power -= OrderList[i].GetComponent<CardDisplay>().card.power;
                            diagULCell.card.damage = slot.card.power;
                            diagULCell.card.signeDamage = "-";
                            diagULCell.card.showDamage = true;
                            StartCoroutine(Damage(diagULCell.card));
                        }
                    }
                }

            }


            if (OrderList[i].GetComponent<CardDisplay>().card.frenchName == "Hippocampe")
            {
                Vector2 cardToAttack = new Vector2(slot.coordinates.x, slot.coordinates.y);
                casesManager.DetectCard(slot, slot.card.power, cardToAttack, 1, 1);
                casesManager.DetectCard(slot, slot.card.power, cardToAttack, 1, -1);
                casesManager.DetectCard(slot, slot.card.power, cardToAttack, -1, -1);
                casesManager.DetectCard(slot, slot.card.power, cardToAttack, -1, 1);
                casesManager.DetectCard(slot, slot.card.power, cardToAttack, -2, -2);
            }

            if (OrderList[i].GetComponent<CardDisplay>().card.frenchName == "Cam�l�on")
            {
                Vector2 cardToAttack = new Vector2(slot.coordinates.x, slot.coordinates.y);
                casesManager.DetectCard(slot, slot.card.power, cardToAttack, 0, 1);
                casesManager.DetectCard(slot, slot.card.power, cardToAttack, -1, 0);
            }

            if (OrderList[i].GetComponent<CardDisplay>().card.frenchName == "Ch�vre")
            {
                Vector2 cardToAttack = new Vector2(slot.coordinates.x, slot.coordinates.y);
                casesManager.DetectCard(slot, slot.card.power, cardToAttack, 1, 0);
                casesManager.DetectCard(slot, slot.card.power, cardToAttack, -1, 0);
                casesManager.DetectCard(slot, slot.card.power, cardToAttack, -1, 1);
                casesManager.DetectCard(slot, slot.card.power, cardToAttack, 1, 1);
            }

            if (OrderList[i].GetComponent<CardDisplay>().card.frenchName == "Oursin")
            {
                Vector2 cardToAttack = new Vector2(slot.coordinates.x, slot.coordinates.y);
                casesManager.DetectCard(slot, slot.card.power, cardToAttack, 1, 0);
                casesManager.DetectCard(slot, slot.card.power, cardToAttack, -1, 0);
                casesManager.DetectCard(slot, slot.card.power, cardToAttack, 0, 1);
                casesManager.DetectCard(slot, slot.card.power, cardToAttack, 0, -1);
            }

            if (OrderList[i].GetComponent<CardDisplay>().card.frenchName == "Panda")
            {
                Vector2 cardToAttack = new Vector2(slot.coordinates.x, slot.coordinates.y);
                casesManager.DetectCard(slot, slot.card.power, cardToAttack, 1, 0);
                casesManager.DetectCard(slot, slot.card.power, cardToAttack, 0, 1);
            }

            if (OrderList[i].GetComponent<CardDisplay>().card.frenchName == "Renard")
            {
                Vector2 cardToAttack = new Vector2(slot.coordinates.x, slot.coordinates.y);
                casesManager.DetectCard(slot, slot.card.power, cardToAttack, 1, 0);
                casesManager.DetectCard(slot, slot.card.power, cardToAttack, -1, 0);
            }

            if (OrderList[i].GetComponent<CardDisplay>().card.frenchName == "Carpe Ko�")
            {
                Vector2 cardToAttack = new Vector2(slot.coordinates.x, slot.coordinates.y);
                casesManager.DetectCard(slot, slot.card.power, cardToAttack, 0, 1);
                casesManager.DetectCard(slot, slot.card.power, cardToAttack, 0, -1);
            }

            print(OrderList[i].name + OrderList[i].GetComponent<CardDisplay>().card.frenchName + OrderList[i].GetComponent<CardDisplay>().card.power);

            if (i >= 11)
            {
                for (int j = 0; j <= placedCardsList.Count - 1; j++)
                {
                    int power = placedCardsList[j].GetComponent<CardDisplay>().card.power;
                    if (placedCardsList[j].GetComponent<CardDisplay>().card.isEnemy == false)
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
                    print("Le joueur a gagn�");
                else if (numberCardsPlayer < numberCardsIA)
                    print("L'IA a gagn�");
                else if (numberCardsPlayer == numberCardsIA)
                {
                    if (pdvPlayer > pdvIA)
                        print("Le joueur a gagn�");
                    else if (pdvPlayer < pdvIA)
                        print("L'IA a gagn�");
                }

                round = 1;
                ResetCards();
            }
            #endregion
        }
    }

    public void ResetCards()
    {
        placedCardsList.Clear();
        OrderList.Clear();
        casesManager.CasesListUsed.Clear();

        for (int i = playerDeck.parentPlayerDeck.transform.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(playerDeck.parentPlayerDeck.transform.GetChild(i).gameObject);
        }

        for (int i = playerDeck.parentIADeck.transform.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(playerDeck.parentIADeck.transform.GetChild(i).gameObject);
        }
        playerDeck.CardsCreation();
       
    }


    public IEnumerator Damage(Card card)
    {
        yield return new WaitForSeconds(2);
        card.showDamage = false;
    }
}