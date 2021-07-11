using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseChaser : MonoBehaviour
{
    // 카메라로부터의 거리
    public float _distanceFromCamera = 5f;

    [Range(0.01f, 1.0f)]
    public float _ChasingSpeed = 0.1f;

    private Vector3 _mousePos;
    private Vector3 _nextPos;

    private void OnValidate()
    {
        if(_distanceFromCamera < 0f)
            _distanceFromCamera = 0f;
    }

    void Update()
    {
        _mousePos = Input.mousePosition;
        _mousePos.z = _distanceFromCamera;

        _nextPos = Camera.main.ScreenToWorldPoint(_mousePos);
        transform.position = Vector3.Lerp(transform.position, _nextPos, _ChasingSpeed);
    }
}
