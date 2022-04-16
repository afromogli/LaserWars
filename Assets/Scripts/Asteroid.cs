using UnityEngine;

namespace Assets.Scripts
{
    public class Asteroid : MonoBehaviour
    {
        public void Start()
        {
            
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "PulseProjectile")
            {
                Debug.Log("Asteroid destroyed");
            }
        }
    }
}
