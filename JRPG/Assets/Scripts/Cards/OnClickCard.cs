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
        placedCards.lastCardClicked = gameObject; 
        if (!isPlaced && countUp == 0)
        {
            initalPos = gameObject.transform.position;
            gameObject.transform.DOMove(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 2), 0.5f, false);
            countUp++;
            //description.SetActive(true);
        }

        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        if(hit.collider != gameObject)
        {
            if (!isPlaced && countDown == 0)
            {
                gameObject.transform.DOMove(new Vector2(initalPos.x, initalPos.y), 0.5f, false);
                countDown++;
                //description.SetActive(false);
                //countUp = 0;
            }
        }
    }
}
