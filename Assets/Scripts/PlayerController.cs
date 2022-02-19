using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 _initPlayerPos;
    private Vector3 _initMousePos;
    private Rigidbody _rb;
    [SerializeField] private float _sensitivity;
    [SerializeField] private float _rotateSpeed;
    private int _totalScore;
    private bool _canControl;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        _initPlayerPos = transform.position;
        _canControl = true;
    }
    private void OnEnable()
    {
        GameEvents.Instance.OnCoinCollected += OnCoinCollected;
        GameEvents.Instance.OnGameRestart += OnGameRestart;
    }

    private void OnDisable()
    {
        GameEvents.Instance.OnCoinCollected -= OnCoinCollected;
        GameEvents.Instance.OnGameRestart -= OnGameRestart;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_canControl)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _rb.velocity = Vector3.zero;
                _rb.angularVelocity = Vector3.zero;
                _initMousePos = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 direction = Input.mousePosition - _initMousePos;
                //transform.Translate(direction.x / Screen.width * Vector3.right + direction.y / Screen.height * Vector3.forward);
                _rb.velocity = _sensitivity * (Mathf.Clamp(direction.x / Screen.width, -5, 5) * Vector3.right + Mathf.Clamp(direction.y / Screen.height, -5, 5) * Vector3.forward);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(new Vector3(direction.x / Screen.width, 0, direction.y / Screen.height)), _rotateSpeed * Time.fixedDeltaTime);
                GameEvents.Instance.PlayerMoved(transform);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            GameEvents.Instance.CoinTriggered(transform, other.gameObject);
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

    private void OnGameRestart()
    {
        _canControl = true;
        _totalScore = 0;
        transform.position = _initPlayerPos;
    }
}
