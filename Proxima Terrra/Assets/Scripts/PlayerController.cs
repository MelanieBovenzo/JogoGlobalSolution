using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // MOVEMENT
    private Vector2 movement;
    private Vector3 currentMovement;
    [SerializeField] CharacterController characterController;
    [SerializeField] float moveSpeed;
    [SerializeField] float gravity;

    // LOOKING
    private Vector2 mouseRotation;
    private float verticalRotation;
    [SerializeField] Camera mainCamera;
    [SerializeField] float upDownMax;
    [SerializeField] float cameraSensitivity;

    // INTERACTION
    [SerializeField] PlayerInput playerInput;
    [SerializeField] DialogueController dialogueController;
    private bool interacting;
    private bool _canInteract;
    [SerializeField] TaskController taskController;
    [SerializeField] TextMeshProUGUI interactionText;

    // ITEMS
    public bool hasCutter;
    public int rockCount;
    [HideInInspector] public bool hasTranslator;

    // MENUS
    private bool options;
    [SerializeField] GameObject optionsCanvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentMovement.y = gravity;
    }

    void FixedUpdate()
    {
        // MOVING CHARACTER
        Vector3 direction = transform.TransformDirection(new Vector3(movement.x, 0, movement.y)).normalized;
        currentMovement.x = direction.x * moveSpeed;
        currentMovement.z = direction.z * moveSpeed;

        characterController.Move(currentMovement * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        cameraSensitivity = Settings.mouseSensitivity;

        // HORIZONTAL ROTATION
        transform.Rotate(0, mouseRotation.x * Time.deltaTime * cameraSensitivity, 0);

        // VERTICAL ROTATION
        verticalRotation = Mathf.Clamp(verticalRotation - (mouseRotation.y * Time.deltaTime * cameraSensitivity), -upDownMax, upDownMax);
        mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2.5f))
        {
            if (_canInteract)
            {
                if (hit.collider.tag == "Person")
                {
                    if (interacting)
                    {
                        interactionText.text = string.Empty;
                        dialogueController._isExamining = false;
                        dialogueController._isTalking = true;

                        dialogueController.personController = hit.collider.GetComponent<PersonController>();
                        dialogueController.StartDialogue();

                        SwitchToDialogue();
                    }
                    else
                    {
                        interactionText.text = "Pressione [E] para conversar";
                    }
                }
                else if (hit.collider.tag == "Examine")
                {
                    if (interacting)
                    {
                        interactionText.text = string.Empty;
                        dialogueController._isExamining = true;
                        dialogueController._isTalking = false;

                        dialogueController.examineController = hit.collider.GetComponent<ExamineController>();
                        dialogueController.examineController.Examine();

                        SwitchToDialogue();
                    }
                    else
                    {
                        interactionText.text = "Pressione [E] para examinar";
                    }
                }
                else if (hit.collider.tag == "Interact")
                {
                    if (interacting)
                    {
                        interactionText.text = string.Empty;
                        hit.collider.GetComponent<InteractionController>().Interaction();
                    }
                    else
                    {
                        interactionText.text = "Pressione [E] para interagir";
                    }
                }
                else
                {
                    interactionText.text = string.Empty;
                }
            }           
        }
        else
        {
            interactionText.text = string.Empty;
        }


        if (interacting)
        {
            _canInteract = false;
        }
        else
        {
            _canInteract = true;
        }

        if (options)
        {
            optionsCanvas.SetActive(true);

            Time.timeScale = 0;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            playerInput.actions.FindActionMap("Player").Disable();
            playerInput.actions.FindActionMap("Options").Enable();
        }
    }

    private void SwitchToDialogue()
    {
        playerInput.actions.FindActionMap("Player").Disable();
        playerInput.actions.FindActionMap("Dialogue").Enable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "TaskTrigger" && taskController.task2Started)
        {
            taskController.task2Completed = true;
            Destroy(other.gameObject);
        }
    }

    public void GetMove(InputAction.CallbackContext value)
    {
        movement = value.ReadValue<Vector2>();
    }

    public void GetLook(InputAction.CallbackContext value)
    {
        mouseRotation = value.ReadValue<Vector2>();
    }

    public void GetInteract(InputAction.CallbackContext value)
    {
        interacting = value.performed;
    }

    public void GetOptions(InputAction.CallbackContext value)
    {
        options = value.performed;
    }
}
