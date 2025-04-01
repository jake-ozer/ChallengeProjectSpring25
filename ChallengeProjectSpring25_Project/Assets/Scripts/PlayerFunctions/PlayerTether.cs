using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder;

public class PlayerTether : MonoBehaviour
{
    public float TetherRadius = 20f;
    public int requiredHitsToUntether = 2;
    public float coolDown = 5;
    [SerializeField] public LayerMask TetherMask;

    private GameObject tetherObject = null;
    private int numHitsTilUntether;
    private float timer;

    public Material origCircleMaterial;
    public Material activeCircleMaterial;

    private bool onCooldown;

    private void Start()
    {
        numHitsTilUntether = requiredHitsToUntether;
        timer = coolDown;
    }

    public void RegisterHit()
    {
        //Debug.Log("hit registered");
        if (!tetherObject) return;
        numHitsTilUntether--;
        if (numHitsTilUntether <= 0)
        {
            tetherObject = null;
            //numHitsTilUntether = coolDown;
            onCooldown = true;
            //Debug.Log("Untethered");
            numHitsTilUntether = requiredHitsToUntether;
        }
    }

    public bool CanMoveTo(Vector3 pos)
    {
        if (!tetherObject) return true;

        Vector3 dist = transform.position + pos - tetherObject.transform.position;
        
        //Debug.Log(dist);
        //Debug.Log(TetherRadius * TetherRadius);
        
        if (Vector3.Dot(dist, dist) <= TetherRadius * TetherRadius) return true;
        return false;
    }

    private void Update()
    {
        if (onCooldown)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                onCooldown = false;
                timer = coolDown;
            }
        }

        if (tetherObject) return;

        /*        foreach (var obj in FindFirstObjectByType<BossHealth>().gameObject)
                {

                }*/

        var obj = FindFirstObjectByType<BossHealth>().gameObject;

        Vector3 dist = transform.position - obj.transform.position;
        if (Vector3.Dot(dist, dist) <= TetherRadius * TetherRadius && !onCooldown)
        {
            tetherObject = obj;
            //Debug.Log("Tethered");
        }


        //LOGIC FOR VISUAL INDICATORS
        var tetherCircleIndicator = obj.transform.Find("TetherCircleIndicator").gameObject;
        tetherCircleIndicator.transform.localScale = new Vector3(TetherRadius, tetherCircleIndicator.transform.localScale.y, TetherRadius);
        if (tetherObject == null)
        {
            tetherCircleIndicator.GetComponent<MeshRenderer>().material = origCircleMaterial;
            //tetherCircleIndicator.SetActive(true);
        }
        else {
            //tetherCircleIndicator.SetActive(false);
            tetherCircleIndicator.GetComponent<MeshRenderer>().material = activeCircleMaterial;
        }



    }
}
