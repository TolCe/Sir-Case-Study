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

    public event Action OnGameStarted;
    public void GameStarted()
    {
        if (OnGameStarted != null)
        {
            OnGameStarted();
        }
    }

    public event Action<GameObject, Vector3> OnDynamicObjectPositioned;
    public void DynamicObjectPositioned(GameObject obj, Vector3 position)
    {
        if (OnDynamicObjectPositioned != null)
        {
            OnDynamicObjectPositioned(obj, position);
        }
    }

    public event Action<GameObject> OnCoinTriggered;
    public void CoinTriggered(GameObject collided)
    {
        if (OnCoinTriggered != null)
        {
            OnCoinTriggered(collided);
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
    public event Action OnGameRestarted;
    public void GameRestarted()
    {
        if (OnGameRestarted != null)
        {
            OnGameRestarted();
        }
    }
}