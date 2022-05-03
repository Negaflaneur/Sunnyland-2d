using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyRewardTurnOn : MonoBehaviour
{
    public GameObject DRMenu;

    public void TurnDRMenuON()
    {
        DRMenu.SetActive(true);
    }
}
