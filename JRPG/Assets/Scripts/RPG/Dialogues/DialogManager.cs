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
    public bool playCombat2 = false;
    public bool playCombat3 = false;

    public GameObject dialogUI;

    public GameObject[] maps;
    public GameObject[] pnjs;

    public GameObject[] wall;

    public Queue<string> sentences;
    private Queue<string> names;

    public SimpleBlit _simpleBlit;


    public bool combatAlreadyLauched = false;
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
        if (pnj == pnjs[0])
            playCombat1 = true;
        if (pnj == pnjs[1])
            playCombat2 = true;
        if (pnj == pnjs[2])
            playCombat3 = true;

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
            EndDialogWithCombat();
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

    public void EndDialog()
    {
        ZoomCamera.instance.zoomActive = false;
        dialogUI.SetActive(false);
        PlayerMovement.instance.moveSpeed = PlayerMovement.instance.initMoveSpeed;
        PlayerMovement.instance.animator.enabled = true;
    }
    public void EndDialogWithCombat()
    {
        ZoomCamera.instance.zoomActive = false;

        if (playCombat1)
            StartCoroutine(WaitOneFrame(2, "Fight1"));
        if (playCombat2)
            StartCoroutine(WaitOneFrame(2, "Fight2"));
        if (playCombat3)
            StartCoroutine(WaitOneFrame(2, "Fight3"));


        IEnumerator WaitOneFrame(float timeToWait, string scene)
        {
            _simpleBlit.transitionIsActive = true;
            yield return new WaitForSeconds(timeToWait);
            _simpleBlit.cutoffVal = 0f;
            _simpleBlit.TransitionMaterial.SetFloat("_Cutoff", _simpleBlit.cutoffVal);
            _simpleBlit.transitionIsActive = false;
            if(!combatAlreadyLauched)
            {
                AudioManager.instance.Play("FightLaunch");
                SceneManager.LoadScene(scene, LoadSceneMode.Additive);
                combatAlreadyLauched = true;
            }
        }
        

        dialogUI.SetActive(false);
        PlayerMovement.instance.moveSpeed = PlayerMovement.instance.initMoveSpeed;
        PlayerMovement.instance.animator.enabled = true;
    }

    private void Start()
    {
        _simpleBlit.cutoffVal = 0f;
        _simpleBlit.TransitionMaterial.SetFloat("_Cutoff", _simpleBlit.cutoffVal);
        _simpleBlit.transitionIsActive = false;
    }
}
