using Assets.Scripts.Common;
using System;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class PulseWeaponProjectile : PooledGameObject
    {
        public PulseWeaponProjectile(Func<GameObject> createGameObjectMethod) : base(createGameObjectMethod)
        {
        }

        public new void Disable()
        {
            base.Disable();
        }
    }
}
