using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts
{
    public class Explosion : MonoBehaviour
    {
        public float TimeAliveSeconds = 10f;

        private float timeLeftAlive;

        private bool isAlive = false;

        public ObjectPool ExplosionPool { get; set; }

        public void Update()
        {
            if (!isAlive)
            {
                return;
            }

            if (timeLeftAlive >= 0)
            {
                timeLeftAlive -= Time.deltaTime;
            }
            else
            {
                isAlive = false;
                ExplosionPool.Release(gameObject);
            }
        }

        public void Explode()
        {
            isAlive = true;
            timeLeftAlive = TimeAliveSeconds;
        }
    }
}
