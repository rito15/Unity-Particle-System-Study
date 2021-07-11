using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleRotator : MonoBehaviour
{
    [Range(0f, 10f)] public float _radiusX = 4f;
    [Range(0f, 10f)] public float _radiusY = 4f;
    [Range(0f, 10f)] public float _speed = 1f;
    public Transform _target;

    private float _currentAngle = 0f;

    private void OnEnable()
    {
        if (_target == null)
        {
            _target = new GameObject("Rotator Target").transform;
            _target.position = Camera.main.transform.position + Camera.main.transform.forward * 10f;
        }
    }

    private void Update()
    {
        if(_target == null) return;

        Vector3 targetPos = _target.transform.position;

        transform.position = new Vector3(
            Mathf.Cos(_currentAngle) * _radiusX + targetPos.x,
            Mathf.Sin(_currentAngle) * _radiusY + targetPos.y,
            targetPos.z
        );

        _currentAngle = Mathf.Clamp(_currentAngle + Time.deltaTime * _speed, 0f, 360f);
    }
}
