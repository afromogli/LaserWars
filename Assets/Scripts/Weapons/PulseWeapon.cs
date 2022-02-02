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
    private float WorldOutOfBounds = 5000f;
    private float ProjectileSpawnOffset = 5f;

    private ObjectPool<PulseWeaponProjectile> pulseWeaponPool;
    private List<PulseWeaponProjectile> activeProjectiles;
    private float currentCooldown;

    // Start is called before the first frame update
    void Start()
    {
        pulseWeaponPool = new ObjectPool<PulseWeaponProjectile>(MaxProjectileCount, () =>
        {
            return new PulseWeaponProjectile(() => { return Instantiate(PulseWeaponPrefab); });
        });
        activeProjectiles = new List<PulseWeaponProjectile>(MaxProjectileCount);
        currentCooldown = 0;
        PulseSoundAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (Input.GetAxis("Fire1") > 0 && currentCooldown <= 0)
        {
            SpawnProjectile();
            currentCooldown = FireCooldown;
        }

        List<PulseWeaponProjectile> projectilesToDisable = new List<PulseWeaponProjectile>();
        foreach (PulseWeaponProjectile projectile in activeProjectiles)
        {
            projectile.CurrentSpeed = Mathf.Lerp(projectile.CurrentSpeed, Speed, Acceleration * Time.deltaTime);
            projectile.GameObject.transform.position += projectile.Forward * projectile.CurrentSpeed;

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

        // calc new forward vector based on mouse coordinates
        Vector3 mousePos = Input.mousePosition;
        var cam = Camera.main;
        Vector3 point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.farClipPlane));

        // Store forward vector in projectile since it is overridden by the rotation set later on if saved in transform object
        newProjectile.Forward = (point - gameObject.transform.position).normalized;
        projGameObj.transform.position += newProjectile.Forward * ProjectileSpawnOffset; // Spawn a little bit in front of ship
        
        projGameObj.transform.rotation = Quaternion.AngleAxis(90, gameObject.transform.right) * gameObject.transform.rotation;

        activeProjectiles.Add(newProjectile);

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

    void OnDrawGizmosSelected()
    {
        Camera cam = Camera.main;
        Vector3 mousePos = Input.mousePosition;
        Vector3 point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.farClipPlane));
        // Draws a blue line from this transform to the target
        Gizmos.color = Color.green;
        Gizmos.DrawLine(gameObject.transform.position, point);

        Vector3 newForward = (point - gameObject.transform.position).normalized;
        newForward = newForward * 100.0f;
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(gameObject.transform.position, newForward);
    }
}
