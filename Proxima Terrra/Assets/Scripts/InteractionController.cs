using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] Transform leftDoor;
    [SerializeField] Transform rightDoor;
    public string interactionType;
    private int i;

    public void Interaction()
    {
        switch (interactionType)
        {
            case "DoorOpener":
                openDoor();
                break;
            default:
                break;
        }
    }

    private void openDoor()
    {
        leftDoor.position += new Vector3(-1.4f, 0, 0) * Time.deltaTime;
        rightDoor.position += new Vector3(1.4f, 0, 0) * Time.deltaTime;

        if (i <= 90)
        {
            Invoke("openDoor", Time.deltaTime);
            i++;
        }
    }
}
