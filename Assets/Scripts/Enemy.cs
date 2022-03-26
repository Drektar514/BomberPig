using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _gridRelative;
    [SerializeField] private float _speed;
    [SerializeField] private float _heightModule;
    [SerializeField] private float _weightModule;

    private float _moveTime;
    private Vector2[] _directions;
    private Vector2 _direction;

    private void Start()
    {
        _directions = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

    }

    private void Update()
    {
        if (_moveTime <= 0 || Mathf.Abs(transform.position.y) > _heightModule || Mathf.Abs(transform.position.x) > _weightModule)
        {
            _moveTime = Random.Range(0.5f, 2.5f);
            _direction = _directions[Random.Range(0, _directions.Length)];
        }
        Move(_direction);
    }

    private void Move(Vector2 direction)
    {
        transform.Translate(_direction * _speed * Time.deltaTime, _gridRelative);
        _moveTime -= Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Player player))
        {
            player.Dye();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Bomb bomb))
        {
            bomb.Launch(_direction);
        }
    }
}
