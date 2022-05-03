using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class permanentUi : MonoBehaviour
{
    public int cherries = 0;
    public TextMeshProUGUI cherryText;
    public int Pcounter = 0;
    public TextMeshProUGUI gemText;
    public int CoinCounter = 0;
    public int CoinSaved;
    public TextMeshProUGUI CoinText;
    public float healthPoints = 100f;
    public TextMeshProUGUI Health;
    public TextMeshProUGUI Armor;
    public float ArmorPoints = 0f;
    public bool HasArmor = false;

    public static permanentUi perm;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (!perm)
        {
            perm = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
