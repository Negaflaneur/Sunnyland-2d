using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class DailyRewardScript : MonoBehaviour
{
    public float msToWait = 10000.0f;
    public Text ChestTimer;
    public Button ChestButton;
    private ulong LastRewardGained;

    public static DailyRewardScript dailyRewardScript;
    public bool SkinIsReady;

    private void Awake()
    {
        SkinIsReady = false;
    }
    private void Start()
    {
        if (!dailyRewardScript)
        {
            dailyRewardScript = this;
        }
        else
        {
            Destroy(gameObject);
        }

        LastRewardGained = ulong.Parse(PlayerPrefs.GetString("LastRewardGained"));
        permanentUi.perm.CoinSaved = PlayerPrefs.GetInt("Coin");
        permanentUi.perm.CoinText.text = permanentUi.perm.CoinSaved.ToString();

        if (SkinIsReady)
        {
            return;
        }
        else if (permanentUi.perm.CoinSaved < 1000 && !SkinIsReady)
        {
            PlayerPrefs.SetInt("SkinIsReady", 0);
        }
        else if (permanentUi.perm.CoinSaved >= 1000 && !SkinIsReady)
        {
            SkinIsReady = true;
            permanentUi.perm.CoinSaved -= 1000;
            permanentUi.perm.CoinText.text = permanentUi.perm.CoinSaved.ToString();
            PlayerPrefs.SetInt("SkinIsReady", 1);
        }



        if (!IsChestReady())
            ChestButton.interactable = false;
    }

    private void Update()
    {
        if (!ChestButton.IsInteractable())
        {
            if (IsChestReady())
            {
                ChestButton.interactable = true;
                return;
            }

            // Set the timer
            ulong diff = ((ulong)DateTime.Now.Ticks - LastRewardGained);
            ulong m = diff / TimeSpan.TicksPerMillisecond;
            float secondsLeft = (float)(msToWait - m) / 1000.0f;

            string r = "";
            // Hours
            r += ((int)secondsLeft / 3600).ToString() + "h ";
            secondsLeft -= ((int)secondsLeft / 3600) * 3600;
            // Minutes
            r += ((int)secondsLeft / 60).ToString("00") + "m ";
            // Seconds
            r += (secondsLeft % 60).ToString("00") + "s";
            ChestTimer.text = r;
        }
    }
    public void ChestClick()
    {
        LastRewardGained = (ulong)DateTime.Now.Ticks;
        PlayerPrefs.SetString("LastRewardGained", LastRewardGained.ToString());
        ChestButton.interactable = false;

        //reward
        permanentUi.perm.CoinSaved += 100;
        permanentUi.perm.CoinCounter = permanentUi.perm.CoinSaved;
        permanentUi.perm.CoinText.text = permanentUi.perm.CoinCounter.ToString();
        PlayerPrefs.SetInt("Coin", permanentUi.perm.CoinCounter);
    }

    private bool IsChestReady()
    {
        ulong diff = ((ulong)DateTime.Now.Ticks - LastRewardGained);
        ulong m = diff / TimeSpan.TicksPerMillisecond;
        float secondsLeft = (float)(msToWait - m) / 1000.0f;

        if (secondsLeft < 0)
        {
            ChestTimer.text = "Ready";
            return true;
        }
        return false;
    }
}
