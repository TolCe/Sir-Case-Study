using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRegulator : MonoBehaviour
{
    private static CoinRegulator _instance = null;
    public static CoinRegulator Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (CoinRegulator)FindObjectOfType(typeof(CoinRegulator));
            }

            return _instance;
        }
    }

    [SerializeField] private int _initialCoinAmount = 5;
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private float _areaXMax = 4, _areaZMax = 14;
    [SerializeField] private float _minDistanceToCreate = 2;
    private int _totalCoinDeactive;

    private void Start()
    {
        SetCoins();
    }

    private void SetCoins()
    {
        for (int i = 0; i < _initialCoinAmount; i++)
        {
            GameObject coin = Instantiate(_coinPrefab, transform);
            SelectNewPositionForCoins(coin.GetComponent<Coin>());
        }
    }

    public void SelectNewPositionForCoins(Coin coin)
    {
        Vector3 newPosition = Vector3.zero;
        bool canPlace = false;

        while (!canPlace)
        {
            newPosition = new Vector3(Random.Range(-_areaXMax, _areaXMax), coin.transform.position.y, Random.Range(-_areaZMax, _areaZMax));
            int totalAvoided = 0;
            foreach (var item in PositionRegulator.Instance.DynamicPositions.Values)
            {
                if (Vector3.Distance(newPosition, item) > _minDistanceToCreate)
                {
                    totalAvoided++;
                }
            }
            if (totalAvoided >= PositionRegulator.Instance.DynamicPositions.Count)
            {
                canPlace = true;
            }
        }

        coin.SelectNewPosition(newPosition);
    }

    public void RegulatePosition(Coin coin)
    {
        _totalCoinDeactive++;

        if (_totalCoinDeactive == _initialCoinAmount)
        {
            SelectNewPositionForCoins(coin);
        }
        else
        {
            StartCoroutine(WaitToRespawn(coin));
        }
    }

    private IEnumerator WaitToRespawn(Coin coin)
    {
        yield return new WaitForSeconds(Random.Range(0, 5f));
        SelectNewPositionForCoins(coin);
    }
}
