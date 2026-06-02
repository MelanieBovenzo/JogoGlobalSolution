using UnityEngine;
using TMPro;

public class TaskController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI taskText;
    [SerializeField] TextMeshProUGUI sleepText;
    [SerializeField] PlayerController playerController;

    [SerializeField] Canvas taskCanvas;
    [SerializeField] Canvas sleepCanvas;

    public int currentDay = 0;
    public bool canSleep = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
                $"Coletar Pedras {playerController.rockCount.ToString()}/10";
                if (playerController.rockCount >= 10)
                {
                    canSleep = true;
                }
                break;
            case 2:
                taskText.text = $"Dia 2\n" +
                "Formule Áurea";
                canSleep = true;
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
    }

    private void WakeUp()
    {
        taskCanvas.gameObject.SetActive(true);
        sleepCanvas.gameObject.SetActive(false);
    }
}
