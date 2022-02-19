using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRegulator : MonoBehaviour
{
    private static PositionRegulator _instance = null;
    public static PositionRegulator Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (PositionRegulator)FindObjectOfType(typeof(PositionRegulator));
            }

            return _instance;
        }
    }

    public Dictionary<GameObject, Vector3> DynamicPositions;

    private void Awake()
    {
        DynamicPositions = new Dictionary<GameObject, Vector3>();
    }
    private void OnEnable()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnDynamicObjectPositioned += AddPosition;
        }
    }
    private void OnDisable()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnDynamicObjectPositioned -= AddPosition;
        }
    }

    public void AddPosition(GameObject obj, Vector3 position)
    {
        if (DynamicPositions.ContainsKey(obj))
        {
            DynamicPositions[obj] = position;
        }
        else
        {
            DynamicPositions.Add(obj, position);
        }
    }
}
