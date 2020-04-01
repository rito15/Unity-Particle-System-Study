using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rito;

namespace SpaceBounceBall
{
    public class GameManager : RitoSingleton<GameManager>
    {
        public float _gravityYForce = -Physics.gravity.y;

        protected override void Awake()
        {
            base.Awake();

            DontDestroyOnLoad(gameObject);
        }

        private void FixedUpdate()
        {
            if (Physics.gravity.y != _gravityYForce)
                Physics.gravity = new Vector3(Physics.gravity.x, -_gravityYForce, Physics.gravity.z);
        }
    }
}