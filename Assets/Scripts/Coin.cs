using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _prize;

    private void OnEnable()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnCoinTriggered += OnTriggered;
        }
    }

    private void OnDisable()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnCoinTriggered -= OnTriggered;
        }
    }

    private void OnTriggered(GameObject collided)
    {
        if (collided == gameObject)
        {
            SelectNewPosition();
            GameEvents.Instance.CoinCollected(_prize);
        }
    }

    public void SelectNewPosition()
    {
        transform.position = CoinRegulator.Instance.SelectNewPositionForCoins(transform.position.y);
        GameEvents.Instance.DynamicObjectPositioned(gameObject, transform.position);
    }
}
