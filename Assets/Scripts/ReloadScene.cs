using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class ReloadScene : MonoBehaviour
{
    [SerializeField] private string gameOver;

    private void Start()
    {
        if (PlayerPrefs.GetInt("RemoveAds") != 1)
        {
            if (Advertisement.isSupported)
            {
                Advertisement.Initialize("3684789", false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Advertisement.IsReady())
            {
                Advertisement.Show("video");
            }
            SceneManager.LoadScene(gameOver);
            permanentUi.perm.cherries = 0;
            permanentUi.perm.cherryText.text = permanentUi.perm.cherries.ToString();
            permanentUi.perm.Pcounter = 0;
            permanentUi.perm.gemText.text = permanentUi.perm.Pcounter.ToString();
            Music.musicInstance.music.Stop();
        }
    }
}
