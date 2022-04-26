using Assets.Scripts;
using Assets.Scripts.Common;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : MonoBehaviour
{
    public GameObject LaserPrefab;
    private AudioSource LaserSound;
    public int MaxProjectileCount = 500;
    public float FireCooldown = 0.5f;
    public float RayMaxDistance = 10000f;
    private float WorldOutOfBounds = 5000f;
    public float Speed;

    private ObjectPool laserPool;
    private List<GameObject> activeLasers;
    private float currentCooldown;

    private ExplosionSpawner explosionSpawner;

    public LaserWeapon()
    {
        activeLasers = new List<GameObject>(MaxProjectileCount);
        currentCooldown = 0;
    }

    // Start is called before the first frame update
    public void Start()
    {
        ExplosionSpawner[] explSpawners = FindObjectsOfType<ExplosionSpawner>();
        explosionSpawner = explSpawners[0];
    }

    // Update is called once per frame
    public void Update()
    { 
        List<GameObject> projectilesToDisable = new List<GameObject>();
        foreach (GameObject projectile in activeLasers)
        {
            if (!projectile.activeInHierarchy || Mathf.Abs(projectile.transform.position.magnitude) >= WorldOutOfBounds)
            {
                projectilesToDisable.Add(projectile);
            }
        }
        foreach (GameObject disabledLaser in projectilesToDisable)
        {
            activeLasers.Remove(disabledLaser);
        }
    }

    public void FixedUpdate()
    {
        if (laserPool == null)
        {
            laserPool = new ObjectPool(MaxProjectileCount, () =>
            {
                return Instantiate(LaserPrefab);
            });
        }
        if (LaserSound == null)
        {
            LaserSound = GetComponent<AudioSource>();
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
        
        GameObject laserGameObj = laserPool.GetObjectFromPool();
        laserGameObj.SetActive(true);
        laserGameObj.transform.position = transform.position;
        laserGameObj.transform.rotation = transform.rotation;
        Laser laserScript = laserGameObj.GetComponent<Laser>();
        laserScript.IsAlive = true;
        laserScript.Target = hitPosition;
        laserScript.Speed = Speed;
        laserScript.LaserPool = laserPool;
        laserScript.ExplosionSpawner = explosionSpawner;

        if (LaserSound != null)
        {
            LaserSound.Play();
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
