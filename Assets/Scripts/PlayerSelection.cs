using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Security.Cryptography;
using UnityEngine.UI;

public class PlayerSelection : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;
    public GameObject canvas;
    public GameObject mainMenu;
    public GameObject CherryMenu;
    public Button EvilFoxBTN;
    public GameObject PriceText;
    public bool SkinIsUnlocked;
    public bool b;

    public int choice;
    private int SkinIsReady;

    public static PlayerSelection playerSelection;

    void Awake()
    {
        Time.timeScale = 0f;
    }

    private void Start()
    {
        if (!playerSelection)
        {
            playerSelection = this;
        }
        else
        {
            Destroy(gameObject);
        }
        SkinIsReady = PlayerPrefs.GetInt("SkinIsReady");
        if (SkinIsReady == 0)
        {
            EvilFoxBTN.interactable = false;
        }
        else if (SkinIsReady == 1)
        {
            EvilFoxBTN.interactable = true;
        }

        if (EvilFoxBTN.interactable == true)
        {
            Destroy(PriceText);
        }
    }

    private void Update()
    {
        if (PlayerSelectorChoice.playerSelectorChoice.PSchoice == 0)
        {
            Instantiate(Player1, transform.position, Quaternion.identity);
            Time.timeScale = 1f;
            Destroy(gameObject);
        }
        if (PlayerSelectorChoice.playerSelectorChoice.PSchoice == 1)
        {
            Instantiate(Player2, transform.position, Quaternion.identity);
            Time.timeScale = 1f;
            Destroy(gameObject);
        }
    }

    public void FoxButton()
    {
        PlayerSelectorChoice.playerSelectorChoice.PSchoice = 0;
        PlayerPrefs.SetInt("Pchoice", PlayerSelectorChoice.playerSelectorChoice.PSchoice);
        Destroy(canvas);
        mainMenu.SetActive(true);
        CherryMenu.SetActive(true);
    }

    public void EvilFoxButton()
    {
        if (EvilFoxBTN.IsInteractable())
        {
            PlayerSelectorChoice.playerSelectorChoice.PSchoice = 1;
            PlayerPrefs.SetInt("Pchoice", PlayerSelectorChoice.playerSelectorChoice.PSchoice);
            Destroy(canvas);
            mainMenu.SetActive(true);
            CherryMenu.SetActive(true);
        }
    }
}
