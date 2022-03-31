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

    public bool playCombat1 = false;

    public GameObject dialogUI;

    public GameObject[] maps;
    public GameObject[] pnjs;

    public GameObject[] wall;

    private Queue<string> sentences;
    private Queue<string> names;

    public SimpleBlit _simpleBlit;

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
            playCombat1 = true;            
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

        if (playCombat1)
        {
            SceneManager.LoadScene("CardSystem", LoadSceneMode.Additive);
            //StartCoroutine(WaitOneFrame(2));

        }


        /*IEnumerator WaitOneFrame(float timeToWait)
        {
            //_simpleBlit.transitionIsActive = true;
            //yield return new WaitForSeconds(timeToWait);
            //_simpleBlit.cutoffVal = 0f;
            //_simpleBlit.TransitionMaterial.SetFloat("_Cutoff", _simpleBlit.cutoffVal);
            //_simpleBlit.transitionIsActive = false;
            //SceneManager.LoadScene("CardSystem", LoadSceneMode.Additive);
        }
        

        dialogUI.SetActive(false);
        //PlayerMovement.instance.moveSpeed = PlayerMovement.instance.initMoveSpeed;
        //PlayerMovement.instance.animator.enabled = true;
        */
    }

    /*private void Start()
    {
        //_simpleBlit.cutoffVal = 0f;
        //_simpleBlit.TransitionMaterial.SetFloat("_Cutoff", _simpleBlit.cutoffVal);
        _simpleBlit.transitionIsActive = false;
    }
    */
}
