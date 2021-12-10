using System;
using UnityEngine;

namespace Assets.Scripts.Common
{
    public class PooledGameObject : IPooledObject
    {
        public GameObject GameObject { get; set; }

        public PooledGameObject(Func<GameObject> createGameObjectMethod)
        {
            GameObject = createGameObjectMethod();
        }

        public virtual void Disable()
        {
            GameObject.SetActive(false);
        }
    }
}
