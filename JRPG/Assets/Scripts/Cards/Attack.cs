using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public static Attack instance;
    public PlacedCards placedCards;

    

    private void Awake()
    {
        instance = this;
    }

}
