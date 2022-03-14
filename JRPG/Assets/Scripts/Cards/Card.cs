using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu]
public class Card : ScriptableObject
{
    public int index;

    public Sprite background;

    public int power;
    public int gameOrder;

    public new string name;
    public string biome;
    public string type;
    public string description;
    public string onCase;
}