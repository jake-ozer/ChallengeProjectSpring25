using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FireManager : MonoBehaviour
{
    public GameObject[] firePrefabs;
    public LayerMask obstacleLayers;
    public LayerMask groundLayer;

    // min/max boudns of where the fire will spawn
    private Vector3 minBounds, maxBounds;

    private Perlin perlin;
    
    // neccessary to avoid passing negative coordinates to the perlin function
    private float perlinOffset = 500f;
    
    // these should correspond to the size of the fire assets
    private float xStep = 1.5f, yStep = 1.5f;

    public void Start()
    {
        perlin = new Perlin((int)Random.value);
        findMinMax();
        //Debug.Log(minBounds);
        //Debug.Log(maxBounds);
        SpawnFire();
    }

    private void findMinMax()
    {
        minBounds = new Vector3(float.MaxValue, 0, float.MaxValue);
        maxBounds = new Vector3(float.MinValue, 0, float.MinValue);

        GameObject[] allObjects = GameObject.FindObjectsByType<GameObject>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);
        foreach (GameObject obj in allObjects)
        {
            if ((groundLayer.value & (1 << obj.layer)) != 0)
            {
                Renderer renderer = obj.GetComponent<Renderer>();
                if (renderer)
                {
                    Bounds bounds = renderer.bounds;

                    if (bounds.min.x < minBounds.x) minBounds.x = bounds.min.x;
                    if (bounds.min.z < minBounds.z) minBounds.z = bounds.min.z;
                    if (bounds.max.x > maxBounds.x) maxBounds.x = bounds.max.x;
                    if (bounds.max.z > maxBounds.z) maxBounds.z = bounds.max.z;
                }
                
                
            }
        }
    }

    public void SpawnFire()
    {
        // TODO: Figure out spawning position
        // TODO: Account for things on the map
     
        for (float x = minBounds.x; x < maxBounds.x; x+=xStep)
        {
            for (float y = minBounds.z; y < maxBounds.z; y+=yStep)
            {
                float noise = perlin.perlin2d(x+perlinOffset, y+perlinOffset, 0.1f, 1);
                int level = -1;
                if (noise < 1 && noise > 0.9)
                {
                    level = 2;
                }
                else if (noise < 0.9 && noise > 0.8)
                {
                    level = 1;
                }
                else if (noise < 0.8 && noise > 0.7)
                {
                    level = 0;
                }

                if (level > -1)
                {
                    Vector3 rayStart = new Vector3(x, 100, y);
                    if(Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, 
                           Mathf.Infinity, groundLayer))
                    {
                        float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
                        if(slopeAngle <= 30)
                        {
                            Vector3 spawnPos = hit.point;
                            Quaternion spawnRot = Quaternion.FromToRotation(Vector3.up, hit.normal);
                            var fireObj = Instantiate(firePrefabs[level], spawnPos, spawnRot);
                            fireObj.transform.parent = this.transform;
                        }
                    }
                }
            }
        }
    }
}
