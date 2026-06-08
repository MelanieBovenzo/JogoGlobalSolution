using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class PersonController : MonoBehaviour
{
    // CONTROL
    [SerializeField] DialogueController dialogueController;
    [HideInInspector] public bool _isWriting = true;
    [SerializeField] PlayerController playerController;

    // UI
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI personNameText;

    // AUDIO
    [SerializeField] AudioClip protaAudio;
    [SerializeField] AudioClip npcAudio;
    private AudioSource audioSource;

    // LINES
    public int[] dialogueIndexes = new int[] { 0, 1 };
    public string[] lines;
    [HideInInspector] public int i;
    private bool isAlien = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void WriteLine(int index)
    {

        i = index;
        switch (lines[i][0])
        {
            case 'b':
                personNameText.text = "Billy";
                audioSource.clip = npcAudio;
                isAlien = false;
                break;
            case 'h':
                personNameText.text = "Hana";
                audioSource.clip = npcAudio;
                isAlien = false;
                break;
            case 'c':
                personNameText.text = "Capitão Ford";
                audioSource.clip = npcAudio;
                isAlien = false;
                break;
            case 'a':
                personNameText.text = "Anahí";
                audioSource.clip = protaAudio;
                isAlien = false;
                break;
            case 'k':
                personNameText.text = "Kr'r";
                audioSource.clip = npcAudio;
                isAlien = true;
                break;
        }
        StartCoroutine("WriteLineCoroutine");
    }

    public void SkipLine()
    {
        StopAllCoroutines();
        audioSource.Play();
        dialogueText.text = lines[i].Substring(1);
        _isWriting = false;
    }

    public IEnumerator WriteLineCoroutine()
    {
        if (!isAlien || playerController.hasTranslator)
        {
            foreach (char c in lines[i].Substring(1).ToCharArray())
            {
                _isWriting = true;
                dialogueText.text += c;
                audioSource.Play();
                yield return new WaitForSeconds(dialogueController.textSpeed);
                _isWriting = false;
            }
        }
        else
        {
            foreach (char c in lines[i].Substring(1).ToCharArray())
            {
                _isWriting = true;
                switch (Random.Range(0, 5))
                {
                    case 0:
                        dialogueText.text += "%";
                        break;
                    case 1:
                        dialogueText.text += "*";
                        break;
                    case 2:
                        dialogueText.text += "#";
                        break;
                    case 3:
                        dialogueText.text += "&";
                        break;
                    case 4:
                        dialogueText.text += ">";
                        break;
                }
                audioSource.Play();
                yield return new WaitForSeconds(dialogueController.textSpeed);
                _isWriting = false;
            }
        }
    }
}
