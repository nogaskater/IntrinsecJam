using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { LEFT, RIGHT};
public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public Animator Animator => _animator;

    private void Awake()
    {
        if (_animator == null)
            throw new ArgumentNullException("_animator");
    }

    public void SetSpriteDirection(Direction direction)
    {
        if (direction == Direction.LEFT)
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * - 1, transform.localScale.y);
        }
        else if(direction == Direction.RIGHT)
        { 
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
    }
}
