using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class TableBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerBallTransitionController _playerBallTransitionController;

    #region VARIABLES
    [Header("UI ELEMENTS")]
    public Transform table;
    public Transform mask;
    public Vector3 initialPosition;

    [Header("ANIMATION")]
    [SerializeField] float movementY;
    [SerializeField] float animationSpeed;
    [SerializeField] Transform openTransform;
    [SerializeField] Transform closeTransform;

    public int MaxListSize { get; } = 4;

    [Header("TO ANSWER")]
    [SerializeField] List<Paper> toAnswerQueue;
    [SerializeField] List<GameObject> papersToAnswer;
    public int ToAnserQueueCount => toAnswerQueue.Count;

    [Header("ANSWERED")]
    [SerializeField] List<Paper> answeredQueue;
    [SerializeField] List<GameObject> papersDone;
    public int AnsweredQueueCount => answeredQueue.Count;

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

    private void Awake()
    {
        if (_playerBallTransitionController == null)
            throw new ArgumentNullException("_playerBallTransitionController");
    }

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

            mask.DOMove(openTransform.position, animationSpeed);

        }
    }
    #endregion

    #region CLOSE TABLE
    public void CloseTable()
    {
        if (isOpened)
        {
            isOpened = false;

            mask.DOMove(closeTransform.position, animationSpeed);

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

    public void SelectPaper(int index)
    {
        if (!paperOpened)
        {
            OpenTable();

            paperOpened = true;

            currentPaper = toAnswerQueue[index];
            toAnswerQueue.Remove(toAnswerQueue[index]);

            UpdatePaperUI();

            currentPaperToAnswer.SetActive(true);
        }
    }
    public void LaunchPaper(int index)
    {
        print(answeredQueue[index]);
        
        _playerBallTransitionController.PutBallInHand(answeredQueue[index].gameObject);

        //Eliminamos el paper de la lista
        answeredQueue.Remove(answeredQueue[0]);

        UpdatePaperUI();

        CloseTable();
    }
    public void SelectAnswer(int index)
    {
        //SELECCIONAMOS LA RESPUESTA
        SelectAnAnswer(index);

        //CONFIRMAMOS LA MESA
        ConfirmAnswer();

        //CERRAMOS LA MESA
        CloseTable();
    }

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

    #region CONFIRM ANSWER
    public void ConfirmAnswer()
    {
        if (paperOpened && currentPaper.answer != ExamElement.NONE)
        {
            paperOpened = false;

            answeredQueue.Add(currentPaper);


            UpdatePaperUI();

            //Desactivamos la imagen
            currentPaperToAnswer.SetActive(false);
            answer.SetActive(false);
        }
    }
    #endregion

    //#region DEBUG ADD PAPER
    //public void DebugAddPaper()
    //{
    //    Paper paper = new Paper();

    //    //paper.paper_ID = GetIdForNewPaper();
    //    paper.student_ID = UnityEngine.Random.Range(0,5);
    //    paper.question = ExamElement.EXAM_ELEMENT_1;

    //    //Añadimos el papel
    //    toAnswerQueue.Add(paper);

    //    UpdatePaperUI();
    //}
    //#endregion
}
