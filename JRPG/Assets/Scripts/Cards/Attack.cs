using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public void AttackCard()
    {
        for (int i = 0; i < length; i++)
        {
            CaseSlot slot = cell.GetComponent<CaseSlot>();
            slot.card = card.GetComponent<CardDisplay>().card;
            DetectCards(slot);
        }
    }
}
