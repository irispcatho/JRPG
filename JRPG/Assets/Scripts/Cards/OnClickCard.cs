using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OnClickCard : MonoBehaviour
{
    public PlacedCards placedCards;
    public bool isPlaced = false;
    private int countUp = 0;
    private int countDown = 0;
    Vector2 initalPos;

    private void OnMouseDown()
    {
        if (!isPlaced && countUp == 0)
        {
            if(gameObject != placedCards.lastCardClicked)
            {
                initalPos = gameObject.transform.position;
                gameObject.transform.DOMove(new Vector2(initalPos.x, initalPos.y + 0.5f), 0.5f, false);
                countUp++;
                countDown = 0;

            }
        }

        if (!isPlaced && countDown == 0)
        {
            if (gameObject != placedCards.lastCardClicked)
            {
                placedCards.lastCardClicked.transform.DOMove(new Vector2(placedCards.lastCardClicked.transform.position.x, placedCards.lastCardClicked.transform.position.y - 0.5f), 0.5f, false);
                countDown++;
                countUp = 0;
            }
        }
        placedCards.lastCardClicked = gameObject;
    }
}
