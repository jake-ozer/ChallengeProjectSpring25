using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder;

public class PlayerTether : MonoBehaviour
{
    public float TetherRadius = 20f;
    public int requiredHitsToUntether = 2;
    public int coolDown = 5;
    [SerializeField] public LayerMask TetherMask;

    private GameObject tetherObject = null;
    private int numHitsTilUntether;
    private int timer;

    public void RegisterHit()
    {
        Debug.Log("hit registered");
        if (!tetherObject) return;
        if (--numHitsTilUntether >= 0)
        {
            tetherObject = null;
            numHitsTilUntether = coolDown;
            Debug.Log("Untethered");
        }
    }

    public bool CanMoveTo(Vector3 pos)
    {
        if (!tetherObject) return true;

        Vector3 dist = transform.position + pos - tetherObject.transform.position;
        
        //Debug.Log(Vector3.Dot(dist, dist));
        //Debug.Log(TetherRadius * TetherRadius);
        
        if (Vector3.Dot(dist, dist) <= TetherRadius * TetherRadius) return true;
        return false;
    }

    private void Update()
    {
        if (tetherObject) return;
        if (timer-- > 0) return;

        foreach (var obj in FindFirstObjectByType<BossHealth>().gameObject)
        {
            Vector3 dist = transform.position - obj.transform.position;
            if (Vector3.Dot(dist, dist) <= TetherRadius * TetherRadius)
            {
                tetherObject = obj;
                Debug.Log("Tethered");
            }
        }
    }
}
