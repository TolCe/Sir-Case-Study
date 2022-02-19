using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _successPanel;

    private void OnEnable()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnPlayerScoreUpdated += OnCoinUIUpdated;
            GameEvents.Instance.OnGameSuccess += OnGameSuccess;
        }
    }
    private void Start()
    {
        _scoreText.text = "";
        _startPanel.SetActive(true);
        _successPanel.SetActive(false);
    }
    private void OnDisable()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnPlayerScoreUpdated -= OnCoinUIUpdated;
            GameEvents.Instance.OnGameSuccess -= OnGameSuccess;
        }
    }

    private void OnCoinUIUpdated(int totalScore)
    {
        _scoreText.text = totalScore + "";
    }

    private void OnGameSuccess()
    {
        _successPanel.SetActive(true);
    }

    public void StartGame()
    {
        _startPanel.SetActive(false);
        GameEvents.Instance.GameStarted();
    }

    public void ResetGame()
    {
        _scoreText.text = "";
        _startPanel.SetActive(true);
        _successPanel.SetActive(false);
        GameEvents.Instance.GameRestarted();
    }
}
