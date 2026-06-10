using UnityEngine;

public class CientistaController : MonoBehaviour
{
    [SerializeField] PersonController personController;
    [SerializeField] TaskController taskController;
    [SerializeField] DialogueController dialogueController;

    private bool talked = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string allLines = "nada;aOi, você é a cientista da nave né? Não te vi muito na Nave-Mãe.;hÉ, eu tendo a focar bastante nos meus estudos, estava muito intrigada pois encontrei possíveis sinais de vida nesse planeta.;aQue interessante, não lembro de ler nada sobre isso nos arquivos da missão.;hÉ apenas uma hipótese distante e recente por enquanto, mas vou investigar mais enquanto estiver aqui.;aEntendi, foi bom conversar com você.;hIgualmente, você é a única pessoa que parece realmente interessada na missão, talvez você seja boa pra me ajudar.;aEu adoraria.;hOi, precisa de mim?;aComo está suas investigações?;hAinda não descobri nada perto de concreto.;aAh ok, me mantenha atualizada.;hEu vou.;aOlá Hana, como vão os estudos?;hEsses cristais parecem ter propriedades extremamente propícias para usá-los como combustível;aInteressante.;hEu estimo que, se eles forem viáveis para uso como combustíveis, eles serão 10x mais eficientes do que qualquer combustível da Terra.;aHana, você estava certa.;hSobre o quê?;aSobre ter vida alienígena nesse planeta, eu vi um agora pouco e... eu acho que ele falou comigo;hInteressante, muito interessante... onde você viu ele?;aEu estava coletando cristais nas pedras da superfície e uma pedra na verdade era uma espécie de casco da criatura.;hEntendi, obrigada por me contar.;aPor nada! ;aO que você acha sobre os alienígenas?;hPelo que você me contou sobre os cristais ficarem nas costas deles, talvez eles os usam como energia.;aCaramba, então nós estamos roubando a energia deles?;hAlgo assim, quando eu receber uma amostra de um fragmento que você coletou eu posso ter mais certeza.;aNós temos que fazer algo sobre a exploração desses seres, eles só querem extrair mais e mais cristais pelo lucro e a vida alienígena está em perigo!;hCalma Anahí, respira. Olha, eu tenho um projeto que talvez possa ajudar.;aQual?;hEu estive trabalhando há um tempo em um dispositivo tradutor capaz de reconhecer qualquer língua rapidamente e traduzir de ambas as direções;aQue legal! Mas ele conseguiria aprender línguas alienígenas?;hEle foi feito pra isso.;aEntendi, eu posso pegar ele emprestado pra tentar me comunicar e fazer o capitão entender as preocupações deles?;hNa verdade ele nunca foi construído, eu precisaria de uma fonte de energia enorme pra fazê-lo funcionar. Você poderia conseguir as partes dele e eu construo o tradutor.;aDo que você precisa?;hDe um pouco mais de cristal como fonte de energia, uns 2 fragmentos devem ser suficientes, e de uma antena. Acho que você acha uma dessas no quarto do Billy.;aOk, quando conseguir eu te trago.;hConseguiu as peças pro tradutor?;aAinda não, mas não sei se é muito legal coletar mais um fragmento de cristal de um alien.;hNão dá pra fazer um omelete sem quebrar alguns ovos.;aÉ, acho que não...";

        personController.lines = allLines.Split(';');
        personController.dialogueIndexes = new int[] { 1, 2, 3, 4, 5, 6, 7 };
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueController._isTalking == false)
        {
            switch (taskController.currentDay)
            {
                case 1:
                    if (personController.i == 7)
                    {
                        if (!talked)
                        {
                            talked = true;
                            taskController.talkedTo++;
                        }
                        personController.dialogueIndexes = new int[] { 8, 9, 10, 11, 12 };
                    }
                    break;
                case 2:
                    personController.dialogueIndexes = new int[] { 13, 14, 15, 16 };
                    if (taskController.task2Completed)
                    {
                        personController.dialogueIndexes = new int[] { 17, 18, 19, 20, 21, 22, 23 };
                        if (personController.i == 23)
                        {
                            personController.dialogueIndexes = new int[] { 24, 25, 26, 27 };
                        }
                    }
                    break;
                case 3:
                    personController.dialogueIndexes = new int[] { 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39 };
                    if (personController.i == 39)
                    {
                        taskController.task2Started = true;
                        personController.dialogueIndexes = new int[] { 40, 41, 42, 43 };
                    }
                    break;
            }
        }
    }
}
