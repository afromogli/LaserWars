using UnityEngine;

namespace Assets.Scripts
{
    public class Asteroid : MonoBehaviour
    {
        public Transform ExplosionPrefab;

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "PulseProjectile")
            {
                ContactPoint contact = collision.contacts[0];
                Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
                Vector3 position = contact.point;

                Debug.Log("Asteroid destroyed");
                //Instantiate(ExplosionPrefab, position, rotation);
                Destroy(gameObject);
            }
        }
    }
}
