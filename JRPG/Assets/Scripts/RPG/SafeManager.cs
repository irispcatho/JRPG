using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeManager : MonoBehaviour
{
    public static SafeManager instance;
    public Upgrade upgrade;
    public bool isInRange;
    public GameObject[] safeSprite;
    public GameObject buttonE;
    public GameObject upgradeGo;
    public TMPro.TMP_Text upgradeTxt;

    private void Start()
    {
        instance = this;
    }
    private void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            AudioManager.instance.Play("SafeOpen");
            safeSprite[0].SetActive(false);
            safeSprite[1].SetActive(true);
            TriggerSafe();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
            buttonE.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            buttonE.SetActive(false);
        }
    }

    public void TriggerSafe()
    {
        if (!upgrade.asBeenDiscovered)
        {
            upgrade.cardAffected.powerPlayer += upgrade.attackUpgrade;
            upgrade.asBeenDiscovered = true;
            upgradeGo.SetActive(true);
            upgradeTxt.text = "La carte " + upgrade.cardAffected.frenchName + " a gagné " + upgrade.attackUpgrade + " point d'attaque";
            StartCoroutine(UpgradeTxt());
        }
    }

    IEnumerator UpgradeTxt()
    {
        yield return new WaitForSeconds(3);
        upgradeGo.SetActive(false);
    }
}