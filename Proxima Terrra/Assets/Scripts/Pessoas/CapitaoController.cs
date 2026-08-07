using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class CapitaoController : MonoBehaviour
{
    [SerializeField] PersonController personController;
    [SerializeField] TaskController taskController;
    [SerializeField] DialogueController dialogueController;

    private bool talked = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string allLines = "nada;aOlá capitão, tudo bem?;cQue foi? Você está aqui pra trabalhar, não pra bater papo.;a...;cArrumei algo bom pra você fazer, sai da nave e seja a primeira a dar uma olhada nos arredores, vê se você se faz útil.;aOk capitão?;cQue foi? Não me ouviu direito? Sai da nave e dá uma olhada em volta, e vai logo!;cEntão, viu algo útil?;aSó um deserto com algumas pedras contendo os critais que pediram pra gente estudar.;cHmm, nada que ja não sabíamos.;cEi, Anari, pega aquela ferramenta em cima da mesa e vai coletar alguns cristais.;aMeu nome é Anahí, senhor.;cSeu nome não importa, o que importa são os cristais.;cVolte quando você conseguir 10 fragmentos de cristal.;aAqui capitão, consegui os cristais que você me pediu.;cBom trabalho.;aE quando eu coletei o último fragmento eu entrei em contato com uma forma de vida daqui?;cO quê? Você tem certeza?;aSim, eu acho que ele tentou falar comigo...;cVocê tem mais alguma informação sobre eles?;aA rocha com cristal nela estava nas costas de um deles, como se fosse um casco.;cEu vou entrar em contato com o comando, só continue obedecendo minhas ordens e evite contato com eles por enquanto.;cVolte ao trabalho.;cAnalí, hoje você vai coletar cristais para estudo novamente.;aVocê tem certeza capitão? Isso não parece ser ideal para vida desse planeta.;cApenas obedeça minhas ordens, mocinha.;a...;cJá conseguiu os cristais?;aCapitão, eu acho que esses cristais são importantes para os alienígenas, não acha melhor...;cSe você se recusar a seguir uma ordem direta mais uma vez eu te expulso dessa nave e deixo seu óxigênio acabar lá fora.;c'Analí', hoje você vai coletar cristais para estudo novamente.;aVocê tem certeza capitão? Isso não parece ser ideal para vida desse planeta.;cApenas obedeça minhas ordens, mocinha.;a...;cJá conseguiu os cristais?;aCapitão, eu acho que esses cristais são importantes para os alienígenas, não acha melhor...;cSe você se recusar a seguir uma ordem direta mais uma vez eu te expulso dessa nave e deixo seu óxigênio acabar lá fora.;(depois de pegar os cristal);cJá conseguiu os cristais?;aSim, capitão.;cPra você saber, essas criaturas que você parece ter feito amizade atacaram o Billy quando ele foi coletar cristais, ele está no quarto dele se recuperando.;aEle está bem?;cEle vai ficar. Eu acho. Espero que sim, essa nave ainda precisa de consertos. Essas criaturinhas de merda no nosso planeta só atrapalham tudo...";

        personController.lines = allLines.Split(';');
        personController.dialogueIndexes = new int[] { 1, 2, 3, 4, 5 };
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
                        taskController.extraTask1 = "Saia da nave";
                        taskController.task2Started = true;
                        if (!talked)
                        {
                            talked = true;
                            taskController.talkedTo++;
                        }
                        personController.dialogueIndexes = new int[] { 6 };
                    }
                    if (taskController.task2Completed)
                    {
                        personController.dialogueIndexes = new int[] { 7, 8, 9 };
                    }
                    break;
                case 2:
                    personController.dialogueIndexes = new int[] { 10, 11, 12 };
                    if (personController.i == 12)
                    {
                        taskController.task1Started = true;
                        taskController.extraTask1 = "Pegue a ferramenta em cima da mesa";
                        personController.dialogueIndexes = new int[] { 13 };
                    }
                    if (taskController.task2Completed)
                    {
                        personController.dialogueIndexes = new int[] { 14, 15, 16, 17, 18, 19, 20, 21 };
                    }
                    break;
                case 3:
                    personController.dialogueIndexes = new int[] { 22, 23, 24, 25 };
                    if (personController.i == 25)
                    {
                        personController.dialogueIndexes = new int[] { 26, 27, 28, 29, 30 };
                        taskController.task1Started = true;
                        if (taskController.task1Completed)
                        {
                            personController.dialogueIndexes = new int[] { 31, 32, 33, 34, 35 };
                        }
                    }
                    break;
            }
        }
    }
}
