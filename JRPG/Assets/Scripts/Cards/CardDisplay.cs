using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    public Vector3 startPosition;


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

    public GameObject cadreP;
    public GameObject cadreIA;

    public GameObject damageGoP;
    public GameObject damageGoIA;

    public TMP_Text damageGoPText;
    public TMP_Text damageGoIAText;
    public TMP_Text signeDamageGoPText;
    public TMP_Text signeDamageGoIAText;

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

        damageGoPText.text = card.damage.ToString();
        damageGoIAText.text = card.damage.ToString();
        signeDamageGoPText.text = card.signeDamage;
        signeDamageGoIAText.text = card.signeDamage;
        card.showDamage = false;
    }

    public void SavePosition()
    {
        RectTransform rect = GetComponent<RectTransform>();
        startPosition = rect.position;
        print(startPosition);
    }
}
