using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TutoManager : MonoBehaviour
{
    public Dialog dialog;
    public Queue<string> sentences;
    public TMP_Text nameText;
    public TMP_Text dialogText;
    public GameObject dialogUI;
    public float letterSpeed = 0.05f;

    private void Awake()
    {
        sentences = new Queue<string>();
    }
    private void Start()
    {
        StartDialogTuto(dialog);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            DisplayNextSentenceTuto();        
    }
    private void StartDialogTuto(Dialog dialog)
    {
        nameText.text = dialog.name;
        sentences.Clear();
        foreach (string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentenceTuto();
    }

    public void DisplayNextSentenceTuto()
    {
        if(sentences.Count == 6)
        {
            dialogUI.SetActive(false);
            //return;
        }
        if (sentences.Count == 0)
        { 
            EndDialog();
            return;
        }

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

    public void EndDialog()
    {
        dialogUI.SetActive(false);
        SceneManager.LoadScene("TopDown");
    }
}
