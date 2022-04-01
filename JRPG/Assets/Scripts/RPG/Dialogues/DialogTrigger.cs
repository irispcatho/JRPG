using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;
    public DialogManager dialogManager;
    public bool isInRange;
    public GameObject pnj;

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            TriggerDialog();
        }

        if (isInRange && Input.GetKeyDown(KeyCode.Space))
            DialogManager.instance.DisplayNextSentence();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isInRange = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isInRange = false;
        }
    }

    void TriggerDialog()
    {
        DialogManager.instance.StartDialog(dialog, pnj);
    }
}
