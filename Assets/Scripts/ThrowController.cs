using System;
using UnityEngine;
using UnityEngine.UI;

public class ThrowController : MonoBehaviour
{
    [SerializeField] private CharacterAnimation _characterAnimation;

    [Header("GameObject References")]
    [SerializeField] private Rigidbody2D _activeBall;
    [SerializeField] private Transform _throwStartingPoint;

    [Header("UI References")]
    [SerializeField] private GameObject _arrow;

    [Header("UI Settings")]
    [SerializeField] private float _minXScaleArrow;
    [SerializeField] private float _maxXScaleArrow;
    [SerializeField] private float _ballOffsetArrow;

    [Header("Throw Settings")]
    [SerializeField] private float forceMultiplier;

    public Rigidbody2D GetActiveBall()
    {
        return _activeBall;
    }
    public void SetActiveBall(Rigidbody2D rb)
    {
        _activeBall = rb;
    }
    public Transform GetThrowStartingPoint()
    {
        return _throwStartingPoint;
    }

    private void Awake()
    {
        if (_characterAnimation == null)
            throw new ArgumentNullException("_characterAnimation");
    }

    void Start()
    {
        DisableArrow();
    }

    public void TriggerChargeAnimation()
    {
        _characterAnimation.Animator.SetTrigger("Charge");
    }

    public void ThrowBall(Vector2 initialDirection, float initialMagnitude, float minRadPossible, float maxRadPossible)
    {
        if(_activeBall!=null)
        {
            Vector2 ballForce = ComputeInitialForce(initialDirection, initialMagnitude, minRadPossible, maxRadPossible);
            _activeBall.bodyType = RigidbodyType2D.Dynamic;
            _activeBall.AddForce(ballForce, ForceMode2D.Impulse);

            _activeBall.gameObject.layer = LayerMask.NameToLayer("Ball");

            _activeBall.GetComponent<BallController>().Student.HolderActive(false);

            _activeBall = null;

            _characterAnimation.Animator.SetTrigger("Throw");
        }

    }

    private Vector2 ComputeInitialForce(Vector2 initialDirection, float initialMagnitude, float minRadPossible, float maxRadPossible)
    {
        //Debug.Log("Magnitude raw: " + initialMagnitude + " //// Magnitude normalized: " + ReMap(initialMagnitude, 40.0f, 250.0f, 0.01f, 1.0f));
        return (initialDirection.normalized * -1.0f) * ReMap(initialMagnitude, minRadPossible, maxRadPossible, 0.01f, 1.0f) * forceMultiplier;
    }

    public void ResetBall()        
    {
        _activeBall.transform.position = _throwStartingPoint.position;
        _activeBall.velocity = Vector2.zero;
        _activeBall.angularVelocity = 0.0f;
        _activeBall.bodyType = RigidbodyType2D.Kinematic;
    }

    public void UpdateArrowUI(Vector2 initialDirection, float initialMagnitude, bool inMaxRadius, float maxRadPossible)
    {
        float newScale = 0.0f;
        float colorValue = 0.0f;

        if (_activeBall!=null)
        {
            _arrow.SetActive(true);
            _arrow.transform.position = _throwStartingPoint.position + (Vector3)initialDirection.normalized * _ballOffsetArrow;
            _arrow.transform.localRotation = Quaternion.AngleAxis(Vector2.SignedAngle(new Vector2(1.0f, 0.0f), initialDirection.normalized), new Vector3(0.0f, 0.0f, 1.0f));

            colorValue = ReMap(initialMagnitude, 0.0f, maxRadPossible, 0.0f, 1.0f);

            if (inMaxRadius)
            {
                newScale = ReMap(initialMagnitude, 0.0f, maxRadPossible, _minXScaleArrow, _maxXScaleArrow);
                _arrow.transform.localScale = new Vector3(newScale, _arrow.transform.localScale.y, _arrow.transform.localScale.z);

                colorValue = ReMap(newScale, _minXScaleArrow, _maxXScaleArrow, 0.0f, 1.0f);
            }

            ChangeArrowColorGradient(colorValue);
        }
    }

    private void ChangeArrowColorGradient(float value)
    {
        //Debug.Log(value);

        Color c = new Color(value, 1.0f - value, 0.0f);
        _arrow.transform.GetChild(0).GetComponent<Image>().color = c;
    }

    public void DisableArrow()
    {
        _arrow.SetActive(false);
    }

    private float ReMap(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }


}
