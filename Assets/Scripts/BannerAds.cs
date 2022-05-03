using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAds : MonoBehaviour
{
    IEnumerator Start()
    {
        if (PlayerPrefs.GetInt("RemoveAds") != 1)
        {
            Advertisement.Initialize("3684789", true);

            while (!Advertisement.IsReady("banner"))
                yield return null;

            Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
            Advertisement.Banner.Show("banner");
        }
        Debug.Log(PlayerPrefs.GetInt("RemoveAds"));
    }

}
