using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSceneSpawner : MonoBehaviour
{
    private int PSchoice;
    private int Pchoice;
    private GameObject Fox1;
    private GameObject Fox2;

    private void Start()
    {
        Fox1 = PlayerSelection.playerSelection.Player1;
        Fox2 = PlayerSelection.playerSelection.Player2;
        PSchoice = PlayerSelectorChoice.playerSelectorChoice.PSchoice;
        Pchoice = PlayerPrefs.GetInt("Pchoice");
        Time.timeScale = 1f;

        if (PSchoice == 0 || Pchoice == 0)
        {
            Instantiate(Fox1, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (PSchoice == 1 || Pchoice == 1)
        {
            Instantiate(Fox2, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
