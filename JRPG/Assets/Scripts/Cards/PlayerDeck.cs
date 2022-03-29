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

    private int count = 0;

    void Awake()
    {
        for (int i = 0; i < cardsPlayer.Count; i++)
        {
            GameObject go = Instantiate(cardPrefab);
            go.GetComponent<CardDisplay>().card = GameObject.Instantiate(cardsPlayer[i]);
            go.name = "Carte joueur " + go.GetComponent<CardDisplay>().card.frenchName;
            go.transform.SetParent(parentPlayerDeck.transform, false);
            go.GetComponent<OnClickCard>().placedCards = placedCards;
            go.GetComponent<CardDisplay>().card.isEnemy = false;
        }

        for (int i = 0; i < cardsIA.Count; i++)
        {
            GameObject go = Instantiate(cardPrefab);
            go.GetComponent<CardDisplay>().card = GameObject.Instantiate(cardsIA[i]);
            go.name = "Carte IA " + go.GetComponent<CardDisplay>().card.frenchName;
            go.transform.SetParent(parentIADeck.transform, false);
            go.GetComponent<OnClickCard>().placedCards = placedCards;
            go.GetComponent<BoxCollider2D>().enabled = false;
            go.GetComponent<CardDisplay>().card.isEnemy = true;
            go.GetComponent<CardDisplay>().visual.SetActive(false);
            go.GetComponent<CardDisplay>().cardIA.SetActive(true);
        }
    }

    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        foreach (var item in FindObjectsOfType<CardDisplay>())
            item.SavePosition();
    }

    //private Card GetRandomCard()
    //{
    //    int rnd = Random.Range(0, cards.Count);
    //    return cards[rnd];
    //}
}
