using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasesManager : MonoBehaviour
{
    public List<GameObject> CasesList;
    public PlacedCards Placedcards;

    public void CaseIsClicker(int casenumber)
    {
        Vector2 position = CasesList[casenumber].transform.position;
        Placedcards.LastCardClickerd.transform.position = position;
    }
}
