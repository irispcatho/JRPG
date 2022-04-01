using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfosDisplay : MonoBehaviour
{
    public InfosCard infosCard;

    public TMP_Text cardName;
    public TMP_Text power;
    public TMP_Text order;
    public TMP_Text description;
    public SpriteRenderer pattern;

    void Update()
    {
        cardName.text = infosCard.cardName;
        power.text = infosCard.power.ToString();
        order.text = infosCard.order.ToString();
        description.text = infosCard.description;
        pattern.sprite = infosCard.pattern;
    }
}
