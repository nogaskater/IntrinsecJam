using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class TableBehaviour : MonoBehaviour
{
    private const int numQuestions = 4;


    [SerializeField] private PlayerBallTransitionController _playerBallTransitionController;

    #region VARIABLES
    [Header("UI ELEMENTS")]
    public RectTransform mask;

    [Header("ANIMATION")]
    [SerializeField] float movementY;
    [SerializeField] float animationSpeed;
    [SerializeField] RectTransform openTransform;
    [SerializeField] RectTransform closeTransform;

    public int MaxListSize = 4;

    [Header("TO ANSWER")]
    [SerializeField] List<BallController> toAnswerQueue;
    [SerializeField] List<GameObject> papersToAnswer;
    public int ToAnserQueueCount => toAnswerQueue.Count;

    [Header("ANSWERED")]
    [SerializeField] List<BallController> answeredQueue;
    [SerializeField] List<GameObject> papersDone;
    public int AnsweredQueueCount => answeredQueue.Count;

    [Header("PAPER ELEMENTS")]
    [SerializeField] GameObject currentPaperToAnswer;
    public TextMeshProUGUI paperQuestion;
    public TextMeshProUGUI paperAnswer;
    [SerializeField] Image questionImage;
    public BallController CurrentPaper { get; set; }

    [Header("QUESTIONS")]
    public TextMeshProUGUI[] questionTextExam;
    public TextMeshProUGUI[] answerTextExam;

    public String[] questions;

    private List<string> finalQuestions = new List<string>();

    private bool _writingAnswer = false;

    ExamElement[] examQuestions = new ExamElement[10];
    ExamElement[] examAnswers = new ExamElement[10];

    #endregion

    private void Awake()
    {
        if (_playerBallTransitionController == null)
            throw new ArgumentNullException("_playerBallTransitionController");
    }

    void Start()
    {

        //Inicializamos las listas
        toAnswerQueue = new List<BallController>();
        answeredQueue = new List<BallController>();

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


        //CREAMOS EL EXAMEN
        CreateExam();

        mask.sizeDelta = new Vector2(mask.sizeDelta.x, closeTransform.anchoredPosition.y);
    }


    void CreateExam()
    {
        List<string> copiedQuestions = new List<string>(questions);

        for (int i = 0; i < numQuestions; i++)
        {
            int randomID = UnityEngine.Random.Range(0, copiedQuestions.Count);

            finalQuestions.Add(copiedQuestions[randomID]);

            copiedQuestions.RemoveAt(randomID);

        }

        for (int i = 0; i < finalQuestions.Count; i++)
        {
            string[] splitArray = finalQuestions[i].Split('?');

            questionTextExam[i].text = splitArray[0] + "?";
            answerTextExam[i].text = splitArray[1] + ".";
        }
    }
    public void AddNewPaper(BallController _newPaper)
    {
        toAnswerQueue.Add(_newPaper);

        UpdatePaperUI();

        _newPaper.IsSafe = true;
    }
    public void SelectPaper(int index)
    {
        if (CurrentPaper == null)
        {
            mask.DOSizeDelta(new Vector2(mask.sizeDelta.x, openTransform.anchoredPosition.y), animationSpeed);

            CurrentPaper = toAnswerQueue[index];

            paperQuestion.text = questionTextExam[index].text;

            currentPaperToAnswer.SetActive(true);

            UpdatePaperUI();

            AudioManager.Instance.PlaySound("OpenPaper");
        }
    }
    public void SelectAnswer(int index)
    {
        CurrentPaper.Answer = examAnswers[index];

        //PINTAMOS LA RESPUESTA
        string[] splitArray = finalQuestions[index].Split('?');

        paperAnswer.text = splitArray[1];

        Invoke("ConfirmAnswer", 1f);
        _writingAnswer = true;

        AudioManager.Instance.PlaySound("WritePaper");
    }
    public void ConfirmAnswer()
    {
        answeredQueue.Add(CurrentPaper);
        toAnswerQueue.Remove(CurrentPaper);

        currentPaperToAnswer.SetActive(false);

        CurrentPaper = null;

        CloseTable();

        _writingAnswer = false;
    }
    public void LaunchPaper(int index)
    {
        if (_writingAnswer)
            return;

        Rigidbody2D currentBall = _playerBallTransitionController.GetCurrentBall();
        if (currentBall != null)
        {
            currentBall.gameObject.SetActive(false);

            answeredQueue.Add(currentBall.GetComponent<BallController>());

            currentBall.GetComponent<BallController>().Student.HolderActive(false);
        }

        if(CurrentPaper != null)
        {
            AddNewPaper(CurrentPaper);
        }

        _playerBallTransitionController.PutBallInHand(answeredQueue[index].gameObject);


        CloseTable();
    }

    public void CloseTable()
    {
        if(CurrentPaper != null)
        {
            AddNewPaper(CurrentPaper);

            CurrentPaper = null;
        }

        mask.DOSizeDelta(new Vector2(mask.sizeDelta.x, closeTransform.anchoredPosition.y), animationSpeed);

        paperAnswer.text = "";

        UpdatePaperUI();
    }


    public void UpdatePaperUI()
    {
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
                if (papersDone == null)
                    continue;

                papersDone[i].SetActive(true);
            }
        }

    }

}
