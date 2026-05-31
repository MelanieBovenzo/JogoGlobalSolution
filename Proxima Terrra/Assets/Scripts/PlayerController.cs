using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

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
        // HORIZONTAL ROTATION
        transform.Rotate(0, mouseRotation.x * Time.deltaTime * cameraSensitivity, 0);

        // VERTICAL ROTATION
        verticalRotation = Mathf.Clamp(verticalRotation - (mouseRotation.y * Time.deltaTime * cameraSensitivity), -upDownMax, upDownMax);
        mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2.5f))
        {
            if (interacting && _canInteract)
            {

                if (hit.collider.tag == "Person")
                {
                    dialogueController._isExamining = false;
                    dialogueController._isTalking = true;

                    dialogueController.personController = hit.collider.GetComponent<PersonController>();
                    dialogueController.StartDialogue();

                    SwitchToDialogue(hit);
                }
                if (hit.collider.tag == "Examine")
                {
                    dialogueController._isExamining = true;
                    dialogueController._isTalking = false;

                    dialogueController.examineController = hit.collider.GetComponent<ExamineController>();

                    SwitchToDialogue(hit);
                }
                if (hit.collider.tag == "Interact")
                {
                    hit.collider.GetComponent<InteractionController>().Interaction();
                }
            }
        }

        if (interacting)
        {
            _canInteract = false;
        }
        else
        {
            _canInteract = true;
        }
    }

    private void SwitchToDialogue(RaycastHit hit)
    {
        playerInput.actions.FindActionMap("Player").Disable();
        playerInput.actions.FindActionMap("Dialogue").Enable();

        transform.LookAt(hit.collider.transform);
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


}
