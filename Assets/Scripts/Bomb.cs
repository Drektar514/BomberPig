using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private Transform _gridRelative;
    [SerializeField] private float _timeToActive;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _speedForce;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidBody2D;
    private CircleCollider2D _circleCollider2D;
    private bool _isActive = false;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _circleCollider2D = GetComponent<CircleCollider2D>();
        _circleCollider2D.enabled = false;
    }

    private void Update()
    {
        _lifeTime -= Time.deltaTime;

        if(_lifeTime <= _timeToActive)
        {
            _circleCollider2D.enabled = true;
            _spriteRenderer.color = Color.red;
        }

        if (_lifeTime <= 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isActive)
        {
            if(collision.TryGetComponent(out Player player))
            {
              player.Dye();
            }
            if (collision.TryGetComponent(out Enemy enemy))
            {
                Destroy(collision.gameObject);
            }

            Destroy(gameObject);
        }   
    }

    public void Launch(Vector2 direction)
    {
        _isActive = true;
        _spriteRenderer.color = Color.red;
        _rigidBody2D.AddForce(direction * _speedForce);
    }
}
