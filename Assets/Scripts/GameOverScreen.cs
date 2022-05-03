using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class GameOverScreen : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene(PlayerController.playerController.PreviousSceneIndex);
        DialogueScript.dialogueScript.DisableDialogue();

        if (PlayerPrefs.GetInt("RemoveAds") != 1)
        {
            Advertisement.Initialize("3684789", true);

            if (Advertisement.IsReady("video"))
            {
                Advertisement.Show("video");
            }
        }
    }

    public void MenuQuit()
    {
        SceneManager.LoadScene("MainMenu");
        Advertisement.Initialize("3684789", true);

        if (Advertisement.IsReady("video"))
        {
            Advertisement.Show("video");
        }
    }
}
