using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _prize;
    public float x, z;
    private Transform _player;

    private void OnEnable()
    {
        GameEvents.Instance.OnCoinTriggered += OnTriggered;
    }

    private void OnDisable()
    {
        GameEvents.Instance.OnCoinTriggered -= OnTriggered;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            CheckSpawnPosition();
        }
    }

    private void OnTriggered(Transform player, GameObject collided)
    {
        if (collided == gameObject)
        {
            _player = player;
            gameObject.SetActive(false);
            SelectNewPosition();
            GameEvents.Instance.CoinCollected(_prize);
        }
    }

    private void SelectNewPosition()
    {
        Vector3 newPosition = new Vector3(Random.Range(-x, x), transform.position.y, Random.Range(-z, z));

        if (Vector3.Distance(newPosition, _player.position) < 1)
        {
            SelectNewPosition();
            return;
        }

        transform.position = newPosition;
        gameObject.SetActive(true);
    }

    private void CheckSpawnPosition()
    {
        Vector3 newPosition = new Vector3(Random.Range(-x, x), transform.position.y, Random.Range(-z, z));
        transform.position = newPosition;
        gameObject.SetActive(true);
    }
}
