using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateBox : MonoBehaviour
{
    public GameObject RatingMenu;
    public void NoThanksBTN()
    {
        RatingMenu.SetActive(false);
    }

    public void LaterBTN()
    {
        RatingMenu.SetActive(false);
        RatingManager.Instance.rateoff = true;
    }
}
