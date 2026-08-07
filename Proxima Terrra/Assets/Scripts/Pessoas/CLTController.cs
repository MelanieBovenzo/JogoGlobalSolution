using UnityEngine;

public class CLTController : MonoBehaviour
{
    [SerializeField] PersonController personController;
    [SerializeField] TaskController taskController;
    [SerializeField] DialogueController dialogueController;

    private bool talked = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string allLines = "nada;aOlá, você é encarregado pelos consertos da nave, certo?;bInfelizmente, nem sei como essa coisa conseguiu chegar até aqui com tanta coisa faltando.;aCaramba, a empresa não faz checagens de segurança pra se certificar que está tudo certinho?;bFaz muito mal, eles só querem que a nave não exploda gastando o mínimo de dinheiro possível.;aSei...;bEstou ocupado.;aOlá Billy, o que você está fazendo?;bConsertando a nave, vou ter que fazer isso por um bom tempo. Essa nave não vai sair do chão pra voltar à Nave-Mãe até eu arrumar bastante coisa.;aEntão nós estamos presos aqui?;bBasicamente sim.;bEstou ocupado.;bInclusive você deveria me agradecer por poder usar essa ferramenta de coleta de cristais, eu que consertei ela.;aObrigada!;bEla ficou toda destruída quando pousamos aqui, igual nossa nave...;aVocê está melhor? Me contaram que um alienígena te atacou.;bTô sim, ele só bateu no meu braço mas parece que não quebrou nada.;aQue bom...;bÉ bom que eu tive um tempo de descanso pelo menos, agora tenho que voltar a trabalhar.;aVocê está melhor? Me contaram que um alienígena te atacou.;bTô sim, ele só bateu no meu braço mas parece que não quebrou nada.;aQue bom...;bÉ bom que eu tive um tempo de descanso pelo menos, agora tenho que voltar a trabalhar.";

        personController.lines = allLines.Split(';');
        personController.dialogueIndexes = new int[] { 1, 2, 3, 4, 5, };
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueController._isTalking == false)
        {
            switch (taskController.currentDay)
            {
                case 1:
                    if (personController.i == 5)
                    {
                        if (!talked)
                        {
                            talked = true;
                            taskController.talkedTo++;
                        }
                        personController.dialogueIndexes = new int[] { 6 };
                    }
                    break;
                case 2:
                    personController.dialogueIndexes = new int[] { 7, 8, 9, 10 };
                    if (personController.i == 10)
                    {
                        personController.dialogueIndexes = new int[] { 11 };
                    }
                    if (taskController.task1Completed)
                    {
                        personController.dialogueIndexes = new int[] { 12, 13, 14 };
                    }
                    break;
                case 3:
                    personController.dialogueIndexes = new int[] { 15, 16, 17, 18 };
                    break;
            }
        }
    }
}
