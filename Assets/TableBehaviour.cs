using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
    [SerializeField] Button openButton;
    [SerializeField] List<Button> papersToAnswer;
    [SerializeField] List<Button> papersDone;



    bool isOpened = false;
    #endregion

    #region START
    void Start()
    {
        initialPosition = table.position;
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

            openButton.interactable = false;
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
            openButton.interactable = true;
        }
    }
    #endregion
}
