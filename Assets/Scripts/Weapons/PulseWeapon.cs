using Assets.Scripts.Common;
using Assets.Scripts.Weapons;
using System.Collections.Generic;
using UnityEngine;

public class PulseWeapon : MonoBehaviour
{
    public GameObject PulseWeaponPrefab;
    public AudioClip PulseSound;
    public float Speed, Acceleration;
    public int MaxProjectileCount = 500;
    public float FireCooldown = 1f;
    private float WorldOutOfBounds = 10000f;


    private ObjectPool<PulseWeaponProjectile> pulseWeaponPool;
    private List<PulseWeaponProjectile> activeProjectiles;
    private float currentCooldown;

    // Start is called before the first frame update
    void Start()
    {
        pulseWeaponPool = new ObjectPool<PulseWeaponProjectile>(MaxProjectileCount, () => {
            return new PulseWeaponProjectile(() => { return Instantiate(PulseWeaponPrefab); });
        });
        activeProjectiles = new List<PulseWeaponProjectile>(MaxProjectileCount);
        currentCooldown = 0;
    }

    //private PulseWeaponProjectile testProjectile;

    // Update is called once per frame
    void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (Input.GetAxis("Fire1") > 0 && currentCooldown <= 0)
        {
            SpawnProjectile();
            currentCooldown = FireCooldown;
            //Debug.Log("Projectile spawned");
        }

        Vector3 forward = gameObject.transform.forward;
        List<PulseWeaponProjectile> projectilesToDisable = new List<PulseWeaponProjectile>();
        foreach (PulseWeaponProjectile projectile in activeProjectiles)
        {
            projectile.CurrentSpeed = Mathf.Lerp(projectile.CurrentSpeed, Speed, Acceleration * Time.deltaTime);
            projectile.GameObject.transform.position += forward * projectile.CurrentSpeed;

            if (Mathf.Abs(projectile.GameObject.transform.position.magnitude) >= WorldOutOfBounds)
            {
                projectilesToDisable.Add(projectile);
                projectile.Disable();
            }
        }
        foreach (PulseWeaponProjectile disabledProjectile in projectilesToDisable)
        {
            activeProjectiles.Remove(disabledProjectile);
        }     
    }

    private void SpawnProjectile()
    {
        PulseWeaponProjectile newProjectile = pulseWeaponPool.GetObjectFromPool();
        GameObject projGameObj = newProjectile.GameObject;
        projGameObj.SetActive(true);
        projGameObj.transform.position = gameObject.transform.position;
        projGameObj.transform.forward = gameObject.transform.forward;
        // rotate projectile 90 degrees "forwards"
        projGameObj.transform.rotation = Quaternion.AngleAxis(90, gameObject.transform.right) * gameObject.transform.rotation;

        activeProjectiles.Add(newProjectile);
        // TODO: how to handle collisions? Here?
    }
}
