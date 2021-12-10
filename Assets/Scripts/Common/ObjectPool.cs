using System;

namespace Assets.Scripts.Common
{
    public class ObjectPool<T> where T : IPooledObject
    {
        private T[] pooledObjects;

        public ObjectPool(int capacity, Func<T> createGameObjectMethod)
        {
            pooledObjects = new T[capacity];

            T newObject;
            for (int i = 0; i < capacity; i++)
            {
                newObject = createGameObjectMethod();
                newObject.GameObject.SetActive(false);
                pooledObjects[i] = newObject;
            }
        }

        public T GetObjectFromPool()
        {
            for (int i = 0; i < pooledObjects.Length; i++)
            {
                if (!pooledObjects[i].GameObject.activeInHierarchy)
                {
                    return pooledObjects[i];
                }
            }
            return default(T);
        }
    }
}
