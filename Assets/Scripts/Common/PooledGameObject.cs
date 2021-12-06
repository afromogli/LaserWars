using System;
using UnityEngine;

namespace Assets.Scripts.Common
{
    public class PooledGameObject : IObjectPoolItem
    {
        public GameObject GameObject { get; set; }

        public PooledGameObject(Func<GameObject> createGameObjectMethod)
        {
            GameObject = createGameObjectMethod();
        }

        public void Disable()
        {
            GameObject.SetActive(false);
        }
    }
}
