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
            gameObject.SetActive(false);
            CoinRegulator.Instance.RegulatePosition(this);
            GameEvents.Instance.CoinCollected(_prize);
        }
    }

    public void SelectNewPosition(Vector3 newPos)
    {
        gameObject.SetActive(true);
        transform.position = newPos;
        GameEvents.Instance.DynamicObjectPositioned(gameObject, transform.position);
    }
}
