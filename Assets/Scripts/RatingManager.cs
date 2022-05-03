using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatingManager : Singleton<RatingManager>
{
    [SerializeField] private GameObject ratingMenu;

    public bool rateoff = true;

    private int CherriesToRate = 15;


    public void Update()
    {
        if ((permanentUi.perm.cherries >= CherriesToRate) && (rateoff))
        {
            RateUS();
            CherriesToRate += 10;
            rateoff = false;
        }
    }

    public void RateUS()
    {
        ratingMenu.gameObject.SetActive(true);
        Application.OpenURL("market://details?id=com.xxxx.xxx");
    }
}
