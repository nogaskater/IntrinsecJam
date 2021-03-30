using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_ThrowController : MonoBehaviour
{
    [Header("GameObject References")]
    [SerializeField] private Rigidbody2D _npcBall;
    [SerializeField] private Transform _throwStartingPoint;
    [SerializeField] private Transform _player;

    [Header("Throw Settings")]
    [SerializeField] private float _minHeight;
    [SerializeField] private float _maxHeight;

    //--Controlling the assisted throw--//
    private float height;
    private float gravity;
    private float px;
    private float py;
    private float vy;
    private float vx;

    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void ThrowBall()
    {
        height = ComputeRandomHeight();
        gravity = (_npcBall.gravityScale * Physics2D.gravity.magnitude);
        Debug.Log(gravity);
        vy = Mathf.Sqrt(-2 * -gravity * height);
        px = _player.position.x - _throwStartingPoint.position.x;
        py = _player.position.y - _throwStartingPoint.position.y;
        vx = px / (Mathf.Sqrt(- ((2 * height) / -gravity)) + Mathf.Sqrt((2 * (py - height)) / -gravity));
        Debug.Log("Vy: " + vy + " /// Vx: " + vx);

        _npcBall.bodyType = RigidbodyType2D.Dynamic;
        _npcBall.velocity = new Vector2(vx, vy);
    }

    private float ComputeRandomHeight()
    {
        return Random.Range(_player.position.y + _minHeight, _player.position.y + _maxHeight);
    }
}
