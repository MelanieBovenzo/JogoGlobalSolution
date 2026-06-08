using UnityEngine;
using TMPro;
using System;

public class TaskController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    // UI
    [SerializeField] TextMeshProUGUI taskText;
    [SerializeField] TextMeshProUGUI sleepText;
    [SerializeField] Canvas taskCanvas;
    [SerializeField] Canvas sleepCanvas;

    // DAY STUFF
    public int currentDay = 0;
    public bool canSleep = true;

    // TASK STUFF
    [HideInInspector] public String extraTask1 = String.Empty;
    [HideInInspector] public bool task1Completed;
    [HideInInspector] public bool task2Completed;
    [HideInInspector] public bool task1Started;
    [HideInInspector] public bool task2Started;

    // DAY SPECIFIC
    [HideInInspector] public int talkedTo = 0;

    void Start()
    {
        NextDay();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentDay)
        {
            case 1:

                taskText.text = $"Dia 1 \n" +
                $"Conheça a tripulação {talkedTo}/3\n" +
                extraTask1;
                if (task2Completed)
                {
                    taskText.text += " (Completa!)";
                }
                if (task1Completed && task2Completed)
                {
                    taskText.text = $"Dia 1 \n" +
                    $"Conheça a tripulação {talkedTo}/3\n" +
                    extraTask1 + "\n Você completou todas as tarefas por hoje, você pode ir dormir" +
                    "no seu quarto da nave";
                    canSleep = true;
                }
                break;
            case 2:
                taskText.text = $"Dia 2\n" +
                $"Coletar Pedras {playerController.rockCount.ToString()}/10";
                canSleep = false;
                if (playerController.rockCount >= 10)
                {
                    canSleep = true;
                }
                break;
            default:
                break;
        }
    }

    public void NextDay()
    {
        currentDay++;
        taskCanvas.gameObject.SetActive(false);
        sleepCanvas.gameObject.SetActive(true);
        sleepText.text = $"Dia {currentDay}";
        canSleep = false;
        Invoke("WakeUp", 2f);
        switch (currentDay)
        {
            case 1:
                task1Started = true;
                task2Started = false;
                break;
            case 2:
                task1Started = false;
                task2Started = false;
                break;
            default:
                break;
        }
        task1Completed = false;
        task2Completed = false;
    }

    private void WakeUp()
    {
        taskCanvas.gameObject.SetActive(true);
        sleepCanvas.gameObject.SetActive(false);
    }
}
