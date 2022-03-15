using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{
    public List<Card> cardsPlayer = new List<Card>();
    public List<Card> cardsIA = new List<Card>();
    public GameObject parentPlayerDeck;
    public GameObject parentIADeck;
    public GameObject cardPrefab;

    public PlacedCards placedCards;

    private void Start()
    {
        for (int i = 0; i < cardsPlayer.Count; i++)
        {
            GameObject go = Instantiate(cardPrefab);
            go.name = "Carte joueur numéro " + i ;
            go.transform.SetParent(parentPlayerDeck.transform, false);
            go.GetComponent<CardDisplay>().card = cardsPlayer[i];
            go.GetComponent<OnClickCard>().placedCards = placedCards;
        }
        
        for (int i = 0; i < cardsIA.Count; i++)
        {
            GameObject go = Instantiate(cardPrefab);
            go.name = "Carte IA numéro " + i;
            go.transform.SetParent(parentIADeck.transform, false);
            go.GetComponent<CardDisplay>().card = cardsIA[i];
            go.GetComponent<OnClickCard>().placedCards = placedCards;
            go.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    //private Card GetRandomCard()
    //{
    //    int rnd = Random.Range(0, cards.Count);
    //    return cards[rnd];
    //}
}
