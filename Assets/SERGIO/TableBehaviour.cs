using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum ExamElement
{
    NONE,
    EXAM_ELEMENT_1,
    EXAM_ELEMENT_2,
    EXAM_ELEMENT_3,
    EXAM_ELEMENT_4,
    EXAM_ELEMENT_5,
    EXAM_ELEMENT_6,
    EXAM_ELEMENT_7,
    EXAM_ELEMENT_8,
    EXAM_ELEMENT_9,
    EXAM_ELEMENT_10
};



public struct Paper
{
    public int paper_ID;
    public int student_ID;
    public ExamElement question;
    public ExamElement answer;
};

public class TableBehaviour : MonoBehaviour
{
    #region VARIABLES
    [Header("UI ELEMENTS")]
    [SerializeField] RectTransform table;
    [SerializeField] RectTransform mask;
    Vector3 initialPosition;

    [Header("ANIMATION")]
    [SerializeField] float movementY;
    [SerializeField] float animationSpeed;

    [Header("BUTTONS")]
    [SerializeField] GameObject openButton;

    [Header("TO ANSWER")]
    [SerializeField] List<Paper> toAnswerQueue;
    [SerializeField] List<GameObject> papersToAnswer;

    [Header("ANSWERED")]
    [SerializeField] List<Paper> answeredQueue;
    [SerializeField] List<GameObject> papersDone;

    [Header("PAPER ELEMENTS")]
    [SerializeField] GameObject currentPaperToAnswer;    
    [SerializeField] Text paperQuestion;
    [SerializeField] Text paperAnswer;
    [SerializeField] GameObject answer;
    Paper currentPaper;

    [Header("ANSWER IMAGES")]
    [SerializeField] Sprite[] answerImages;

    ExamElement[] examQuestions = new ExamElement[10];
    ExamElement[] examAnswers = new ExamElement[10];

    bool isOpened = false;
    bool paperOpened = false;

    #endregion

    #region START
    void Start()
    {
        initialPosition = table.position;

        //Inicializamos las listas
        toAnswerQueue = new List<Paper>();
        answeredQueue = new List<Paper>();

        //Creamos el examen
        CreateExam();

        //Updateamos la UI
        UpdatePaperUI();

        //PROVISIONAL
        examAnswers[0] = ExamElement.EXAM_ELEMENT_1;
        examAnswers[1] = ExamElement.EXAM_ELEMENT_2;
        examAnswers[2] = ExamElement.EXAM_ELEMENT_3;
        examAnswers[3] = ExamElement.EXAM_ELEMENT_4;
        examAnswers[4] = ExamElement.EXAM_ELEMENT_5;
        examAnswers[5] = ExamElement.EXAM_ELEMENT_6;
        examAnswers[6] = ExamElement.EXAM_ELEMENT_7;
        examAnswers[7] = ExamElement.EXAM_ELEMENT_8;
        examAnswers[8] = ExamElement.EXAM_ELEMENT_9;
        examAnswers[9] = ExamElement.EXAM_ELEMENT_10;
        



    }
    #endregion

    #region UPDATE
    void Update()
    {
        table.position = initialPosition;

    }
    #endregion

    #region CREATE EXAM
    void CreateExam()
    {

    }
    #endregion

    #region OPEN TABLE
    public void OpenTable()
    {
        if (!isOpened)
        {
            isOpened = true;

            mask.DOMoveY(mask.transform.position.y + movementY, animationSpeed);

            openButton.SetActive(false);
        }
    }
    #endregion

    #region CLOSE TABLE
    public void CloseTable()
    {
        if (isOpened)
        {
            isOpened = false;

            mask.DOMoveY(mask.transform.position.y - movementY, animationSpeed);

            //Reactivamos el boton
            openButton.SetActive(true);
        }
    }
    #endregion

    #region ADD NEW PAPER
    public void AddNewPaper(Paper _newPaper)
    {
        //Añadimos el papel
        toAnswerQueue.Add(_newPaper);

        //Updateamos la UI
        UpdatePaperUI();
    }
    #endregion

    #region ERASE PAPER
    public void ErasePaper()
    {

    }
    #endregion

    #region GET ID FOR NEW PAPER
    public int GetIdForNewPaper()
    {
        return toAnswerQueue.Count + 1;
    }
    #endregion

    #region OPEN PAPER
    public void OpenPaper()
    {

    }
    #endregion

    #region UPDATE PAPER UI
    public void UpdatePaperUI()
    {
        //Actualizamos los papeles por contestar
        if (toAnswerQueue.Count == 0)
        {
            foreach (GameObject item in papersToAnswer)
            {
                item.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject item in papersToAnswer)
            {
                item.SetActive(false);
            }

            for (int i = 0; i < toAnswerQueue.Count; i++)
            {
                papersToAnswer[i].SetActive(true);
            }
        }

        //Actualizamos los papeles contestados
        if (answeredQueue.Count == 0)
        {
            foreach (GameObject item in papersDone)
            {
                item.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject item in papersDone)
            {
                item.SetActive(false);
            }

            for (int i = 0; i < answeredQueue.Count; i++)
            {
                papersDone[i].SetActive(true);
            }
        }

    }
    #endregion

    #region SELECT PAPER
    #region SELECT PAPER 1
    public void SelectPaper1()
    {
        if (!paperOpened)
        {
            paperOpened = true;

            currentPaper = toAnswerQueue[0];
            toAnswerQueue.Remove(toAnswerQueue[0]);

            UpdatePaperUI();

            currentPaperToAnswer.SetActive(true);
        }
    }
    #endregion

    #region SELECT PAPER 2
    public void SelectPaper2()
    {
        if (!paperOpened)
        {
            paperOpened = true;

            //Nos guardamos el papel
            currentPaper = toAnswerQueue[1];

            //Quitamos la pelota de la lista por contestar
            papersToAnswer.Remove(papersToAnswer[1]);

            

            UpdatePaperUI();

            currentPaperToAnswer.SetActive(true);
        }
    }
    #endregion

    #region SELECT PAPER 3
    public void SelectPaper3()
    {
        if (!paperOpened)
        {
            paperOpened = true;

            //Nos guardamos el papel
            currentPaper = toAnswerQueue[2];

            //Quitamos la pelota de la lista por contestar
            papersToAnswer.Remove(papersToAnswer[2]);

            UpdatePaperUI();

            currentPaperToAnswer.SetActive(true);
        }
    }
    #endregion

    #region SELECT PAPER 4
    public void SelectPaper4()
    {
        if (!paperOpened)
        {
            paperOpened = true;

            //Nos guardamos el papel
            currentPaper = toAnswerQueue[3];

            //Quitamos la pelota de la lista por contestar
            papersToAnswer.Remove(papersToAnswer[3]);

            UpdatePaperUI();

            currentPaperToAnswer.SetActive(true);
        }
    }
    #endregion

    #region SELECT PAPER 5
    public void SelectPaper5()
    {
        if (!paperOpened)
        {
            paperOpened = true;

            //Nos guardamos el papel
            currentPaper = toAnswerQueue[1];

            //Quitamos la pelota de la lista por contestar
            papersToAnswer.Remove(papersToAnswer[4]);

            UpdatePaperUI();

            currentPaperToAnswer.SetActive(true);
        }
    }
    #endregion
    #endregion

    #region LAUNCH
    #region LAUNCH PAPER 1
    public void LaunchPaper1()
    {
        //LLAMAR A LA FUNCIÓN DEL ADRI
        // --- FUNCION ADRI --- ///


        //Eliminamos el paper de la lista
        answeredQueue.Remove(answeredQueue[0]);

        UpdatePaperUI();
    }
    #endregion

    #region LAUNCH PAPER 2
    public void LaunchPaper2()
    {
        //LLAMAR A LA FUNCIÓN DEL ADRI
        // --- FUNCION ADRI --- ///

        //Eliminamos el paper de la lista
        answeredQueue.Remove(answeredQueue[1]);

        UpdatePaperUI();
    }
    #endregion

    #region LAUNCH PAPER 3
    public void LaunchPaper3()
    {
        //LLAMAR A LA FUNCIÓN DEL ADRI
        // --- FUNCION ADRI --- ///


        //Eliminamos el paper de la lista
        answeredQueue.Remove(answeredQueue[2]);

        UpdatePaperUI();
    }
    #endregion

    #region LAUNCH PAPER 4
    public void LaunchPaper4()
    {
        //LLAMAR A LA FUNCIÓN DEL ADRI
        // --- FUNCION ADRI --- ///


        //Eliminamos el paper de la lista
        answeredQueue.Remove(answeredQueue[3]);

        UpdatePaperUI();
    }
    #endregion

    #region LAUNCH PAPER 5
    public void LaunchPaper5()
    {
        //LLAMAR A LA FUNCIÓN DEL ADRI
        // --- FUNCION ADRI --- ///


        //Eliminamos el paper de la lista
        answeredQueue.Remove(answeredQueue[4]);

        UpdatePaperUI();
    }
    #endregion
    #endregion

    #region SELECT ANSWER

    #region SELECT ANSWER 0
    public void SelecAnswer0()
    {        
        SelectAnAnswer(0);
    }
    #endregion

    #region SELECT ANSWER 1
    public void SelecAnswer1()
    {
        SelectAnAnswer(1);        
    }
    #endregion

    #region SELECT ANSWER 2
    public void SelecAnswer2()
    {
        SelectAnAnswer(2);
    }
    #endregion

    #region SELECT ANSWER 3
    public void SelecAnswer3()
    {
        SelectAnAnswer(3);
    }
    #endregion

    #region SELECT ANSWER 4
    public void SelecAnswer4()
    {
        SelectAnAnswer(4);
    }
    #endregion

    #region SELECT ANSWER 5
    public void SelecAnswer5()
    {
        SelectAnAnswer(5);
    }
    #endregion

    #region SELECT ANSWER 6
    public void SelecAnswer6()
    {
        SelectAnAnswer(6);
    }
    #endregion

    #region SELECT ANSWER 7
    public void SelecAnswer7()
    {
        SelectAnAnswer(7);
    }
    #endregion

    #region SELECT ANSWER 8
    public void SelecAnswer8()
    {
        SelectAnAnswer(8);
    }
    #endregion

    #region SELECT ANSWER 9
    public void SelecAnswer9()
    {
        SelectAnAnswer(9);
    }
    #endregion   


    #region SELECT AN ANSWER
    void SelectAnAnswer(int _answer)
    {
        if (paperOpened)
        {
            //Activamos la imagen
            answer.SetActive(true);

            //Le asignamos el Sprite
            answer.GetComponent<Image>().sprite = answerImages[_answer];
            currentPaper.answer = examAnswers[_answer];
        }  

    }
    #endregion
    #endregion

    #region CONFIRM ANSWER
    public void ConfirmAnswer()
    {
        if (paperOpened && currentPaper.answer != ExamElement.NONE)
        {
            //Desactivamos la imagen
            answer.SetActive(false);
            currentPaperToAnswer.SetActive(false);

            answeredQueue.Add(currentPaper);

            paperOpened = false;

            UpdatePaperUI();
        }
    }
    #endregion

    #region DEBUG ADD PAPER
    public void DebugAddPaper()
    {
        Paper paper = new Paper();

        paper.paper_ID = GetIdForNewPaper();
        paper.student_ID = Random.Range(0,5);
        paper.question = ExamElement.EXAM_ELEMENT_1;

        //Añadimos el papel
        toAnswerQueue.Add(paper);

        UpdatePaperUI();
    }
    #endregion
}
