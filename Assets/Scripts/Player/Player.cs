using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;

    private Animator _animator;
    private PlayerMover _playerMover;

    private void Start()
    {
        _playerMover = GetComponent<PlayerMover>();
        _animator = GetComponent<Animator>();
    }
    public void Dye()
    {
        _playerMover.enabled = false;
        _animator.Play("Dead");
        Invoke("Dying", 1f);
    }

    private void Dying()
    {
        Time.timeScale = 0;
        _gameOverPanel.SetActive(true);
    }
}
