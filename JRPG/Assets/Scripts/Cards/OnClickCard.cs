using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OnClickCard : MonoBehaviour
{
    public PlacedCards placedCards;
    public SelectionManager selectionManager;

    private void OnMouseDown()
    {
        selectionManager.SelectMe(gameObject);
        placedCards.lastCardClicked = gameObject;
    }
}
