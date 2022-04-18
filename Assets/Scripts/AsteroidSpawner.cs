using Assets.Scripts.Common;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject AsteroidPrefab;
    public float Spacing;
    public float RandomSpacing;
    public Vector3 WorldCenterPoint;
    public int ObjectsPerSide;
    public float ScalingMinSize = 1f;
    public float ScalingMaxSize = 3f;

    private ObjectPool objectPool;

    // Start is called before the first frame update
    void Start()
    {
        int objectCount = ObjectsPerSide * ObjectsPerSide * ObjectsPerSide;
        Debug.Log("objectCount: " + objectCount);
        objectPool = new ObjectPool(objectCount, () => { return Instantiate(AsteroidPrefab); });
        Spawn();
    }

    private void Spawn()
    {
        /////////////////////////////////////////////////
        // Spawns asteroids inside a cubic volume
        /////////////////////////////////////////////////

        // Total distance = Spacing * AsteroidCount / NumberOfLayers
        //int asteroidCountPerLayer = AsteroidCount / CubeSideSize;
        float sideLengthPerLayer = Spacing * ObjectsPerSide;
        float halfSideLength = sideLengthPerLayer / 2;

        Debug.Log("total number of objects: " + ObjectsPerSide * ObjectsPerSide * ObjectsPerSide);

        Vector3 position = Vector3.zero;
        // Bottom layer to top layer
        for (int y = 0; y < ObjectsPerSide; y++)
        {
            for (int x = 0; x < ObjectsPerSide; x++)
            {
                for (int z = 0; z < ObjectsPerSide; z++)
                {
                    GameObject gameObject = objectPool.GetObjectFromPool();
                    gameObject.SetActive(true);
                    position.x = (WorldCenterPoint.x - halfSideLength) + x * Spacing + (Random.value * RandomSpacing);
                    position.y = (WorldCenterPoint.y - halfSideLength) + y * Spacing + (Random.value * RandomSpacing);
                    position.z = (WorldCenterPoint.z - halfSideLength) + z * Spacing + (Random.value * RandomSpacing);
                    gameObject.transform.position = position;

                    Random.InitState((int)System.DateTime.Now.Ticks + z + x + y);

                    gameObject.transform.Rotate(Random.value * 360, Random.value * 360, 0);

                    float randomScaleSize = Random.Range(ScalingMinSize, ScalingMaxSize);

                    gameObject.transform.localScale = new Vector3(randomScaleSize, randomScaleSize, randomScaleSize);
                }
            }
        }

    }
}
