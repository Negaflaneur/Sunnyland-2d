using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueScript : MonoBehaviour
{
    public GameObject dialogueMenu;
    public int index;
    public TextMeshProUGUI TextDisplay;
    public string[] sentences;
    public float TypingSpeed = .01f;
    public GameObject Cloud;
    public bool DialogueIsActive;
    // public GameObject ContinueBTN;
    public static DialogueScript dialogueScript;

    private void Start()
    {
        if (!dialogueScript)
        {
            dialogueScript = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        DialogueIsActive = false;
        //ContinueBTN.SetActive(false);
        //ContinueBTN.SetActive(true);
        StartCoroutine(Type());
    }

    private void Update()
    {
        if (TextDisplay.text == sentences[index])
        {
            NextSentence();
        }
        if (index == sentences.Length - 1)
        {
            dialogueMenu.SetActive(false);
            Cloud.SetActive(false);
            DialogueIsActive = false;
        }
    }

    private IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            TextDisplay.text += letter;
            yield return new WaitForSeconds(TypingSpeed);
        }
    }

    public void NextSentence()
    {
        //ContinueBTN.SetActive(false);
        if (index < sentences.Length - 1)
        {
            index += 1;
            TextDisplay.text = "";
            StartCoroutine(Type());
        }
    }

    public void SetDialogueActive()
    {
        dialogueMenu.SetActive(true);
        Cloud.SetActive(true);
        DialogueIsActive = true;
    }
    public void DisableDialogue()
    {
        dialogueMenu.SetActive(false);
    }
}
