using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;
using System;
using System.Linq;
using Microsoft.Unity.VisualStudio.Editor;

public class DialogueController : MonoBehaviour
{
    // REFERENCES
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI examineTextComponent;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] PlayerController playerController;
    [SerializeField] Canvas dialogueCanvas;
    [SerializeField] Canvas examineCanvas;

    // CONTROL STUFF
    private bool next;
    private bool _canNext;
    [HideInInspector] public bool _isExamining;
    [HideInInspector] public bool _isTalking;

    // DIALOGUE
    private int i;
    private int index;
    public float textSpeed;
    [HideInInspector] public PersonController personController;
    [HideInInspector] public ExamineController examineController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (next && _canNext)
        {
            if (_isExamining)
            {
                playerInput.actions.FindActionMap("Dialogue").Disable();
                playerInput.actions.FindActionMap("Player").Enable();

                examineCanvas.gameObject.SetActive(false);
            }
            if (_isTalking)
            {
                if (personController._isWriting == false)
                {
                    if (personController.dialogueIndexes.Contains(i + 1))
                    {
                        ++i;
                        index = personController.dialogueIndexes[i];

                        dialogueText.text = string.Empty;

                        personController.WriteLine(index);
                    }
                    else
                    {
                        dialogueCanvas.gameObject.SetActive(false);
                        playerInput.actions.FindActionMap("Dialogue").Disable();
                        playerInput.actions.FindActionMap("Player").Enable();
                    }
                }
                else
                {
                    personController.SkipLine();
                }
            }
        }

        if (next)
        {
            _canNext = false;
        }
        else
        {
            _canNext = true;
        }
    }

    public void StartDialogue()
    {
        i = 0;

        index = personController.dialogueIndexes[i];

        dialogueCanvas.gameObject.SetActive(true);

        dialogueText.text = string.Empty;

        personController.WriteLine(index);
    }



    public void GetNext(InputAction.CallbackContext value)
    {
        next = value.performed;
    }
}
