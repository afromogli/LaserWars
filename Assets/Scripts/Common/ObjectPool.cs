using System;
using UnityEngine;

namespace Assets.Scripts.Common
{
    public class ObjectPool
    {
        private GameObject[] pooledObjects;

        public ObjectPool(int capacity, Func<GameObject> createGameObjectMethod)
        {
            pooledObjects = new GameObject[capacity];

            GameObject newObject;
            for (int i = 0; i < capacity; i++)
            {
                newObject = createGameObjectMethod();
                newObject.SetActive(false);
                pooledObjects[i] = newObject;
            }
        }

        public GameObject GetObjectFromPool()
        {
            for (int i = 0; i < pooledObjects.Length; i++)
            {
                if (!pooledObjects[i].activeInHierarchy)
                {
                    return pooledObjects[i];
                }
            }
            return default(GameObject);
        }

        public void Release(GameObject obj)
        {
            obj.SetActive(false);
        }
    }
}
