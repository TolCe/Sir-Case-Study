using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _followSpeed;

    private void OnEnable()
    {
        GameEvents.Instance.OnPlayerMoved += OnPlayerMoved;
    }

    private void OnDisable()
    {
        GameEvents.Instance.OnPlayerMoved -= OnPlayerMoved;
    }

    private void OnPlayerMoved(Transform player)
    {
        transform.position = Vector3.Lerp(transform.position, player.position + _offset, _followSpeed * Time.deltaTime);
    }
}
