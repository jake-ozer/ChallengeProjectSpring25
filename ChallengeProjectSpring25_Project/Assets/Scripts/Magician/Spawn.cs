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
        
        
        
        if (time >= 4f)
        {
            Vector3 offset = new Vector3(Random.insideUnitCircle.y * radius, .10f);//found that calling insideUnitSphere is supposed to spawn a cirlce radius
            offset.z = .5f;
            Vector3 spawn = theBoss.transform.position + offset;
            Debug.Log("offset: " + offset);
            Debug.Log("boss pos: " + theBoss.transform.position);
            Debug.Log("spawn: " + spawn);
            Vector3 testOff = new Vector3(5, .1f, 5);
            //Debug.Log(Random.insideUnitSphere);
            var miniEnemy = Instantiate(theMagician, spawn, Quaternion.identity);
            miniEnemy.transform.parent = theBoss.transform;
            time = 0;
        }
    }
}
