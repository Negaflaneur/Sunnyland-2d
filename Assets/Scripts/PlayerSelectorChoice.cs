using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectorChoice : MonoBehaviour
{
    public static PlayerSelectorChoice playerSelectorChoice;
    public int PSchoice = 2;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (!playerSelectorChoice)
        {
            playerSelectorChoice = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
