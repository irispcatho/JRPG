using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoriesManager : MonoBehaviour
{
    public PlacedCards pC;
    public List<SpriteRenderer> victories;

    private void Update()
    {
        if (pC.round == 0)
        {
            if (pC.whoWon == 0)
                victories[2].color = new Color(0, 0.7f, 0.05f, 1);
            else if (pC.whoWon == 1)
                victories[0].color = new Color(0.73f, 0.06f, 0.2f, 1);
        }

        if (pC.round == 1)
        {
            if (pC.whoWon == 0)
            {
                if (pC.numberWinPlayer == 2)
                    victories[1].color = new Color(0, 0.7f, 0.05f, 1);
                else if(pC.numberWinPlayer == 1)
                    victories[2].color = new Color(0, 0.7f, 0.05f, 1);
            }

            if(pC.whoWon == 1)
            {
                if(pC.numberWinIA == 2)
                    victories[1].color = new Color(0.73f, 0.06f, 0.2f, 1);
                else if (pC.numberWinIA == 1)
                    victories[0].color = new Color(0.73f, 0.06f, 0.2f, 1);
            }
        }

        if(pC.round == 2)
        {
            if (pC.whoWon == 0)
                victories[1].color = new Color(0, 0.7f, 0.05f, 1);
            else if (pC.whoWon == 1)
                victories[1].color = new Color(0.73f, 0.06f, 0.2f, 1);
        }
    }
}
