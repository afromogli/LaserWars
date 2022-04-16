using UnityEngine;

public class ShotBehavior : MonoBehaviour {

    public Vector3 Target { get; set; }
    public float Speed { get; set; }

    public GameObject CollisionExplosion;

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
            GameObject explosion = (GameObject)Instantiate(
                CollisionExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(explosion, 1f);
        }
    }
}
