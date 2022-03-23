using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlacedCards : MonoBehaviour
{
    public List<GameObject> placedCardsList;
    public List<GameObject> OrderList;
    public CasesManager casesManager;

    public GameObject lastCardClicked;
    private int count = 0;

    void Update()
    {
        if (placedCardsList.Count >= 12 && count == 0)
        {
            print("Toutes les cartes ont été jouées");
            for (int i = 0; i <= OrderList.Count - 1; i++)
            {
                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Cheval")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    casesManager.DetectCardUL(slot, slot.card.power);
                    casesManager.DetectCardUR(slot, slot.card.power);
                    casesManager.DetectCardDL(slot, slot.card.power);
                    casesManager.DetectCardDL2(slot, slot.card.power);
                    casesManager.DetectCardDR(slot, slot.card.power);
                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Coq")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    CaseSlot leftCell = casesManager.GetCellOnGrid(slot.coordinates.x - 1, slot.coordinates.y); // gauche
                    if (leftCell)
                    {
                        if (leftCell.card)
                        {
                            if (slot.card.isEnemy != leftCell.card.isEnemy)
                                leftCell.card.power -= slot.card.power;
                            else
                                leftCell.card.power += 2;
                        }
                    }

                    CaseSlot rightCell = casesManager.GetCellOnGrid(slot.coordinates.x + 1, slot.coordinates.y); // droite
                    if (rightCell)
                    {
                        if (rightCell.card)
                        {
                            if (slot.card.isEnemy != rightCell.card.isEnemy)
                                rightCell.card.power -= slot.card.power;
                            else
                                rightCell.card.power += 2;
                        }
                    }

                    CaseSlot upCell = casesManager.GetCellOnGrid(slot.coordinates.x, slot.coordinates.y + 1); // haut
                    if (upCell)
                    {
                        if (upCell.card)
                        {
                            if (slot.card.isEnemy != upCell.card.isEnemy)
                                upCell.card.power -= slot.card.power;
                            else
                                upCell.card.power += 2;
                        }
                    }

                    CaseSlot downCell = casesManager.GetCellOnGrid(slot.coordinates.x, slot.coordinates.y - 1); // bas
                    if (downCell)
                    {
                        if (downCell.card)
                        {
                            if (slot.card.isEnemy != downCell.card.isEnemy)
                                downCell.card.power -= slot.card.power;
                            else
                                downCell.card.power += 2;
                        }
                    }
                }


                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Rat")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    casesManager.DetectCardLeft(slot, slot.card.power);
                    casesManager.DetectCardDown(slot, slot.card.power);
                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Buffle")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    casesManager.DetectCardUp(slot, slot.card.power);
                    casesManager.DetectCardUp2(slot, slot.card.power);
                    casesManager.DetectCardDown(slot, slot.card.power);
                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Sanglier")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;

                    casesManager.DetectCardDown(slot, slot.card.power);
                    casesManager.DetectCardDown2(slot, slot.card.power);
                    casesManager.DetectCardDown3(slot, slot.card.power);
                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Abeille")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    casesManager.DetectCardLeft(slot, slot.card.power);
                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Dragon")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;

                    CaseSlot leftCell = casesManager.GetCellOnGrid(slot.coordinates.x - 1, slot.coordinates.y); // gauche
                    if (leftCell)
                    {
                        if (leftCell.card)
                            leftCell.card.power -= slot.card.power;
                    }

                    CaseSlot rightCell = casesManager.GetCellOnGrid(slot.coordinates.x + 1, slot.coordinates.y); // gauche
                    if (rightCell)
                    {
                        if (rightCell.card)
                            rightCell.card.power -= slot.card.power;
                    }

                    CaseSlot upCell = casesManager.GetCellOnGrid(slot.coordinates.x, slot.coordinates.y + 1); // gauche
                    if (upCell)
                    {
                        if (upCell.card)
                            upCell.card.power -= slot.card.power;
                    }

                    CaseSlot downCell = casesManager.GetCellOnGrid(slot.coordinates.x, slot.coordinates.y - 1); // gauche
                    if (downCell)
                    {
                        if (downCell.card)
                            downCell.card.power -= slot.card.power;
                    }

                    CaseSlot diagDRCell = casesManager.GetCellOnGrid(slot.coordinates.x + 1, slot.coordinates.y - 1);
                    if (diagDRCell)
                    {
                        if (diagDRCell.card)
                        {
                            if (diagDRCell.card)
                                diagDRCell.card.power -= slot.card.power;
                        }
                    }

                    CaseSlot diagDLCell = casesManager.GetCellOnGrid(slot.coordinates.x - 1, slot.coordinates.y - 1);
                    if (diagDLCell)
                    {
                        if (diagDLCell.card)
                        {
                            if (diagDLCell.card)
                                diagDLCell.card.power -= slot.card.power;
                        }
                    }

                    CaseSlot diagURCell = casesManager.GetCellOnGrid(slot.coordinates.x + 1, slot.coordinates.y + 1);
                    if (diagURCell)
                    {
                        if (diagURCell.card)
                        {
                            if (diagURCell.card)
                                diagURCell.card.power -= slot.card.power;
                        }
                    }

                    CaseSlot diagULCell = casesManager.GetCellOnGrid(slot.coordinates.x - 1, slot.coordinates.y + 1);
                    if (diagULCell)
                    {
                        if (diagULCell.card)
                        {
                            if (diagULCell.card)
                                diagULCell.card.power -= slot.card.power;
                        }
                    }

                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Hippocampe")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    casesManager.DetectCardUL(slot, slot.card.power);
                    casesManager.DetectCardUR(slot, slot.card.power);
                    casesManager.DetectCardDL(slot, slot.card.power);
                    casesManager.DetectCardDL2(slot, slot.card.power);
                    casesManager.DetectCardDR(slot, slot.card.power);
                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Caméléon")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    casesManager.DetectCardUp(slot, slot.card.power);
                    casesManager.DetectCardLeft(slot, slot.card.power);
                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Chèvre")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    casesManager.DetectCardUL(slot, slot.card.power);
                    casesManager.DetectCardUR(slot, slot.card.power);
                    casesManager.DetectCardLeft(slot, slot.card.power);
                    casesManager.DetectCardRight(slot, slot.card.power);
                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Oursin")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    casesManager.DetectCardRight(slot, slot.card.power);
                    casesManager.DetectCardLeft(slot, slot.card.power);
                    casesManager.DetectCardUp(slot, slot.card.power);
                    casesManager.DetectCardDown(slot, slot.card.power);
                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Panda")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    casesManager.DetectCardUp(slot, slot.card.power);
                    casesManager.DetectCardRight(slot, slot.card.power);
                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Renard")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    casesManager.DetectCardLeft(slot, slot.card.power);
                    casesManager.DetectCardRight(slot, slot.card.power);
                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Carpe Koï")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    casesManager.DetectCardUp(slot, slot.card.power);
                    casesManager.DetectCardDown(slot, slot.card.power);
                }

                Debug.Log(placedCardsList[i].GetComponent<CardDisplay>().card.frenchName + placedCardsList[i].GetComponent<CardDisplay>().card.power + placedCardsList[i].GetComponent<CardDisplay>().card.isEnemy);
            }
            count++;
        }
    }

}
