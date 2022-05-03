using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ShopScript : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject ShopMenu;
    private int[] price = new int[] { 3, 10, 5, 7, 5, 2 };
    public GameObject[] items;
    public TextMeshProUGUI[] labels;

    public static ShopScript shopScript;
    //int index;

    private void Start()
    {
        if (!shopScript)
        {
            shopScript = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        for(var index = 0; index < price.Length; index++)
        {
            labels[index].text = price[index].ToString();
        }
    }

    public void SetShopMenuActive()
    {
        MainMenu.SetActive(false);
        ShopMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void BackBTN()
    {
        MainMenu.SetActive(true);
        ShopMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ArmorBTN()
    {
        Fun(0);
    }
    public void ChestBTN()
    {
        Fun(1);
    }
    public void JumpPowerUpBTN()
    {
        Fun(2);
    }
    public void ScalePowerUpBTN()
    {
        Fun(3);
    }
    public void SpeedPowerUpBTN()
    {
        Fun(4);
    }
    public void TimePowerUpBTN()
    {
        Fun(5);
    }
    private void Fun(int index)
    {
        if (permanentUi.perm.cherries >= price[index])
        {
            Instantiate(items[index], transform.position, Quaternion.identity);
            permanentUi.perm.cherries -= price[index];
            permanentUi.perm.cherryText.text = permanentUi.perm.cherries.ToString();
            BackBTN();
        }
    }
}
