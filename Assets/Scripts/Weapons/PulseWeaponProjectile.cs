using Assets.Scripts.Common;
using System;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    // TODO: is this class needed? Can it be merged with ShotBehavior?
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
