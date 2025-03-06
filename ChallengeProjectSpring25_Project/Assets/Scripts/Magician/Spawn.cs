using UnityEngine;
using UnityEngine.UIElements;

public class Spawn : MonoBehaviour
{
    [SerializeField] GameObject theMagician;
    [SerializeField] GameObject theBoss;
    private float time;
    private float radius = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 offseft = new Vector3(Random.Range(-radius, radius), Random.Range(-radius,radius), Random.Range(-radius,radius));
        time += Time.deltaTime;//timer for spawning of the magicians
        Vector3 offset = Random.insideUnitSphere * radius;//found that calling insideUnitSphere is supposed to spawn a cirlce radius
        
        Vector3 spawn = theBoss.transform.position + offset;
        if (time >= 2f)
        {
            //Debug.Log(Random.insideUnitSphere);
            Instantiate(theMagician, spawn, Quaternion.identity);
            time = 0;
        }
    }
}
