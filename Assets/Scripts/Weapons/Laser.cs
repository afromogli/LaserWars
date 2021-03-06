using Assets.Scripts;
using Assets.Scripts.Common;
using UnityEngine;

public class Laser : MonoBehaviour {

    public Vector3 Target { get; set; }
    public float Speed { get; set; }

    private AudioSource ExplosionSound;
    public ExplosionSpawner ExplosionSpawner { get; set; }
    public ObjectPool LaserPool { get; set; }
    public bool IsAlive { get; set; }

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
        if (!IsAlive)
        {
            return;
        }
        float step = Speed * Time.deltaTime;

        if (Target != null)
        {
            if (transform.position == Target)
            {
                Explode();
                IsAlive = false;
                LaserPool.Release(gameObject);
                return;
            }
            transform.position = Vector3.MoveTowards(transform.position, Target, step);
        }

    }

    void Explode()
    {
        if (ExplosionSpawner != null)
        {
            ExplosionSpawner.Spawn(transform.position, transform.rotation);

            if (ExplosionSound != null)
            {
                ExplosionSound.Play();
            }
        }
    }
}
