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

    private int i;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void WriteLine(int index)
    {
        i = index;
        StartCoroutine("WriteLineCoroutine");
    }

    public void SkipLine()
    {
        StopAllCoroutines();
        dialogueText.text = lines[i];
        _isWriting = false;
    }

    public IEnumerator WriteLineCoroutine()
    {
        foreach (char c in lines[i].ToCharArray())
        {
            _isWriting = true;
            dialogueText.text += c;
            yield return new WaitForSeconds(dialogueController.textSpeed);
            _isWriting = false;
        }
    }
}
