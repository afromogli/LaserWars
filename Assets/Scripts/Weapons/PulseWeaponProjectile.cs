using Assets.Scripts.Common;
using System;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class PulseWeaponProjectile : PooledGameObject
    {
        public float CurrentSpeed;
        public Vector3 TempForward { get; set; }

        public PulseWeaponProjectile(Func<GameObject> createGameObjectMethod) : base(createGameObjectMethod)
        {
            Rigidbody rigidBody = GameObject.GetComponent<Rigidbody>();

            rigidBody.isKinematic = false;
            rigidBody.detectCollisions = true;
        }

        public new void Disable()
        {
            base.Disable();
            CurrentSpeed = 0f;
        }
    }
}
