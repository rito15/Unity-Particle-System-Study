using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rito;
using NaughtyAttributes;

namespace SpaceBounceBall
{
    public class MoveController : MonoBehaviour
    {
        [GetComponent, ReadOnly]
        public Rigidbody _rigidbody;

        [GetComponent, ReadOnly]
        public MaterialChanger _materialChanger;

        public float _power = 2000f;
        /// <summary> 충전도 : 0 ~ 100 </summary>
        public float _powerCharge = 0f;
        public float _powerIncrement = 150f;

        private Vector3 _prevPos;
        public Vector3 _velocity;

        void Start()
        {
            _prevPos = transform.position;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _powerCharge = 0f;
            }
            else if (Input.GetMouseButton(0) && _velocity.Equals(Vector3.zero))
            {
                _powerCharge += _powerIncrement * Time.deltaTime;   // 기모으기
                _powerCharge.Ex_Clamp(0f, 100f);

                _materialChanger?.MakeBlack(5f);
            }
            else if (Input.GetMouseButtonUp(0) && _powerCharge > 0f)
            {
                ShootToMousePoint();
                _powerCharge = 0f;

                _materialChanger?.ResetMaterialColor();
            }

            if (!_velocity.Equals(Vector3.zero))
            {
                _powerCharge = 0f;
                _materialChanger?.ResetMaterialColor();
            }
        }

        void FixedUpdate()
        {
            _velocity = transform.position - _prevPos;
            _prevPos = transform.position;

            if (_velocity.y.Ex_Range(-0.01f, 0.01f) && 
                RitoRaycaster.AtoB(transform.position, (transform.position + Vector3.down), 0.51f, Layers.Map))
            {
                _rigidbody.Ex_ResetAllForces();
                _velocity = Vector3.zero;
            }
        }

        private void ShootToMousePoint()
        {
            Vector3 clickPoint = RitoRaycaster.GetCamToMouse(Layers.TouchArea);
            if (clickPoint.Equals(Vector3.negativeInfinity))
                return;

            Vector3 direction = clickPoint - transform.position; direction.Normalize();

            //_rigidbody.AddForce(direction * _power * (_powerCharge * 0.01f), ForceMode.Force);
            _rigidbody.AddForce(direction * _power * (_powerCharge * 0.01f), ForceMode.Acceleration);
        }
    }
}
