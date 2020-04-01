using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rito;
using NaughtyAttributes;

namespace SpaceBounceBall
{
    public class MaterialChanger : MonoBehaviour
    {
        [GetComponentInAChild("Sphere Mesh")]
        public Renderer _renderer;

        private Material _material;
        private Color _originalRGB;

        private void Awake()
        {

        }

        private void Start()
        {
            if (_renderer)
                _material = _renderer.material;

            if(_material)
                _originalRGB = _material.color;
        }

        /// <summary>
        /// <para/> [Public]
        /// <para/> 점차 검정으로 만들기
        /// <para/> * intensity : 0~100 (100 : 바로 검정으로 바꾸기)
        /// </summary>
        public void MakeBlack(float intensity)
        {
            if (isActiveAndEnabled == false) return;

            intensity *= 0.01f;

            float r = _material.color.r;
            float g = _material.color.g;
            float b = _material.color.b;

            r = r - (r * intensity);
            g = g - (g * intensity);
            b = b - (b * intensity);

            r.Ex_Clamp(0f, 1f);
            g.Ex_Clamp(0f, 1f);
            b.Ex_Clamp(0f, 1f);

            _material.color = new Color(r, g, b);
        }

        /// <summary>
        /// <para/> [Public]
        /// <para/> 최초 색상으로 돌리기
        /// </summary>
        public void ResetMaterialColor()
        {
            if (isActiveAndEnabled == false) return;

            _material.color = _originalRGB;
        }
    }
}