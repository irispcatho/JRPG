using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseSlot : MonoBehaviour
{
    public GameObject CasesManager;
    public int casenumber;

    private void OnMouseDown()
    {
        CasesManager.GetComponent<CasesManager>().CaseIsClicker(casenumber);
    }
}
