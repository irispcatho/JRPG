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
        if (placedCardsList.Count >= 12 && count ==0)
        {
            print("Toutes les cartes ont été jouées");
            for (int i = 0; i <= OrderList.Count - 1; i++)
            {
                print("Boucle lancée");
                if(placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Cheval")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    print(slot);
                    casesManager.DetectCardUL(slot, slot.card.power);
                    casesManager.DetectCardUR(slot, slot.card.power);
                    casesManager.DetectCardDL(slot, slot.card.power);
                    casesManager.DetectCardDL2(slot, slot.card.power);
                    casesManager.DetectCardDR(slot, slot.card.power);
                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Coq")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    print(slot);
                    casesManager.DetectCardRight(slot, slot.card.power);
                    casesManager.DetectCardLeft(slot, slot.card.power);
                    casesManager.DetectCardUp(slot, slot.card.power);
                    casesManager.DetectCardDown(slot, slot.card.power);
                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Rat")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    print(slot);
                    casesManager.DetectCardLeft(slot, slot.card.power);
                    casesManager.DetectCardDown(slot, slot.card.power);
                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Buffle")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    print(slot);
                    casesManager.DetectCardUp(slot, slot.card.power);
                    casesManager.DetectCardUp2(slot, slot.card.power);
                    casesManager.DetectCardDown(slot, slot.card.power);
                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Sanglier")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    print(slot);
                    casesManager.DetectCardDown(slot, slot.card.power);
                    casesManager.DetectCardDown2(slot, slot.card.power);
                    casesManager.DetectCardDown3(slot, slot.card.power);
                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Abeille")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    print(slot);
                    casesManager.DetectCardLeft(slot, slot.card.power);
                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Dragon")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    print(slot);
                    casesManager.DetectCardRight(slot, slot.card.power);
                    casesManager.DetectCardLeft(slot, slot.card.power);
                    casesManager.DetectCardUp(slot, slot.card.power);
                    casesManager.DetectCardDown(slot, slot.card.power);
                    casesManager.DetectCardDL(slot, slot.card.power);
                    casesManager.DetectCardDR(slot, slot.card.power);
                    casesManager.DetectCardUR(slot, slot.card.power);
                    casesManager.DetectCardUL(slot, slot.card.power);
                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Hippocampe")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    print(slot);
                    casesManager.DetectCardUL(slot, slot.card.power);
                    casesManager.DetectCardUR(slot, slot.card.power);
                    casesManager.DetectCardDL(slot, slot.card.power);
                    casesManager.DetectCardDL2(slot, slot.card.power);
                    casesManager.DetectCardDR(slot, slot.card.power);
                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Caméléon")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    print(slot);
                    casesManager.DetectCardUp(slot, slot.card.power);
                    casesManager.DetectCardLeft(slot, slot.card.power);
                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Chèvre")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    print(slot);
                    casesManager.DetectCardUL(slot, slot.card.power);
                    casesManager.DetectCardUR(slot, slot.card.power);
                    casesManager.DetectCardLeft(slot, slot.card.power);
                    casesManager.DetectCardRight(slot, slot.card.power);
                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Oursin")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    print(slot);
                    casesManager.DetectCardRight(slot, slot.card.power);
                    casesManager.DetectCardLeft(slot, slot.card.power);
                    casesManager.DetectCardUp(slot, slot.card.power);
                    casesManager.DetectCardDown(slot, slot.card.power);
                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Panda")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    print(slot);
                    casesManager.DetectCardUp(slot, slot.card.power);
                    casesManager.DetectCardRight(slot, slot.card.power);
                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Renard")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    print(slot);
                    casesManager.DetectCardLeft(slot, slot.card.power);
                    casesManager.DetectCardRight(slot, slot.card.power);
                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Carpe Koï")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    print(slot);
                    casesManager.DetectCardUp(slot, slot.card.power);
                    casesManager.DetectCardDown(slot, slot.card.power);
                }

                //if(placedCardsList[i].GetComponent<CardDisplay>().card.power <= 0)
                //{
                //    placedCardsList[i].SetActive(false);
                //}
            }
            count++;
        }
    }

}
