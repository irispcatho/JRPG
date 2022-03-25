using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlacedCards : MonoBehaviour
{
    public List<GameObject> placedCardsList;
    public List<GameObject> OrderList;
    public CasesManager casesManager;

    private int pdvPlayer;
    private int pdvIA;
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
    }

    public IEnumerator Attack()
    {
        for (int i = 0; i <= OrderList.Count - 1; i++)
        {
            yield return new WaitForSeconds(2);
            print("coroutine lancée");
            print(OrderList[i].name);        
            
            #region AttackPattern
            if (OrderList[i].GetComponent<CardDisplay>().card.frenchName == "Cheval")
            {
                CaseSlot slot = OrderList[i].GetComponent<CardDisplay>().card.cell;
                casesManager.DetectCardUL(slot, slot.card.power);
                casesManager.DetectCardUR(slot, slot.card.power);
                casesManager.DetectCardDL(slot, slot.card.power);
                casesManager.DetectCardDL2(slot, slot.card.power);
                casesManager.DetectCardDR(slot, slot.card.power);
            }

            if (OrderList[i].GetComponent<CardDisplay>().card.frenchName == "Coq")
            {
                CaseSlot slot = OrderList[i].GetComponent<CardDisplay>().card.cell;
                CaseSlot leftCell = casesManager.GetCellOnGrid(slot.coordinates.x - 1, slot.coordinates.y); // gauche
                if (leftCell)
                {
                    if (leftCell.card)
                    {
                        if (slot.card.isEnemy != leftCell.card.isEnemy)
                            leftCell.card.power -= slot.card.power;
                        else if (slot.card.isEnemy == leftCell.card.isEnemy)
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
                        else if (slot.card.isEnemy == rightCell.card.isEnemy)
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
                        else if (slot.card.isEnemy == upCell.card.isEnemy)
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
                        else if (slot.card.isEnemy == downCell.card.isEnemy)
                            downCell.card.power += 2;
                    }
                }
            }


            if (OrderList[i].GetComponent<CardDisplay>().card.frenchName == "Rat")
            {
                CaseSlot slot = OrderList[i].GetComponent<CardDisplay>().card.cell;
                casesManager.DetectCardLeft(slot, slot.card.power);
                casesManager.DetectCardDown(slot, slot.card.power);
            }

            if (OrderList[i].GetComponent<CardDisplay>().card.frenchName == "Buffle")
            {
                CaseSlot slot = OrderList[i].GetComponent<CardDisplay>().card.cell;
                casesManager.DetectCardUp(slot, slot.card.power);
                casesManager.DetectCardUp2(slot, slot.card.power);
                casesManager.DetectCardDown(slot, slot.card.power);
            }

            if (OrderList[i].GetComponent<CardDisplay>().card.frenchName == "Sanglier")
            {
                CaseSlot slot = OrderList[i].GetComponent<CardDisplay>().card.cell;

                casesManager.DetectCardDown(slot, slot.card.power);
                casesManager.DetectCardDown2(slot, slot.card.power);
                casesManager.DetectCardDown3(slot, slot.card.power);
            }

            if (OrderList[i].GetComponent<CardDisplay>().card.frenchName == "Abeille")
            {
                CaseSlot slot = OrderList[i].GetComponent<CardDisplay>().card.cell;
                casesManager.DetectCardLeft(slot, slot.card.power);
            }

            if (OrderList[i].GetComponent<CardDisplay>().card.frenchName == "Dragon")
            {
                CaseSlot slot = OrderList[i].GetComponent<CardDisplay>().card.cell;

                CaseSlot leftCell = casesManager.GetCellOnGrid(slot.coordinates.x - 1, slot.coordinates.y); // gauche
                if (leftCell)
                {
                    if (leftCell.card)
                        leftCell.card.power -= OrderList[i].GetComponent<CardDisplay>().card.power;
                }

                CaseSlot rightCell = casesManager.GetCellOnGrid(slot.coordinates.x + 1, slot.coordinates.y); // gauche
                if (rightCell)
                {
                    if (rightCell.card)
                        rightCell.card.power -= OrderList[i].GetComponent<CardDisplay>().card.power;
                }

                CaseSlot upCell = casesManager.GetCellOnGrid(slot.coordinates.x, slot.coordinates.y + 1); // gauche
                if (upCell)
                {
                    if (upCell.card)
                        upCell.card.power -= OrderList[i].GetComponent<CardDisplay>().card.power;
                }

                CaseSlot downCell = casesManager.GetCellOnGrid(slot.coordinates.x, slot.coordinates.y - 1); // gauche
                if (downCell)
                {
                    if (downCell.card)
                        downCell.card.power -= OrderList[i].GetComponent<CardDisplay>().card.power;
                }

                CaseSlot diagDRCell = casesManager.GetCellOnGrid(slot.coordinates.x + 1, slot.coordinates.y - 1);
                if (diagDRCell)
                {
                    if (diagDRCell.card)
                    {
                        if (diagDRCell.card)
                            diagDRCell.card.power -= OrderList[i].GetComponent<CardDisplay>().card.power;
                    }
                }

                CaseSlot diagDLCell = casesManager.GetCellOnGrid(slot.coordinates.x - 1, slot.coordinates.y - 1);
                if (diagDLCell)
                {
                    if (diagDLCell.card)
                    {
                        if (diagDLCell.card)
                            diagDLCell.card.power -= OrderList[i].GetComponent<CardDisplay>().card.power;
                    }
                }

                CaseSlot diagURCell = casesManager.GetCellOnGrid(slot.coordinates.x + 1, slot.coordinates.y + 1);
                if (diagURCell)
                {
                    if (diagURCell.card)
                    {
                        if (diagURCell.card)
                            diagURCell.card.power -= OrderList[i].GetComponent<CardDisplay>().card.power;
                    }
                }

                CaseSlot diagULCell = casesManager.GetCellOnGrid(slot.coordinates.x - 1, slot.coordinates.y + 1);
                if (diagULCell)
                {
                    if (diagULCell.card)
                    {
                        if (diagULCell.card)
                            diagULCell.card.power -= OrderList[i].GetComponent<CardDisplay>().card.power;
                    }
                }

            }

            
            if (OrderList[i].GetComponent<CardDisplay>().card.frenchName == "Hippocampe")
            {
                CaseSlot slot = OrderList[i].GetComponent<CardDisplay>().card.cell;
                casesManager.DetectCardUL(slot, slot.card.power);
                casesManager.DetectCardUR(slot, slot.card.power);
                casesManager.DetectCardDL(slot, slot.card.power);
                casesManager.DetectCardDL2(slot, slot.card.power);
                casesManager.DetectCardDR(slot, slot.card.power);
            }

            if (OrderList[i].GetComponent<CardDisplay>().card.frenchName == "Caméléon")
            {
                CaseSlot slot = OrderList[i].GetComponent<CardDisplay>().card.cell;
                casesManager.DetectCardUp(slot, slot.card.power);
                casesManager.DetectCardLeft(slot, slot.card.power);
            }

            if (OrderList[i].GetComponent<CardDisplay>().card.frenchName == "Chèvre")
            {
                CaseSlot slot = OrderList[i].GetComponent<CardDisplay>().card.cell;
                casesManager.DetectCardUL(slot, slot.card.power);
                casesManager.DetectCardUR(slot, slot.card.power);
                casesManager.DetectCardLeft(slot, slot.card.power);
                casesManager.DetectCardRight(slot, slot.card.power);
            }

            if (OrderList[i].GetComponent<CardDisplay>().card.frenchName == "Oursin")
            {
                CaseSlot slot = OrderList[i].GetComponent<CardDisplay>().card.cell;
                casesManager.DetectCardRight(slot, slot.card.power);
                casesManager.DetectCardLeft(slot, slot.card.power);
                casesManager.DetectCardUp(slot, slot.card.power);
                casesManager.DetectCardDown(slot, slot.card.power);
            }

            if (OrderList[i].GetComponent<CardDisplay>().card.frenchName == "Panda")
            {
                CaseSlot slot = OrderList[i].GetComponent<CardDisplay>().card.cell;
                casesManager.DetectCardUp(slot, slot.card.power);
                casesManager.DetectCardRight(slot, slot.card.power);
            }

            if (OrderList[i].GetComponent<CardDisplay>().card.frenchName == "Renard")
            {
                CaseSlot slot = OrderList[i].GetComponent<CardDisplay>().card.cell;
                casesManager.DetectCardLeft(slot, slot.card.power);
                casesManager.DetectCardRight(slot, slot.card.power);
            }

            if (OrderList[i].GetComponent<CardDisplay>().card.frenchName == "Carpe Koï")
            {
                CaseSlot slot = OrderList[i].GetComponent<CardDisplay>().card.cell;
                casesManager.DetectCardUp(slot, slot.card.power);
                casesManager.DetectCardDown(slot, slot.card.power);
            }

            #endregion
            //for (int i = 0; i == placedCardsList.Count; i++)
            //{
            //    int newPower = placedCardsList[i].GetComponent<CardDisplay>().card.power;
            //    if (placedCardsList[i].GetComponent<CardDisplay>().card.isEnemy == false)
            //    {
            //        placedCardsList[i].GetComponent<CardDisplay>().onCaseTextPower.text = newPower.ToString();
            //    }
            //    else
            //        placedCardsList[i].GetComponent<CardDisplay>().onCaseTextIAPower.text = newPower.ToString();

            //    if (placedCardsList[i].GetComponent<CardDisplay>().card.isEnemy == false && newPower > 0)
            //        pdvPlayer += newPower;
            //    if (placedCardsList[i].GetComponent<CardDisplay>().card.isEnemy == true && newPower > 0)
            //        pdvIA += newPower;

            //    if (newPower <= 0)
            //    {
            //        CaseSlot cellToRemove = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
            //        GameObject cellToRemoveGo = cellToRemove.gameObject;
            //        print("Carte " + placedCardsList[i] + " doit être retirée" + cellToRemove + "    " + cellToRemoveGo);
            //        placedCardsList[i].GetComponent<CardDisplay>().onCase.SetActive(false);
            //        placedCardsList[i].GetComponent<CardDisplay>().onCaseIA.SetActive(false);
            //        if (placedCardsList[i].GetComponent<CardDisplay>().card.isEnemy == true)
            //            casesManager.numberIACards--;
            //        if (placedCardsList[i].GetComponent<CardDisplay>().card.isEnemy == false)
            //            casesManager.numberCards--;
            //        casesManager.CasesListUsed.Remove(cellToRemoveGo);
            //        placedCardsList[i].GetComponent<CardDisplay>().card.isDead = true;

            //    }



            //print("Manche finie !");
            //print(pdvIA + " pdv IA");
            //print(pdvPlayer + " pdv Player");

            //if (casesManager.numberCards < casesManager.numberIACards)
            //print("L'IA a gagné");
            //else if (casesManager.numberCards > casesManager.numberIACards)
            //print("Le joueur a gagné");
            //else if (casesManager.numberCards > casesManager.numberIACards)
            //{
            //if (pdvIA > pdvPlayer)
            //print("L'IA a gagné");
            //else if (pdvIA < pdvPlayer)
            //print("Le player a gagné");
            //}

            //for (int j = 0; j <= placedCardsList.Count - 1; j++)
            //{
            //    if (placedCardsList[j].GetComponent<CardDisplay>().card.isDead == true)
            //    {
            //        placedCardsList.Remove(placedCardsList[j]);
            //        OrderList.Remove(placedCardsList[j]);
            //    }
            //}
        }

    }

}