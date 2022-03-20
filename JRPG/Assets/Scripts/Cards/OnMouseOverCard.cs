using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class OnMouseOverCard : MonoBehaviour
{
    public CardDisplay cardDisplay;
    public bool isPlaced = false;
    private int countUp = 0;
    private int countDown = 0;
    Vector2 initalPos;
    public GameObject description;
    public TMP_Text powerTxt;
    public TMP_Text orderTxt;
    public TMP_Text biomeTxt;

    private void Start()
    {
        description = GetComponent<CardDisplay>().onMouseOver;
        powerTxt.text = "Puissance : " + cardDisplay.powerText.text;
        orderTxt.text = "Ordre de jeu : " + cardDisplay.gameOrderText.text;
        //biomeTxt.text = "Type : " + cardDisplay.biomeText.text;
        description.SetActive(false);
    }
    private void OnMouseOver()
    {
        if (!isPlaced && countUp == 0)
        {
            initalPos = gameObject.transform.position;
            gameObject.transform.DOMove(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 1), 0.5f, false);
            countUp++;
            description.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        if(!isPlaced && countDown == 0)
        {
            gameObject.transform.DOMove(new Vector2(initalPos.x, initalPos.y), 0.5f, false);
            countDown++;
            description.SetActive(false);
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        countUp = 0;
        countDown = 0;
    }
}
