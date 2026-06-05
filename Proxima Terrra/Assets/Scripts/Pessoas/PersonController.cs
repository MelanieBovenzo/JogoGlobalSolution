using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;
using System;

public class PersonController : MonoBehaviour
{
    // CONTROL
    [SerializeField] DialogueController dialogueController;
    [HideInInspector] public bool _isWriting = true;

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
            case 'j':
                personNameText.text = "John";
                audioSource.clip = npcAudio;
                break;
            case 's':
                personNameText.text = "Sarah";
                audioSource.clip = npcAudio;
                break;
            case 'c':
                personNameText.text = "Carl";
                audioSource.clip = npcAudio;
                break;
            case 'a':
                personNameText.text = "Anahí";
                audioSource.clip = protaAudio;
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
        foreach (char c in lines[i].Substring(1).ToCharArray())
        {
            _isWriting = true;
            dialogueText.text += c;
            audioSource.Play();
            yield return new WaitForSeconds(dialogueController.textSpeed);
            _isWriting = false;
        }
    }
}
