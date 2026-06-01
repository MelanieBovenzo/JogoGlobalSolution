using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] Transform leftDoor;
    [SerializeField] Transform rightDoor;
    public string interactionType;
    private int i = 0;
    [SerializeField] bool doorClosed = false;
    [SerializeField] bool locked = false;
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
            default:
                break;
        }
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