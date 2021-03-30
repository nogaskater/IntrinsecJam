using UnityEngine;

public enum ExamElement
{
    NONE = 0,
    EXAM_ELEMENT_1 = 1,
    EXAM_ELEMENT_2 = 2,
    EXAM_ELEMENT_3 = 3,
    EXAM_ELEMENT_4 = 4,
    EXAM_ELEMENT_5 = 5,
    EXAM_ELEMENT_6 = 6,
    EXAM_ELEMENT_7 = 7,
    EXAM_ELEMENT_8 = 8,
    EXAM_ELEMENT_9 = 9,
    EXAM_ELEMENT_10 = 10
};

public class Paper : MonoBehaviour
{
    public ExamElement question;
    public ExamElement answer;
}
