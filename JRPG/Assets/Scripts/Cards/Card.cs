using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu]
public class Card : ScriptableObject
{
    public string frenchName;
    public Sprite background;
    public Sprite animalG;
    public Sprite animalR;
    public Sprite biome;
    public Sprite typeImage;
    public Sprite pattern;

    public int powerIA;
    public int powerPlayer;
    public int gameOrder;

    public string animalTxt;
    public string description;

    public CaseSlot cell;

    public bool isEnnemy;
    public bool isDead;
    public bool isPlaced;

    public int damage;
    public string signeDamage;

    public bool showDamage;
}