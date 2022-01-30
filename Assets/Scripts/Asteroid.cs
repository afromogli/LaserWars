using UnityEngine;

namespace Assets.Scripts
{
    public class Asteroid : MonoBehaviour
    {
        public Transform ExplosionPrefab;

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "PulseProjectile")
            {
                Debug.Log("Asteroid destroyed");
                //Instantiate(ExplosionPrefab, position, rotation);
                Destroy(gameObject);
            }
        }
    }
}
