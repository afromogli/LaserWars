using Assets.Scripts.Common;
using System.Collections.Generic;
using UnityEngine;

public class PulseWeapon : MonoBehaviour
{
    public GameObject PulseWeaponPrefab;
    public AudioClip PulseSound;
    public float Speed, Acceleration;
    public int MaxProjectileCount = 500;
    private ObjectPool pulseWeaponPool;
    private List<GameObject> activeProjectiles;

    // Start is called before the first frame update
    void Start()
    {
        pulseWeaponPool = new ObjectPool(MaxProjectileCount, () => {
            return Instantiate<GameObject>(PulseWeaponPrefab);
        });
        activeProjectiles = new List<GameObject>(MaxProjectileCount);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Fire1") > 0)
        {
            SpawnProjectile();
        }
    }

    private void SpawnProjectile()
    {
        GameObject newProjectile = pulseWeaponPool.GetObjectFromPool();

        activeProjectiles.Add(newProjectile);

        // TODO: how to handle speed and acceleration? Lerp? 
        // TODO: how to handle collisions? Here?
    }
}
