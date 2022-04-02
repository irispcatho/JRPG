using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;
    public DialogManager dialogManager;
    public bool isInRange;
    public GameObject pnj;
    public GameObject pressE;

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
            TriggerDialog();

        if (isInRange && Input.GetKeyDown(KeyCode.Space))
            DialogManager.instance.DisplayNextSentence();

        if (isInRange && Input.GetKeyDown(KeyCode.Escape))
            DialogManager.instance.EndDialog();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isInRange = true;
            pressE.SetActive(true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isInRange = false;
            pressE.SetActive(false);
        }
    }

    void TriggerDialog()
    {
        pressE.SetActive(false);
        DialogManager.instance.StartDialog(dialog, pnj);
    }
}
