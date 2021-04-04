using UnityEngine;

[System.Serializable]
public class Question
{
    [TextArea(3, 5)]
    [SerializeField] private string _question;
    [TextArea(3, 5)]
    [SerializeField] private string _answer;

    public string GetQuestion => _question;
    public string GetAnswer => _answer;
}
