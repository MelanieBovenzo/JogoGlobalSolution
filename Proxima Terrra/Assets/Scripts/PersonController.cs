using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;
using System;

public class PersonController : MonoBehaviour
{
    public string personName;
    public string[] lines;
    public bool _isWriting;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] DialogueController dialogueController;
    [SerializeField] TextMeshProUGUI personNameText;
    [SerializeField] AudioClip protaAudio;
    [SerializeField] AudioClip humanAudio;
    [SerializeField] AudioClip alienAudio;
    private AudioSource audioSource;

    private int i;
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
            case 't':
                personNameText.text = "Teste";
                audioSource.clip = humanAudio;
                break;
            case 'p':
                personNameText.text = "Prota";
                audioSource.clip = protaAudio;
                break;
        }
        StartCoroutine("WriteLineCoroutine");
    }

    public void SkipLine()
    {
        StopAllCoroutines();
        audioSource.Play();
        dialogueText.text = lines[i];
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
