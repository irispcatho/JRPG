using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    public SpriteRenderer backgroundImage;
    public SpriteRenderer type;
    public SpriteRenderer animal;
    public TMP_Text animalTxt;

    public TMP_Text powerText;
    public TMP_Text gameOrderText;

    public GameObject visual;
    public GameObject onCase;

    public SpriteRenderer biome;
    //public TMP_Text descriptionText;
    public TMP_Text onCaseText;
    public SpriteRenderer onCaseImage;

    public GameObject onMouseOver;

    public bool isEnemy;

    void Start()
    {
        backgroundImage.sprite = card.background;
        powerText.text = card.power.ToString();
        gameOrderText.text = card.gameOrder.ToString();
        animal.sprite = card.animal;
        animalTxt.text = card.animalTxt;
        biome.sprite = card.biome;
        type.sprite = card.type;
        onCaseImage.sprite = card.animal;
        //descriptionText.text = card.description;
        onCaseText.text = card.gameOrder.ToString();
    }
}
