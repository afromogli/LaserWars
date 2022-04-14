using Assets.Scripts.Common;
using Assets.Scripts.Weapons;
using System.Collections.Generic;
using UnityEngine;

public class PulseWeapon : MonoBehaviour
{
    public GameObject PulseWeaponPrefab;
    private AudioSource PulseSoundAudioSource;
    public float Speed, Acceleration;
    public int MaxProjectileCount = 500;
    public float FireCooldown = 1f;
    public float RayMaxDistance = 10000f;
    private float WorldOutOfBounds = 5000f;
    //private float ProjectileSpawnOffset = 5f;

    private ObjectPool<PulseWeaponProjectile> pulseWeaponPool;
    private List<PulseWeaponProjectile> activeProjectiles;
    private float currentCooldown;

    public PulseWeapon()
    {
        activeProjectiles = new List<PulseWeaponProjectile>(MaxProjectileCount);
        currentCooldown = 0;
    }

    // Start is called before the first frame update
    public void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    { 
        List<PulseWeaponProjectile> projectilesToDisable = new List<PulseWeaponProjectile>();
        foreach (PulseWeaponProjectile projectile in activeProjectiles)
        {
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

    public void FixedUpdate()
    {
        if (pulseWeaponPool == null)
        {
            pulseWeaponPool = new ObjectPool<PulseWeaponProjectile>(MaxProjectileCount, () =>
            {
                return new PulseWeaponProjectile(() => { return Instantiate(PulseWeaponPrefab); });
            });
        }
        if (PulseSoundAudioSource == null)
        {
            PulseSoundAudioSource = GetComponent<AudioSource>();
        }

        currentCooldown -= Time.deltaTime;
        if (Input.GetAxis("Fire1") > 0 && currentCooldown <= 0)
        {
            SpawnProjectile();
            currentCooldown = FireCooldown;
        }
    }

    private void SpawnProjectile()
    {
        RaycastHit hit;
        Camera cam = Camera.main;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        var startPos = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 hitPosition;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(ray, out hit, RayMaxDistance))
        {
            Debug.DrawRay(startPos, ray.direction * hit.distance, Color.yellow, 3, false);
            Debug.Log("Did Hit");

            hitPosition = hit.point;
        }
        else
        {
            Debug.DrawRay(startPos, ray.direction * RayMaxDistance, Color.white, 3, false);
            Debug.Log("Did not Hit");

            hitPosition = startPos + ray.direction * RayMaxDistance;
        }

        PulseWeaponProjectile newProjectile = pulseWeaponPool.GetObjectFromPool();
        GameObject projGameObj = newProjectile.GameObject;
        projGameObj.SetActive(true);
        projGameObj.transform.position = transform.position;
        projGameObj.transform.rotation = transform.rotation;
        ShotBehavior shotBehavior = projGameObj.GetComponent<ShotBehavior>();
        shotBehavior.SetTarget(hitPosition);

        if (PulseSoundAudioSource != null)
        {
            PulseSoundAudioSource.Play();
        }
    }


    void OnGUI()
    {
        Camera cam = Camera.main;
        Vector3 mousePos = Input.mousePosition;
        Vector3 point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.farClipPlane));

        GUILayout.BeginArea(new Rect(20, 20, 250, 120));
        GUILayout.Label("Screen pixels: " + cam.pixelWidth + ":" + cam.pixelHeight);
        GUILayout.Label("Mouse position: " + mousePos);
        GUILayout.Label("World position: " + point.ToString("F3"));
        GUILayout.EndArea();
    }

    //void OnDrawGizmosSelected()
    //{
    //    Camera cam = Camera.main;
    //    Vector3 mousePos = Input.mousePosition;
    //    Vector3 point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.farClipPlane));
    //    // Draws a blue line from this transform to the target
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawLine(gameObject.transform.position, point);

    //    Vector3 newForward = (point - gameObject.transform.position).normalized;
    //    newForward = newForward * 100.0f;
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawLine(gameObject.transform.position, newForward);
    //}
}
