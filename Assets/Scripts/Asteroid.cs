using UnityEngine;

namespace Assets.Scripts
{
    public class Asteroid : MonoBehaviour
    {
        public Transform ExplosionPrefab;
        public AudioSource ExplosionSound;

        public void Start()
        {
            GameObject asteroidSpawner = GameObject.FindGameObjectWithTag("AsteroidSpawner");
            if (asteroidSpawner != null)
            {
                ExplosionSound = asteroidSpawner.GetComponent<AudioSource>();
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "PulseProjectile")
            {
                if (ExplosionSound != null)
                {
                    ExplosionSound.Play();
                }
                Debug.Log("Asteroid destroyed");
                //Instantiate(ExplosionPrefab, position, rotation);
                Destroy(gameObject);
            }
        }
    }
}
