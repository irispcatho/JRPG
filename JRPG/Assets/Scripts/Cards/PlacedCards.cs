using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlacedCards : MonoBehaviour
{
    public List<GameObject> placedCardsList;
    public List<GameObject> OrderList;
    public CasesManager casesManager;

    public GameObject lastCardClicked;

    void Update()
    {
        if (placedCardsList.Count >= 12)
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
            }
            //casesManager.DetectCards(slot);
        }
    }

}
