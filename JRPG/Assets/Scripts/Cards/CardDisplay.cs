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
    public SpriteRenderer animalG;
    public SpriteRenderer animalR;
    public TMP_Text animalTxt;

    public GameObject cardIA;

    public TMP_Text powerText;
    public TMP_Text gameOrderText;

    public GameObject visual;
    public GameObject onCase;
    public GameObject onCaseIA;

    public SpriteRenderer biome;
    //public TMP_Text descriptionText;
    public TMP_Text onCaseTextOrder;
    public TMP_Text onCaseTextPower;
    public SpriteRenderer onCaseImage;

    public TMP_Text onCaseTextIAOrder;
    public TMP_Text onCaseTextIAPower;
    public SpriteRenderer onCaseImageIA;

    public GameObject onMouseOver;

    void Start()
    {
        backgroundImage.sprite = card.background;
        powerText.text = card.power.ToString();
        gameOrderText.text = card.gameOrder.ToString();
        animalG.sprite = card.animalG;
        animalR.sprite = card.animalR;
        animalTxt.text = card.animalTxt;
        biome.sprite = card.biome;
        type.sprite = card.type;
        //descriptionText.text = card.description;
        onCaseImage.sprite = card.animalG;
        onCaseTextOrder.text = card.gameOrder.ToString();
        onCaseTextPower.text = card.power.ToString();

        onCaseImageIA.sprite = card.animalR;
        onCaseTextIAOrder.text = card.gameOrder.ToString();
        onCaseTextIAPower.text = card.power.ToString();
    }


        

}
