using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Adds : MonoBehaviour, IUnityAdsListener
{
    public void OnUnityAdsDidError(string message)
    {
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            permanentUi.perm.CoinSaved += 20;
            permanentUi.perm.CoinText.text = permanentUi.perm.CoinSaved.ToString();
            PlayerPrefs.SetInt("Coin", permanentUi.perm.CoinSaved);

        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }

    public void OnUnityAdsReady(string placementId)
    {
    }

    public void ShowRewardedVideo()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize("3684789", true);

        if (Advertisement.IsReady("rewardedVideo"))
        {
            Advertisement.Show("rewardedVideo");
        }
    }
}
