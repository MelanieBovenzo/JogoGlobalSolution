using UnityEngine;

public class AlienController : MonoBehaviour
{
    [SerializeField] PersonController personController;
    [SerializeField] TaskController taskController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        personController.dialogueIndexes = new int[] { 0, 1, 2, 3, 4 };
    }

    // Update is called once per frame
    void Update()
    {

    }
}
