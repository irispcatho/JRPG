using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu]
public class Card : ScriptableObject
{
    public Sprite background;
    public Sprite animal;
    public Sprite biome;
    public Sprite type;

    public int power;
    public int gameOrder;

    public string animalTxt;
    public string description;
}