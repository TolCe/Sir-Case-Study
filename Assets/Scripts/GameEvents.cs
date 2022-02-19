using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    private static GameEvents _instance = null;
    public static GameEvents Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (GameEvents)FindObjectOfType(typeof(GameEvents));
            }

            return _instance;
        }
    }

    public event Action<Transform, GameObject> OnCoinTriggered;
    public void CoinTriggered(Transform player, GameObject collided)
    {
        if (OnCoinTriggered != null)
        {
            OnCoinTriggered(player, collided);
        }
    }

    public event Action<int> OnCoinCollected;
    public void CoinCollected(int prize)
    {
        if (OnCoinCollected != null)
        {
            OnCoinCollected(prize);
        }
    }

    public event Action<int> OnPlayerScoreUpdated;
    public void PlayerScoreUpdated(int totalScore)
    {
        if (OnPlayerScoreUpdated != null)
        {
            OnPlayerScoreUpdated(totalScore);
        }
    }

    public event Action<Transform> OnPlayerMoved;
    public void PlayerMoved(Transform player)
    {
        if (OnPlayerMoved != null)
        {
            OnPlayerMoved(player);
        }
    }

    public event Action OnGameSuccess;
    public void GameSuccess()
    {
        if (OnGameSuccess != null)
        {
            OnGameSuccess();
        }
    }
    public event Action OnGameRestart;
    public void GameRestart()
    {
        if (OnGameRestart != null)
        {
            OnGameRestart();
        }
    }
}