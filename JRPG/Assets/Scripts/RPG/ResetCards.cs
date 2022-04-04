using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCards : MonoBehaviour
{
    public Card[] cards;
    void Start()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].powerPlayer = cards[i].powerIA;
        }
    }

}
