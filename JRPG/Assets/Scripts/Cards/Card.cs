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
    public Sprite type;
    public string typeTxt;

    public int power;
    public int gameOrder;

    public string animalTxt;
    public string description;

    public CaseSlot cell;

    public bool isEnemy;
    public bool isDead;

    public int damage;
    public string signeDamage;

    public bool showDamage;
}