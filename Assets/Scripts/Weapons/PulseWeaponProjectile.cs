using Assets.Scripts.Common;
using System;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class PulseWeaponProjectile : PooledGameObject
    {
        public float CurrentSpeed;

        public PulseWeaponProjectile(Func<GameObject> createGameObjectMethod) : base(createGameObjectMethod)
        {
        }

        public void Reset()
        {
            CurrentSpeed = 0f;
        }
    }
}
