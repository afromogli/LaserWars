using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts
{
    public class ExplosionSpawner : MonoBehaviour
    {
        public const int MaxExplosions = 100;

        public GameObject ExplosionPrefab;

        private ObjectPool objectPool;
        public ObjectPool ObjectPool
        {
            get
            {
                if (objectPool == null)
                {
                    objectPool = new ObjectPool(MaxExplosions, () => {
                        return Instantiate(ExplosionPrefab);
                    });
                }
                return objectPool;
            }
        }

        public GameObject Spawn(Vector3 position, Quaternion rotation)
        {
            GameObject explosionObj = ObjectPool.GetObjectFromPool();
            explosionObj.transform.SetPositionAndRotation(position, rotation);

            Explosion explosionScript = explosionObj.GetComponent<Explosion>();
            explosionScript.ExplosionPool = ObjectPool;

            //ParticleSystem particleSystem = explosionObj.GetComponent<ParticleSystem>();
            //particleSystem.Stop();
            //particleSystem.time = 0;
            //particleSystem.Play();

            return explosionObj;
        }
    }
}
