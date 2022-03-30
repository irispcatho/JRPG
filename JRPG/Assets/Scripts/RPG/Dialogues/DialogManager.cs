using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    public TMP_Text nameText;
    public TMP_Text dialogText;

    public float letterSpeed = 0.05f;

    public bool discoverMap01 = false;

    public GameObject dialogUI;

    public GameObject[] maps;
    public GameObject[] pnjs;

    public GameObject[] wall;

    private Queue<string> sentences;
    private Queue<string> names;

    private void Awake()
    {
        instance = this;

        sentences = new Queue<string>();
        names = new Queue<string>();

        dialogUI.SetActive(false);
    }

    public void StartDialog(Dialog dialog, GameObject pnj)
    {
        ZoomCamera.instance.zoomActive = true;

        if(pnj == pnjs[0])
        {
            discoverMap01 = true;            
        }

        PlayerMovement.instance.moveSpeed = 0;
        PlayerMovement.instance.animator.enabled = false;

        dialogUI.SetActive(true);

        names.Clear();
        sentences.Clear();

        foreach (string name in dialog.names)
        {
            names.Enqueue(name);
        }
        foreach (string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string name = names.Dequeue();
        nameText.text = name;
        
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(letterSpeed);
        }
    }

    void EndDialog()
    {
        ZoomCamera.instance.zoomActive = false;

        if (discoverMap01)
        {
            SceneManager.LoadScene("CardSystem", LoadSceneMode.Additive);
        }
        dialogUI.SetActive(false);
        PlayerMovement.instance.moveSpeed = PlayerMovement.instance.initMoveSpeed;
        PlayerMovement.instance.animator.enabled = true;
    }
}
