using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public Button[] levelButtons;

    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
            {
                levelButtons[i].interactable = false;
            }
        }
    }

    public void LevelSelect(string LevelName)
    {
        SceneManager.LoadScene(LevelName);
    }
    /*public void LoadLevel1()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void LoadLevel2()
    {
        if (PlayerController.playerController.Level2 || level2 == 2)
        {
            SceneManager.LoadScene("SecondScene");
        }
    }
    public void LoadLevel3()
    {
        if (PlayerController.playerController.Level3 || level3 == 3)
        {
            SceneManager.LoadScene("ThirdScene");
        }
    }
    public void LoadLevel4()
    {
        if (PlayerController.playerController.Level4 || level4 == 4)
        {
            SceneManager.LoadScene("FourthScene");
        }
    }*/
}
