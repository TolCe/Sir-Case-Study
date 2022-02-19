using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _sensitivity = 20;
    [SerializeField] private float _rotateSpeed = 360;
    [SerializeField] private float _maxSpeed = 5;
    private Rigidbody _rb;
    private Vector3 _initPlayerPos;
    private Vector3 _initMousePos;
    private int _totalScore;
    private bool _canControl;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _initPlayerPos = transform.position;
    }
    private void OnEnable()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnCoinCollected += OnCoinCollected;
            GameEvents.Instance.OnGameStarted += OnGameStarted;
            GameEvents.Instance.OnGameRestarted += OnGameRestarted;
        }
    }
    private void OnDisable()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnCoinCollected -= OnCoinCollected;
            GameEvents.Instance.OnGameStarted -= OnGameStarted;
            GameEvents.Instance.OnGameRestarted -= OnGameRestarted;
        }
    }

    private void FixedUpdate()
    {
        if (_canControl)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _rb.velocity = Vector3.zero;
                _rb.angularVelocity = Vector3.zero;
                _initMousePos = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                Vector3 direction = Input.mousePosition - _initMousePos;
                _rb.velocity = _sensitivity * (Mathf.Clamp(direction.x / Screen.width, -_maxSpeed, _maxSpeed) * Vector3.right + Mathf.Clamp(direction.y / Screen.height, -_maxSpeed, _maxSpeed) * Vector3.forward);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(new Vector3(direction.x / Screen.width, 0, direction.y / Screen.height)), _rotateSpeed * Time.fixedDeltaTime);
                GameEvents.Instance.PlayerMoved(transform);
            }
            else
            {
                _initMousePos = Input.mousePosition;
            }
        }

        GameEvents.Instance.DynamicObjectPositioned(gameObject, transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            GameEvents.Instance.CoinTriggered(other.gameObject);
        }
    }

    private void OnCoinCollected(int prize)
    {
        _totalScore += prize;
        GameEvents.Instance.PlayerScoreUpdated(_totalScore);

        if (_totalScore >= 100)
        {
            _canControl = false;
            GameEvents.Instance.GameSuccess();
        }
    }

    private void OnGameStarted()
    {
        _canControl = true;
    }

    private void OnGameRestarted()
    {
        _canControl = false;
        _totalScore = 0;
        transform.position = _initPlayerPos;
    }
}
