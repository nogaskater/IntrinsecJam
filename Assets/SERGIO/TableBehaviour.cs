using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum QuestionType
{
    QUESTION_1,
    QUESTION_2,
    QUESTION_3,
    QUESTION_4,
    QUESTION_5,
    QUESTION_6,
    QUESTION_7,
    QUESTION_8,
    QUESTION_9,
    QUESTION_10,
};

public enum AnswerType
{
    ANSWER_1,
    ANSWER_2,
    ANSWER_3,
    ANSWER_4,
    ANSWER_5,
    ANSWER_6,
    ANSWER_7,
    ANSWER_8,
    ANSWER_9,
    ANSWER_10
};

public struct Paper
{
    int paper_ID;
    int student_ID;
    QuestionType question;
    AnswerType answer;
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

    [Header("TABLE ELEMENTS")]
    [SerializeField] GameObject currentPaperToAnswer;

    bool isOpened = false;
    #endregion

    #region START
    void Start()
    {
        initialPosition = table.position;

        //Inicializamos las listas
        toAnswerQueue = new List<Paper>();
        answeredQueue = new List<Paper>();

        //UpdatePaperUI();
    }
    #endregion

    #region UPDATE
    void Update()
    {
        table.position = initialPosition;

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
                papersToAnswer[i].SetActive(true);
            }
        }

    }
    #endregion

    #region SELECT
    #region SELECT PAPER 1
    public void SelectPaper1()
    {
        UpdatePaperUI();

        currentPaperToAnswer.SetActive(true);

    }
    #endregion

    #region SELECT PAPER 2
    public void SelectPaper2()
    {
        papersToAnswer.Remove(papersToAnswer[1]);

        UpdatePaperUI();

        currentPaperToAnswer.SetActive(true);
    }
    #endregion

    #region SELECT PAPER 3
    public void SelectPaper3()
    {
        papersToAnswer.Remove(papersToAnswer[2]);

        UpdatePaperUI();

        currentPaperToAnswer.SetActive(true);
    }
    #endregion

    #region SELECT PAPER 4
    public void SelectPaper4()
    {
        papersToAnswer.Remove(papersToAnswer[3]);

        UpdatePaperUI();

        currentPaperToAnswer.SetActive(true);
    }
    #endregion

    #region SELECT PAPER 5
    public void SelectPaper5()
    {
        papersToAnswer.Remove(papersToAnswer[4]);

        UpdatePaperUI();

        currentPaperToAnswer.SetActive(true);
    }
    #endregion
    #endregion

    #region LAUNCH
    #region LAUNCH PAPER 1
    public void LaunchPaper1()
    {
        UpdatePaperUI();


        //LLAMAR A LA FUNCIÓN DEL ADRI

        //Eliminamos el paper de la lista
        answeredQueue.Remove(answeredQueue[0]);
    }
    #endregion

    #region LAUNCH PAPER 2
    public void LaunchPaper2()
    {

        UpdatePaperUI();


        //LLAMAR A LA FUNCIÓN DEL ADRI

        //Eliminamos el paper de la lista
        answeredQueue.Remove(answeredQueue[1]);
    }
    #endregion

    #region LAUNCH PAPER 3
    public void LaunchPaper3()
    {
        UpdatePaperUI();


        //LLAMAR A LA FUNCIÓN DEL ADRI

        //Eliminamos el paper de la lista
        answeredQueue.Remove(answeredQueue[2]);
    }
    #endregion

    #region LAUNCH PAPER 4
    public void LaunchPaper4()
    {
        UpdatePaperUI();

        //LLAMAR A LA FUNCIÓN DEL ADRI

        //Eliminamos el paper de la lista
        answeredQueue.Remove(answeredQueue[3]);
    }
    #endregion

    #region LAUNCH PAPER 5
    public void LaunchPaper5()
    {
        UpdatePaperUI();


        //LLAMAR A LA FUNCIÓN DEL ADRI

        //Eliminamos el paper de la lista
        answeredQueue.Remove(answeredQueue[4]);
    }
    #endregion
    #endregion

    #region



}
