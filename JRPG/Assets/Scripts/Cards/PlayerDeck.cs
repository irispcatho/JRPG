using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{
    public List<Card> cards = new List<Card>();
    public GameObject parent;
    public GameObject cardPrefab;
    public int cardCount = 6;

    private void Start()
    {
        FirstFight();
    }

    private void FirstFight()
    {
        for (int i = 0; i < cardCount; i++)
        {
            GameObject go = Instantiate(cardPrefab);
            go.transform.SetParent(parent.transform, false);
            go.GetComponent<CardDisplay>().card = cards[i];
        }
    }

    //private Card GetRandomCard()
    //{
    //    int rnd = Random.Range(0, cards.Count);
    //    return cards[rnd];
    //}
}
