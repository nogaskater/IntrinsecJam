using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Collections;

public class TableBehaviour : MonoBehaviour
{
    private const int numQuestions = 4;

    [SerializeField] private PlayerBallTransitionController _playerBallTransitionController;
    [SerializeField] private ThrowController _throwController;

    [Header("UI ELEMENTS")]
    public RectTransform mask;

    [Header("ANIMATION")]
    [SerializeField] float animationSpeed;
    [SerializeField] RectTransform openTransform;
    [SerializeField] RectTransform closeTransform;

    public int MaxListSize = 5;

    [SerializeField] private List<Question> _questionsAndAnswers;
    private Dictionary<ExamElement, Question> _finalQuestions = new Dictionary<ExamElement, Question>();

    [SerializeField] private List<PaperUI> _preAnswerListUI;
    public PaperUI CurrentPaperUIAtTable { get; set; }


    [SerializeField] private List<PaperUI> _postAnswerListUI;
    public PaperUI CurrentPaperUIInHand { get; set; }


    [Header("PAPER ELEMENTS")]
    public TextMeshProUGUI paperQuestion;
    public TextMeshProUGUI paperAnswer;

    [Header("QUESTIONS")]
    public TextMeshProUGUI[] questionTextExam;
    public TextMeshProUGUI[] answerTextExam;

    private bool _isWritingAnswer = false;


    private void Awake()
    {
        if (_playerBallTransitionController == null)
            throw new ArgumentNullException("_playerBallTransitionController");
        if (_throwController == null)
            throw new ArgumentNullException("_throwController");
    }
    private void OnEnable()
    {
        _throwController.OnBallThrow += RemovePaperInHand;
    }
    private void OnDisable()
    {
        _throwController.OnBallThrow -= RemovePaperInHand;        
    }

    void Start()
    {
        CreateExam();

        InitializePaperUI();

        mask.sizeDelta = new Vector2(mask.sizeDelta.x, closeTransform.anchoredPosition.y);
    }

    private void InitializePaperUI()
    {
        foreach (var paperUI in _preAnswerListUI)
        {
            paperUI.gameObject.SetActive(false);
        }

        foreach (var paperUI in _postAnswerListUI)
        {
            paperUI.gameObject.SetActive(false);
        }
    }

    private void CreateExam()
    {
        List<Question> copiedQuestions = new List<Question>(_questionsAndAnswers);

        for (int i = 0; i < numQuestions; i++)
        {
            int randomID = UnityEngine.Random.Range(0, copiedQuestions.Count);

            _finalQuestions.Add((ExamElement)(i+1), copiedQuestions[randomID]);

            copiedQuestions.RemoveAt(randomID);
        }

        int counter = 0;
        foreach (var question in _finalQuestions)
        {
            questionTextExam[counter].text = question.Value.GetQuestion;
            answerTextExam[counter].text = question.Value.GetAnswer;

            counter++;
        }
    }

    private PaperUI AddPaper(BallController _ballcontroller, List<PaperUI> paperUIList)
    {
        foreach (var paperUI in paperUIList)
        {
            if (paperUI.StoredPaper == null)
            {
                paperUI.StoredPaper = _ballcontroller;

                paperUI.gameObject.SetActive(true);

                return paperUI;
            }
        }

        return null;
    }
    private void RemovePaper(PaperUI paperUI)
    {
        paperUI.StoredPaper = null;

        paperUI.SelectorActive(false);

        paperUI.gameObject.SetActive(false);
    }

    public void AddUnansweredPaper(BallController _newPaper)
    {
        AddPaper(_newPaper, _preAnswerListUI);

        _newPaper.IsSafe = true;
    }
    private PaperUI AddAnsweredPaper(BallController _answeredPaper)
    {
        return AddPaper(_answeredPaper, _postAnswerListUI);
    }

    private void RemovePaperAtTable()
    {
        RemovePaper(CurrentPaperUIAtTable);

        CurrentPaperUIAtTable = null;
    }
    private void RemovePaperInHand()
    {
        RemovePaper(CurrentPaperUIInHand);

        CurrentPaperUIInHand = null;
    }

    private void SelectPaper(PaperUI paperUI)
    {
        if (!IsThereRoomToAnswerMorePapers() || _isWritingAnswer)
            return;

        AudioManager.Instance.PlaySound("OpenPaper");

        if (CurrentPaperUIAtTable == null)
        {
            CurrentPaperUIAtTable = paperUI;

            //print(CurrentPaper.Question);

            paperUI.SelectorActive(true);

            SetOpenedPaper(CurrentPaperUIAtTable.StoredPaper);

            OpenTable();

            return;
        }

        if(CurrentPaperUIAtTable == paperUI)
        {
            CurrentPaperUIAtTable = null;

            paperUI.SelectorActive(false);

            CloseTable();

            return;
        }

        CurrentPaperUIAtTable.SelectorActive(false);

        CurrentPaperUIAtTable = paperUI;

        CurrentPaperUIAtTable.SelectorActive(true);

        SetOpenedPaper(CurrentPaperUIAtTable.StoredPaper);
    }

    private void SelectAnswer(int examElement)
    {
        if (_isWritingAnswer)
            return;

        CurrentPaperUIAtTable.StoredPaper.Answer = (ExamElement)examElement;

        paperAnswer.text = _finalQuestions[CurrentPaperUIAtTable.StoredPaper.Answer].GetAnswer;

        _isWritingAnswer = true;

        StartCoroutine(ConfirmAnswer(1));


        AudioManager.Instance.PlaySound("WritePaper");
    }
    private IEnumerator ConfirmAnswer(float time)
    {
        yield return new WaitForSeconds(time);

        PaperUI answeredPaper = AddAnsweredPaper(CurrentPaperUIAtTable.StoredPaper);

        RemovePaperAtTable();

        AutoLaunchPaper(answeredPaper);

        CloseTable();

        AudioManager.Instance.PlaySound("Button Press");

        _isWritingAnswer = false;
    }
    public void AutoLaunchPaper(PaperUI paperUI)
    {
        if (CurrentPaperUIInHand == null)
        {
            CurrentPaperUIInHand = paperUI;

            _playerBallTransitionController.PutBallInHand(paperUI.StoredPaper);

            paperUI.SelectorActive(true);

            return;
        }

        if (CurrentPaperUIInHand != paperUI)
        {
            CurrentPaperUIInHand.SelectorActive(false);
            _playerBallTransitionController.RemoveBallFromHand(CurrentPaperUIInHand.StoredPaper);

            CurrentPaperUIInHand = paperUI;
            CurrentPaperUIInHand.SelectorActive(true);
            _playerBallTransitionController.PutBallInHand(CurrentPaperUIInHand.StoredPaper);
        }
    }
    public void LaunchPaper(PaperUI paperUI)
    {
        if (_isWritingAnswer)
            return;

        if(CurrentPaperUIAtTable != null)
        {
            CurrentPaperUIAtTable.SelectorActive(false);

            CurrentPaperUIAtTable = null;
        }

        CloseTable();

        if (CurrentPaperUIInHand == paperUI)
            return;

        if(CurrentPaperUIInHand == null)
        {
            CurrentPaperUIInHand = paperUI;

            _playerBallTransitionController.PutBallInHand(paperUI.StoredPaper);

            paperUI.SelectorActive(true);

            return;
        }

        if(CurrentPaperUIInHand != paperUI)
        {
            CurrentPaperUIInHand.SelectorActive(false);
            _playerBallTransitionController.RemoveBallFromHand(CurrentPaperUIInHand.StoredPaper);

            CurrentPaperUIInHand = paperUI;
            CurrentPaperUIInHand.SelectorActive(true);
            _playerBallTransitionController.PutBallInHand(CurrentPaperUIInHand.StoredPaper);
        }
    }

    public void OpenTable()
    {
        mask.DOSizeDelta(new Vector2(mask.sizeDelta.x, openTransform.anchoredPosition.y), animationSpeed);
    }

    public void CloseTable()
    {
        mask.DOSizeDelta(new Vector2(mask.sizeDelta.x, closeTransform.anchoredPosition.y), animationSpeed);
    }


    public void SetOpenedPaper(BallController ballController)
    {
        paperQuestion.text = _finalQuestions[ballController.Question].GetQuestion;

        paperAnswer.text = "";
    }

    public bool IsThereRoomForMoreNewPapers()
    {
        foreach (var paperUI in _preAnswerListUI)
        {
            if (paperUI.StoredPaper == null)
                return true;
        }

        return false;
    }

    private bool IsThereRoomToAnswerMorePapers()
    {
        foreach (var paperUI in _postAnswerListUI)
        {
            if (paperUI.StoredPaper == null)
                return true;
        }

        return false;
    }

}
