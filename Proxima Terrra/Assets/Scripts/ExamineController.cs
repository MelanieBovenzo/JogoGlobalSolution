using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using TMPro;

public class ExamineController : MonoBehaviour
{
    public string examineText;
    public Image examineImage;
    [SerializeField] Canvas examineCanvas;
    [SerializeField] Image examineImageComponent;
    [SerializeField] TextMeshProUGUI examineTextComponent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Examine()
    {
        examineCanvas.gameObject.SetActive(true);
        examineTextComponent.text = string.Empty;

        examineImageComponent = examineImage;
        examineTextComponent.text = examineText;
    }
}
