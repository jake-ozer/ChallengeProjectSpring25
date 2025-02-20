using System;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    public GameObject[] firePrefabs;
    public LayerMask obstacleLayers;
    private float groundLevel = -1.92f;

    public void Start()
    {
        SpawnFire();
    }

    public void SpawnFire()
    {
        // TODO: Figure out spawning position
        // TODO: Account for things on the map
     
        for (uint x = 0; x < 50; ++x)
        {
            for (uint y = 0; y < 50; ++y)
            {
                float noise = Mathf.PerlinNoise((float)x/5f, (float)y/5f);
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
                    Instantiate(firePrefabs[level], new Vector3(x, groundLevel, y), Quaternion.identity);
                }
            }
        }
    }
}
