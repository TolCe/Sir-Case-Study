using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private GameObject _successPanel;

    private void OnEnable()
    {
        GameEvents.Instance.OnPlayerScoreUpdated += OnCoinUIUpdated;
        GameEvents.Instance.OnGameSuccess += OnGameSuccess;
    }

    private void Start()
    {
        _scoreText.text = "";
        _successPanel.SetActive(false);
    }
    private void OnDisable()
    {
        GameEvents.Instance.OnPlayerScoreUpdated -= OnCoinUIUpdated;
        GameEvents.Instance.OnGameSuccess -= OnGameSuccess;
    }

    private void OnCoinUIUpdated(int totalScore)
    {
        _scoreText.text = totalScore + "";
    }

    private void OnGameSuccess()
    {
        _scoreText.text = "";
        _successPanel.SetActive(true);
    }

    public void ResetGame()
    {
        _successPanel.SetActive(false);
        GameEvents.Instance.GameRestart();
    }
}
