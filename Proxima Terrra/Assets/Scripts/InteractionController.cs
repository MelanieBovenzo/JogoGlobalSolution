using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    // DOOR
    [SerializeField] Transform leftDoor;
    [SerializeField] Transform rightDoor;
    private int i = 0;
    [SerializeField] bool doorClosed = false;
    [SerializeField] bool locked = false;

    // PICKUP
    [SerializeField] PlayerController playerController;

    // SLEEP
    [SerializeField] TaskController taskController;

    public string interactionType;
    
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Interaction()
    {
        switch (interactionType)
        {
            case "DoorOpener":
                if (locked == false)
                {
                    if (doorClosed)
                    {
                        openDoor();
                    }
                    else
                    {
                        closeDoor();
                    }
                }
                else
                {
                    audioSource.Play();
                }
                break;
            case "PickupRock":
                playerController.gameObject.GetComponent<AudioSource>().Play();
                Invoke("CutRock", 1f);
                break;
            case "PickupCutter":
                break;
            case "Sleep":
                if(taskController.canSleep)
                {
                    taskController.NextDay();
                }
                else
                {
                    audioSource.Play();
                }
                break;
            default:
                break;
        }
    }

    private void CutRock()
    {
        audioSource.Play();
        playerController.rockCount += 1;
        Destroy(this.gameObject);
    }

    private void openDoor()
    {
        leftDoor.transform.Translate(new Vector3(1.3f, 0, 0) * Time.deltaTime, Space.Self);
        rightDoor.transform.Translate(new Vector3(-1.3f, 0, 0) * Time.deltaTime, Space.Self);

        if (i <= 90)
        {
            Invoke("openDoor", Time.deltaTime);
            i++;
        }
        else
        {
            i = 0;
            doorClosed = false;
        }
    }

    private void closeDoor()
    {
        rightDoor.transform.Translate(new Vector3(1.3f, 0, 0) * Time.deltaTime, Space.Self);
        leftDoor.transform.Translate(new Vector3(-1.3f, 0, 0) * Time.deltaTime, Space.Self);

        if (i <= 90)
        {
            Invoke("closeDoor", Time.deltaTime);
            i++;
        }
        else
        {
            i = 0;
            doorClosed = true;
        }
    }
}