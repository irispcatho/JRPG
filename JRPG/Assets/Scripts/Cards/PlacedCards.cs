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
                    casesManager.DetectCardUL(slot);
                    casesManager.DetectCardUR(slot);
                    casesManager.DetectCardDL(slot);
                    casesManager.DetectCardDL2(slot);
                    casesManager.DetectCardDR(slot);
                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Coq")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    print(slot);
                    casesManager.DetectCardRight(slot);
                    casesManager.DetectCardLeft(slot);
                    casesManager.DetectCardUp(slot);
                    casesManager.DetectCardDown(slot);
                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Rat")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    print(slot);
                    casesManager.DetectCardLeft(slot);
                    casesManager.DetectCardDown(slot);
                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Buffle")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    print(slot);
                    casesManager.DetectCardUp(slot);
                    casesManager.DetectCardUp2(slot);
                    casesManager.DetectCardDown(slot);
                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Sanglier")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    print(slot);
                    casesManager.DetectCardDown(slot);
                    casesManager.DetectCardDown2(slot);
                    casesManager.DetectCardDown3(slot);
                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Abeille")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    print(slot);
                    casesManager.DetectCardLeft(slot);
                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Dragon")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    print(slot);
                    casesManager.DetectCardRight(slot);
                    casesManager.DetectCardLeft(slot);
                    casesManager.DetectCardUp(slot);
                    casesManager.DetectCardDown(slot);
                    casesManager.DetectCardDL(slot);
                    casesManager.DetectCardDR(slot);
                    casesManager.DetectCardUR(slot);
                    casesManager.DetectCardUL(slot);
                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Hippocampe")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    print(slot);
                    casesManager.DetectCardUL(slot);
                    casesManager.DetectCardUR(slot);
                    casesManager.DetectCardDL(slot);
                    casesManager.DetectCardDL2(slot);
                    casesManager.DetectCardDR(slot);
                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Caméléon")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    print(slot);
                    casesManager.DetectCardUp(slot);
                    casesManager.DetectCardLeft(slot);
                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Chèvre")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    print(slot);
                    casesManager.DetectCardUL(slot);
                    casesManager.DetectCardUR(slot);
                    casesManager.DetectCardLeft(slot);
                    casesManager.DetectCardRight(slot);
                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Oursin")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    print(slot);
                    casesManager.DetectCardRight(slot);
                    casesManager.DetectCardLeft(slot);
                    casesManager.DetectCardUp(slot);
                    casesManager.DetectCardDown(slot);
                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Panda")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    print(slot);
                    casesManager.DetectCardUp(slot);
                    casesManager.DetectCardRight(slot);
                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Renard")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    print(slot);
                    casesManager.DetectCardLeft(slot);
                    casesManager.DetectCardRight(slot);
                }

                if (placedCardsList[i].GetComponent<CardDisplay>().card.frenchName == "Carpe Koï")
                {
                    CaseSlot slot = placedCardsList[i].GetComponent<CardDisplay>().card.cell;
                    print(slot);
                    casesManager.DetectCardUp(slot);
                    casesManager.DetectCardDown(slot);
                }
            }
            count++;
        }
    }

}
