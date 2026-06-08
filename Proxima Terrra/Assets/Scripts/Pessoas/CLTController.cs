using UnityEngine;

public class CLTController : MonoBehaviour
{
    [SerializeField] PersonController personController;
    [SerializeField] TaskController taskController;

    private bool talked = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        personController.dialogueIndexes = new int[] { 0, 1, 2, 3, 4, 5 };
    }

    // Update is called once per frame
    void Update()
    {
        if (personController._isWriting == false)
        {
            switch (personController.i + 1)
            {
                case 5:
                    taskController.extraTask1 = "Saia da nave";
                    if (!talked)
                    {
                        talked = true;
                        taskController.talkedTo++;
                    }
                    personController.dialogueIndexes = new int[] { 6 };
                    break;
            }
        }
    }
}
