using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Animator))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Vector2 _horizontalColliderSize;
    [SerializeField] private Vector2 _verticallColliderSize;
    [SerializeField] private Transform _gridRelative;
    [SerializeField] private float _speed;

    private Animator _animator;
    private BoxCollider2D _boxCollider2D;
    private Vector2 _direction;
    private float _horizontalInput;
    private float _verticalInput;

    private void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        MoveDirection();
    }

    private void MoveDirection()
    {
        _horizontalInput = _joystick.Horizontal;
        _verticalInput = _joystick.Vertical;

        _animator.SetFloat("Horizontal", _horizontalInput);
        _animator.SetFloat("Vertical", _verticalInput);

        if (_horizontalInput > 0)
        {
            Move(Vector3.right);
            ChangeCollider();
        }

        else if (_horizontalInput < 0)
        {
            Move(Vector3.left);
            ChangeCollider();
        }

        else if (_verticalInput > 0)
        {
            Move(Vector3.up);
            ChangeCollider();
        }

        else if (_verticalInput < 0)
        {
            Move(Vector3.down);
            ChangeCollider();
        }
    }

    private void Move(Vector3 direction)
    {
        _direction = direction;
        transform.Translate(direction * _speed * Time.deltaTime, _gridRelative);
    }

    private void ChangeCollider()
    {
        if (Mathf.Abs(_horizontalInput) > 0)
            _boxCollider2D.size = _horizontalColliderSize;
        else if (Mathf.Abs(_verticalInput) > 0)
            _boxCollider2D.size = _verticallColliderSize;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bomb bomb))
        {
            bomb.Launch(_direction);
        }
    }

}
