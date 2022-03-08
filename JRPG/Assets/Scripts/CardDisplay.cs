using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    public SpriteRenderer backgroundImage;

    public TMP_Text powerText;
    public TMP_Text gameOrderText;

    public TMP_Text nameText;
    public TMP_Text biomeText;
    public TMP_Text typeText;
    public TMP_Text descriptionText;

    void Start()
    {
        backgroundImage.sprite = card.background;

        powerText.text = card.power.ToString();
        gameOrderText.text = card.gameOrder.ToString();

        nameText.text = card.name;
        biomeText.text = card.biome;
        typeText.text = card.type;
        descriptionText.text = card.description;
    }
}
