using UnityEngine;

public class ShotBehavior : MonoBehaviour {

    public Vector3 target;
    public GameObject collisionExplosion;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;

        if (target != null)
        {
            if (transform.position == target)
            {
                Explode();
                return;
            }
            transform.position = Vector3.MoveTowards(transform.position, target, step);
        }

    }

    public void SetTarget(Vector3 target)
    {
        this.target = target;
    }

    void Explode()
    {
        if (collisionExplosion != null)
        {
            GameObject explosion = (GameObject)Instantiate(
                collisionExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(explosion, 1f);
        }
    }
}
