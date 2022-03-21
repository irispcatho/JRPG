using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseSlot : MonoBehaviour
{
    public GameObject CasesManager;
    public int casenumber;

    public Card card;

    public Vector2Int coordinates;

    private void OnMouseDown()
    {
        CasesManager.GetComponent<CasesManager>().CaseIsClicker(casenumber);
        //CasesManager.GetComponent<CasesManager>().TakeACard(casenumber);
    }
}
