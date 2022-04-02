using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeManager : MonoBehaviour
{
    public static SafeManager instance;
    public Upgrade upgrade;
    public bool isInRange;
    public GameObject[] safeSprite;

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
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
        }
    }

    private void TriggerSafe()
    {
        upgrade.asBeenDiscovered = true;
        upgrade.cardAffected.powerPlayer += upgrade.attackUpgrade;
    }
}