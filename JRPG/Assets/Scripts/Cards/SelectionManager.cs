using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SelectionManager : MonoBehaviour
{
    public OnClickCard onClickCard;
    Vector2 initalPos;
    public PlacedCards placedCards;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            print(hit.collider.gameObject);
            initalPos = placedCards.lastCardClicked.transform.position;

            if (hit.collider.gameObject != placedCards.lastCardClicked)
            {
                print("Click");
                placedCards.lastCardClicked.transform.DOMove(new Vector2(initalPos.x, initalPos.y), 0.5f, false);
            }
        }
    }
}
