using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class OnMouseOverCard : MonoBehaviour
{
    private int countUp = 0;
    private int countDown = 0;
    Vector2 initalPos;
    public GameObject description;
    public TMP_Text descriptionText;

    private void Start()
    {
        description = GetComponent<OnMouseOverCard>().description;
        descriptionText = GetComponent<OnMouseOverCard>().descriptionText;
        description.SetActive(false);
    }
    private void OnMouseOver()
    {
        if (countUp == 0)
        {
            initalPos = gameObject.transform.position;
            gameObject.transform.DOMove(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 1), 0.5f, false);
            countUp++;

            description.SetActive(true);
            description.transform.position = new Vector2(gameObject.transform.position.x + 1, gameObject.transform.position.y + 4);
            descriptionText.text = GetComponent<CardDisplay>().card.description;
        }
    }

    private void OnMouseExit()
    {
        if(countDown == 0)
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
