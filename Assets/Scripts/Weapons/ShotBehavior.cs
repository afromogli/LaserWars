using UnityEngine;

public class ShotBehavior : MonoBehaviour {

    public Vector3 Target { get; set; }
    public float Speed { get; set; }

    public GameObject CollisionExplosion;
    private AudioSource ExplosionSound;

    public void Start()
    {
        GameObject asteroidSpawner = GameObject.FindGameObjectWithTag("AsteroidSpawner");
        if (asteroidSpawner != null)
        {
            ExplosionSound = asteroidSpawner.GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float step = Speed * Time.deltaTime;

        if (Target != null)
        {
            if (transform.position == Target)
            {
                Explode();
                return;
            }
            transform.position = Vector3.MoveTowards(transform.position, Target, step);
        }

    }

    void Explode()
    {
        if (CollisionExplosion != null)
        {
            // TODO: use PooledGameObject instead
            GameObject explosion = (GameObject)Instantiate(CollisionExplosion, transform.position, transform.rotation);
            Destroy(explosion, 1f);
            if (ExplosionSound != null)
            {
                ExplosionSound.Play();
            }
            gameObject.SetActive(false);
        }
    }
}
